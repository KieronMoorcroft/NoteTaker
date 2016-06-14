using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NoteTaker.Models
{
    public class Notes
    {

        public int Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string Title { get; set; }

        [Required]
        public string Text { get; set; }    
        public DateTime CreatedAt { get; set; }

    }
}