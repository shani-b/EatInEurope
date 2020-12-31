using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EatInEurope
{
    public class UserReview
    {
        public string RestID { get; set; }
        public string Content { get; set; }
        public string Date { get; set; }
        public int Rate { get; set; }
        public UserReview(string id, string content, string date, int rate)
        {
            RestID = id;
            Content = content;
            Date = date;
            Rate = rate;
        }
    }
}
