using System;
using System.ComponentModel.DataAnnotations;

namespace ImagePreviewAPI.Models.Requests
{
    public class ImagePreviewRequest
    {
        [Required(ErrorMessage = "'ImageUrls' field is required. It should be array.")]
        public string[] ImageUrls { get; set; }
    }
}
