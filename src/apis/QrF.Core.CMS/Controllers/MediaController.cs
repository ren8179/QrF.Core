using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QrF.Core.CMS.Entities;
using QrF.Core.CMS.Service;
using QrF.Core.ComFr.Mvc.Controllers;
using QrF.Core.ComFr.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QrF.Core.Utils.Extension;
using QrF.Core.ComFr.Constant;
using System.IO;

namespace QrF.Core.CMS.Controllers
{
    [Route("CMSAPI/[controller]")]
    public class MediaController : BasicController<MediaEntity, string, IMediaService>
    {
        private readonly ILogger _logger;
        private readonly IStorage _storage;
        public MediaController(IMediaService service,
            ILoggerFactory loggerFactory,
            IStorage storage)
            : base(service)
        {
            _logger = loggerFactory.CreateLogger<MediaController>();
            _storage = storage;
        }
        [HttpPost("Upload")]
        public async Task<IActionResult> Upload(string parentId, string folder, long size)
        {
            if (Request.Form.Files.Count < 1)
                return Ok(false);
            if (folder.IsNotNullAndWhiteSpace())
            {
                var parent = Service.Get(m => m.Title == folder && m.MediaType == (int)MediaType.Folder).FirstOrDefault();
                if (parent == null)
                {
                    parent = new MediaEntity
                    {
                        Title = folder,
                        MediaType = (int)MediaType.Folder,
                        ParentID = "#"
                    };
                    Service.Add(parent);
                }
                parentId = parent.ID;
            }
            parentId = parentId ?? "#";
            string fileName = Path.GetFileName(Request.Form.Files[0].FileName);
            var entity = new MediaEntity
            {
                ParentID = parentId,
                Title = fileName,
                Status = Request.Form.Files[0].Length == size ? (int)RecordStatus.Active : (int)RecordStatus.InActive
            };
            string extension = Path.GetExtension(fileName).ToLower();
            entity.Url = await _storage.SaveFileAsync(Request.Form.Files[0].OpenReadStream(), $"{Guid.NewGuid().ToString("N")}{extension}");
            if (entity.Url.IsNotNullAndWhiteSpace())
            {
                Service.Add(entity);
                entity.Url = Url.Content(entity.Url);
            }
            return Ok(entity);

        }
        [HttpPost("AppendFile")]
        public async Task<IActionResult> AppendFile(string id, long position, long size)
        {
            var media = Service.Get(id);
            if (media == null || Request.Form.Files.Count < 1)
                return Ok(false);
            if (position + Request.Form.Files[0].Length == size)
            {
                media.Status = (int)RecordStatus.Active;
                Service.Update(media);
            }
            await _storage.AppendFileAsync(Request.Form.Files[0].OpenReadStream(), media.Url);
            media.Url = Url.Content(media.Url);
            return Ok(media);
        }
        [HttpGet("Delete")]
        public override IActionResult Delete(string ids)
        {
            DeleteMedia(ids);
            return base.Delete(ids);
        }

        private void DeleteMedia(string mediaId)
        {
            var media = Service.Get(mediaId);
            if (media != null && media.MediaType != (int)MediaType.Folder)
            {
                if (media.Url.StartsWith("http://") || media.Url.StartsWith("https://"))
                {
                    media.Url = "~" + new Uri(media.Url).AbsolutePath;
                }

                _storage.Delete(media.Url);
            }
            else
            {
                Service.Get(m => m.ParentID == mediaId).Each(m => DeleteMedia(m.ID));
            }
            Service.Remove(mediaId);
        }
    }
}
