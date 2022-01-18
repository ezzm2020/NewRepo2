using BookStore.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.ViewModel
{
    public class BookAuthorViewModel
    {
        public int BooKID { get; set; }
        [Required]
        [MaxLength(20)]
        [MinLength(5)]
        public string Title { get; set; }

        [Required]
        [StringLength(120,MinimumLength =5)]
        public string Descroption { get; set; }
        public int AuthorId { get; set; }
        public List<Author> Authors { get; set; }
        public IFormFile File { get; set; }
        //old path of image
        public string Imgurl { get; set; }
    }
}
