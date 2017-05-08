using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AdventureWorksApp.Models
{
    public class Photo
    {
        public int PhotoID { get; set; }
        [Required(ErrorMessage = "Please Enter Title for Photo")]
        [StringLength(200)]
        public string Title { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [Required(ErrorMessage = "Please select a photo to upload")]
        [DisplayName("Picture")]
        public byte[] PhotoFile { get; set; }
        [DisplayName("Thumpnail")]
        public byte[] ThumpnailFile { get; set; }
        [DisplayName("ImageMime")]
        public string ImageMimeType { get; set; }
        [DisplayName("Created Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yy}", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; }
        [DisplayName("Owner")]
        public string Owner { get; set; }
        public virtual ICollection<Comment>
            Comments { get; set; }
        
    }
 }