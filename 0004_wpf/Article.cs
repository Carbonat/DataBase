using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0004_wpf
{
    public class Article
    {
        public Author Creator { get; set; }
        public StringBuilder Text { get; set; }
        public StringBuilder Title { get; set; }
        public DateTime Date { get; set; }
        public int NumberComments { get; set; }

    }
}
