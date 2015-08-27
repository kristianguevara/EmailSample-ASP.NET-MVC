using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmailSample.Models
{
    public class EmailModel
    {
        public int Id { get; set; }
        public string subject { get; set; }
        public string message { get; set; }
        public string sender { get; set; }
        public string receiver { get; set; }
    }
}