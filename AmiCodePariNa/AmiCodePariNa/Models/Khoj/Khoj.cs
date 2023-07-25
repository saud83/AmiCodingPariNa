using AmiCodePariNa.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmiCodePariNa.Models.Khoj
{
    public class Khoj
    {
        public int Id { get; set; }
        public string listOfNumbers { get; set; }

        public DateTime date { get; set; }

        public User user { get; set; }

        public int UserId { get; set; }
    }
}