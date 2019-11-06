using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using SkiaSharp;

namespace ImagePreviewAPI.Services
{
    public class ImageUploaderService
    {
        private readonly IWebHostEnvironment environment;

        public ImageUploaderService(IWebHostEnvironment environment)
        {
            this.environment = environment;
        }

        public async Task<string> Upload(string url)
        {
            string guid = Guid.NewGuid().ToString();

            using (HttpClient httpClient = new HttpClient())
            {
                using SKBitmap skBitmap = SKBitmap.Decode(await httpClient.GetByteArrayAsync(url));

                if (skBitmap == null) throw new Exception();

                using Stream outputStream = File.OpenWrite(environment.WebRootPath + "/original/" + guid + ".jpg");
                using Stream outputResizedStream = File.OpenWrite(environment.WebRootPath + "/preview/" + guid + ".jpg");

                using (SKImage skImage = SKImage.FromBitmap(skBitmap))
                {
                    skImage.Encode(SKEncodedImageFormat.Jpeg, 70).SaveTo(outputStream);
                }

                using (SKBitmap skBitmapResized = skBitmap.Resize(new SKImageInfo(100, 100), SKFilterQuality.High))
                {
                    if (skBitmapResized == null) throw new Exception();

                    using (SKImage skImage = SKImage.FromBitmap(skBitmapResized))
                    {
                        skImage.Encode(SKEncodedImageFormat.Jpeg, 70).SaveTo(outputResizedStream);
                    }
                }
            }

            return guid;
        }
    }
}
