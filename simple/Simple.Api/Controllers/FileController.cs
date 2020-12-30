using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace Simple.Api.Controllers
{
    /// <summary>
    /// 文件管理
    /// </summary>
    [Route("api/files")]
    [ApiController]
    public class FileController : BaseController
    {
        private readonly ILogger _logger;

        public FileController(ILogger<FileController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="form"></param>
        [HttpPost]
        public void UploadFile(IFormFile form)
        {
            if (form != null)
                _logger.LogInformation(@$"{DateTime.Now.ToString("yyyy-dd-MM HH:mm:ss")}上传文件 '{form.FileName}'");
        }
    }
}
