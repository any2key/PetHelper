using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.IO.Compression;
using WebKeyGenerator.Models.Buisness;
using WebKeyGenerator.Models.Identity;
using WebKeyGenerator.Models.Requests;
using WebKeyGenerator.Models.Responses;
using WebKeyGenerator.Services;

namespace WebKeyGenerator.Controllers
{
    [Route("api/[controller]")]
    public class DownloadController
    {
        string path;
        string rootpath;
        public DownloadController(IWebHostEnvironment env)
        {
            path=Path.Combine(env.ContentRootPath,"logs");
            rootpath = env.ContentRootPath;
        }
        [HttpGet("getlogs")]
        public async Task<FileStreamResult> DownloadAsync()
        {
            var fileName = $"logs{DateTime.Now.ToShortDateString()}.zip";
            var mimeType = "application/zip";

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);


            if(File.Exists(Path.Combine(rootpath, $"logs{DateTime.Now.ToShortDateString()}.zip")))
            {
                File.Delete(Path.Combine(rootpath, $"logs{DateTime.Now.ToShortDateString()}.zip"));
            }

            ZipFile.CreateFromDirectory(
           Path.Combine(path),
            Path.Combine(rootpath, $"logs{DateTime.Now.ToShortDateString()}.zip"));


            Stream stream = File.OpenRead(Path.Combine(rootpath,fileName));


            return new FileStreamResult(stream, mimeType)
            {
                FileDownloadName = fileName
            };
        }
    }
}
