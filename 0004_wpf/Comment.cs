using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0004_wpf
{
    public class Comment
    {
        public int CommenterId
        {
            get;
            set;
        }
        public string Comm
        {
            get;
            set;
        }
        public string Commenter { get; set; }
        public DateTime Time
        {
            get;
            set;
        }
        public string DateString
        {
            get { return Time.ToString("dd/MM/yyyy"); }
        }
    }
}
