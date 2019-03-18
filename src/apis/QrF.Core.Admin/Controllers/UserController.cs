using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QrF.Core.Admin.Dto;
using QrF.Core.Admin.Interfaces;
using Microsoft.AspNetCore.Hosting;

namespace QrF.Core.Admin.Controllers
{
    /// <summary>
    /// 用户管理
    /// </summary>
    [Route("AdminAPI/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBusiness _business;
        private readonly IHostingEnvironment _env;
        public UserController(IUserBusiness business, IHostingEnvironment env)
        {
            _business = business;
            _env = env ?? throw new ArgumentNullException(nameof(env));
        }
        /// <summary>
        /// 查询分页列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet("GetPageList")]
        public async Task<BasePageQueryOutput<QueryUserDto>> GetPageListAsync([FromQuery] QueryUsersInput input)
        {
            return await _business.GetPageList(input);
        }
        /// <summary>
        /// 编辑用户信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("Edit")]
        public async Task<IActionResult> EditAsync([FromBody] UserDto input)
        {
            await _business.EditModel(input);
            return Ok(new MsgResultDto { Success = true });
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        [HttpPost("Delete")]
        public async Task<IActionResult> DeleteAsync([FromBody] DelInput input)
        {
            if (input == null || !input.KeyId.HasValue) throw new Exception("编号不存在");
            await _business.DelModel(input.KeyId.Value);
            return Ok(new MsgResultDto { Success = true });
        }
        [HttpPost("Upload")]
        public async Task<IActionResult> Upload()
        {
            if (Request.Form.Files.Count < 1)
                return Ok(false);
            string fileName = Path.GetFileName(Request.Form.Files[0].FileName);
            string extension = Path.GetExtension(fileName).ToLower();
            var newName = $"{Guid.NewGuid().ToString("N")}{extension}";
            var url = await SaveFileAsync(Request.Form.Files[0].OpenReadStream(), GenerateDictionary(), newName);
            return Ok(new { Title = fileName, Url = Url.Content(url) });

        }
        private async Task<string> SaveFileAsync(Stream stream, string directory, string fileName)
        {
            string filePath = Path.Combine(MapPath(directory), fileName);
            DirectoryInfo dir = new DirectoryInfo(Path.GetDirectoryName(filePath));
            if (!dir.Exists)
            {
                dir.Create();
            }
            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                await stream.CopyToAsync(fileStream);
            }
            string webPath = string.Join("/", directory.Split(new char[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar }, StringSplitOptions.RemoveEmptyEntries));

            return $"~/{webPath}/{fileName}";
        }
        private string GenerateDictionary()
        {
            return Path.Combine("Files");
        }
        private string MapPath(string path)
        {
            var dic = Path.Combine(path.TrimStart('~').Split(new char[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar }, StringSplitOptions.RemoveEmptyEntries));
            return Path.Combine(_env.WebRootPath, dic);
        }
    }
}
