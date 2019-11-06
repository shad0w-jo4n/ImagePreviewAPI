using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ImagePreviewAPI.Models;
using ImagePreviewAPI.Models.Requests;
using ImagePreviewAPI.Services;

namespace ImagePreviewAPI.Controllers
{
    [ApiController]
    [Route("api/image")]
    public class ImageController : ControllerBase
    {
        private readonly ImageUploaderService imageUploader;

        public ImageController(ImageUploaderService imageUploader)
        {
            this.imageUploader = imageUploader;
        }

        [HttpPost]
        [Route("upload")]
        public JsonResult Upload([FromBody] ImagePreviewRequest requestData)
        {
            List<Task<string>> tasks = new List<Task<string>>();

            foreach (string url in requestData.ImagesUrls)
            {
                tasks.Add(imageUploader.Upload(url));
            }

            Task<string[]> continuation = Task.WhenAll(tasks);

            try
            {
                continuation.Wait();
            }
            catch (AggregateException) {}

            List<string> failed = new List<string>();
            Dictionary<string, UploadedImagesData> uploaded = new Dictionary<string, UploadedImagesData>();

            for (int i = 0; i < tasks.Count; i++)
            {
                if (tasks[i].Status == TaskStatus.RanToCompletion)
                {
                    string originalUrl = "/original/" + tasks[i].Result + ".jpg";
                    string previewUrl = "/preview/" + tasks[i].Result + ".jpg";

                    uploaded.Add(requestData.ImagesUrls[i], new UploadedImagesData
                    {
                        Original = originalUrl,
                        Preview = previewUrl
                    });
                }
                else
                {
                    failed.Add(requestData.ImagesUrls[i]);
                }
            }

            return new JsonResult(new
            {
                Uploaded = uploaded,
                Failed = failed
            });
        }
    }
}
