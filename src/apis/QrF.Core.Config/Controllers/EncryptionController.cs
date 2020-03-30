using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using QrF.Core.Config.Dto;
using QrF.Core.Utils.Encryption;
using System;
using System.Security.Cryptography;
using System.Text;

namespace QrF.Core.Config.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("ConfigAPI/[controller]")]
    [ApiController]
    public class EncryptionController : ControllerBase
    {
        private IMemoryCache _cache;
        public EncryptionController(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }

        [HttpPost("UploadConfig")]
        public IActionResult UploadConfig([FromForm]EncryptionInput input)
        {
            if (input == null && string.IsNullOrEmpty(input.FileName))
                return BadRequest("缺少文件名");
            var files = Request.Form.Files;
            var key = $"_{DateTime.Now:yyyyMMddHHmmss}_{Guid.NewGuid()}";
            if (!_cache.TryGetValue(input.FileName + key, out byte[] cacheEntry))
            {
                foreach (var file in files)
                {
                    if (file.Length < 1) continue;
                    using (var stream = file.OpenReadStream())
                    {
                        byte[] buffer = new byte[stream.Length];
                        stream.Read(buffer, 0, buffer.Length);
                        var passwordBytes = Encoding.ASCII.GetBytes(input.FileName);
                        passwordBytes = SHA256.Create().ComputeHash(passwordBytes);
                        cacheEntry = AES.GetEncryptedByteArray(buffer, passwordBytes);
                    }
                    break;
                }
                _cache.Set(input.FileName + key, cacheEntry, new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromHours(1)));
            }
            return Ok(key);
        }

        [HttpGet("GetFile")]
        public IActionResult GetFile(string name, string key)
        {
            var token = name + key;
            var bytes = _cache.Get<byte[]>(token);
            if (bytes == null) return BadRequest("加密文件已失效，请重新上传");
            return File(bytes, System.Net.Mime.MediaTypeNames.Application.Octet, name);
        }
        [HttpPost("UploadOldConfig")]
        public IActionResult UploadOldConfig([FromForm]EncryptionInput input)
        {
            if (input == null && string.IsNullOrEmpty(input.FileName))
                return BadRequest("缺少文件名");
            var files = Request.Form.Files;
            var key = $"_{DateTime.Now:yyyyMMddHHmmss}_{Guid.NewGuid()}";
            if (!_cache.TryGetValue(input.OldName + key, out byte[] cacheEntry))
            {
                foreach (var file in files)
                {
                    if (file.Length < 1) continue;
                    using (var stream = file.OpenReadStream())
                    {
                        byte[] buffer = new byte[stream.Length];
                        stream.Read(buffer, 0, buffer.Length);
                        var passwordBytes = Encoding.ASCII.GetBytes(input.FileName);
                        passwordBytes = SHA256.Create().ComputeHash(passwordBytes);
                        cacheEntry = AES.GetDecryptedByteArray(buffer, passwordBytes);
                    }
                    break;
                }
                _cache.Set(input.OldName + key, cacheEntry, new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromHours(1)));
            }
            return Ok(key);
        }

    }
}
