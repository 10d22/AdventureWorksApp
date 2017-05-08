using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AdventureWorksApp.Models
{
    public class Comment
    {
       
        public int CommentID { get; set; }
        public int PhotoID { get; set; }
        public string Username { get; set; }
        [Required(ErrorMessage = "Please the Text for the comment")]
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }
        public virtual Photo Photo { get; set; }
    }
}