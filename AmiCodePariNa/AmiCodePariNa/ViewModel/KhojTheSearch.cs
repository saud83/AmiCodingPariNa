using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AmiCodePariNa.ViewModel
{
    public class KhojTheSearch
    {
        [Required(ErrorMessage = "Provide some numbers")]
        [Display(Name = "Input Values")]
        public string listOfNumbers { get; set; }
        [Required(ErrorMessage = "Enter a number to be searched")]
        [Display(Name = "Search Value")]
        public int findNumber { get; set; }
    }
}