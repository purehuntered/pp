using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebPremPar.Models
{
    public class UploadModel
    {
        [Required]
        public string Vendor { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string AssetType { get; set; }
        [Required]
        [Display(Name = "Click Through URL")]
        public string ClickURL { get; set; }
        [Required]
        public string Notes { get; set; }

    }
}