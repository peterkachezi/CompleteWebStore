using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebStore.Models
{
    public class SMSResponse
    {
        public string Date { get; set; }
        public string From { get; set; }
        public string Id { get; set; }
        public string LinkId { get; set; }
        public string Text { get; set; }
        public string NetworkCode { get; set; }
        public string To { get; set; }
    }
}