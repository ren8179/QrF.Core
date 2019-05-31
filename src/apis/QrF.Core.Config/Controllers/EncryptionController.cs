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
            if (!_cache.TryGetValue(input.FileName, out byte[] cacheEntry))
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
                _cache.Set(input.FileName, cacheEntry, new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromHours(2)));
            }
            return Ok();
        }

        [HttpGet("GetFile")]
        public IActionResult GetFile(string name)
        {
            var bytes = _cache.Get<byte[]>(name);
            return File(bytes, System.Net.Mime.MediaTypeNames.Application.Octet, name);
        }
    }
}
