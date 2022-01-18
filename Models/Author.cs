using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class Author
    {
          //[^[1-9]\d*$]
        //[RegularExpression(@"^^[1-9]\d*$")]
        public int Id { get; set; }
        public string FullName { get; set; }
    }
}
