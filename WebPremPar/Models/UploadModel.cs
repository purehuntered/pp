using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebPremPar.Models
{
    public class UploadModel
    {                
        [Required]
        [Display(Name = "Brand Name")]
        public string Brand { get; set; }
        [Required]
        [Display(Name = "Asset Type*")]
        public string AssetType { get; set; }
        public string CopyHL { get; set; }
        public string CopyBody { get; set; }
        [Required]
        [Display(Name = "Click Through URL*")]
        public string ClickURL { get; set; }        
        public string UserLog { get; set; }
        [Display(Name = "End Date*")]
        [Required]
        public DateTime StartDt { get; set; }
        [Display(Name = "End Date*")]
        [Required]
        public DateTime EndDt { get; set; }
        public string FileName { get; set; }
    }
}