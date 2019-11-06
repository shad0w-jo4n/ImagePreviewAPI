using System;
using System.ComponentModel.DataAnnotations;

namespace ImagePreviewAPI.Models.Requests
{
    public class UploadImageRequest
    {
        [Required(ErrorMessage = "'imagesUrls' field is required!")]
        [MinLength(1, ErrorMessage = "You must provide at least one link!")]
        public string[] ImagesUrls { get; set; }
    }
}
