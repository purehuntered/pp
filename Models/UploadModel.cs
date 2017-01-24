using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebPremPar.Models
{
    public class UploadModel
    {                
        [Required]
        public string Brand { get; set; }
        [Required]
        public string AssetType { get; set; }
        public string CopyHL { get; set; }
        public string CopyBody { get; set; }
        [Required]
        [Display(Name = "Click Through URL")]
        public string ClickURL { get; set; }        
        public string UserLog { get; set; }
        [Required]
        public DateTime StartDt { get; set; }
        [Required]
        public DateTime EndDt { get; set; }
        

    }
}