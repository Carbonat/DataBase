using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0004_wpf
{
    public class Article
    {
        public Article() { }
        public Article(Article _ar)
        {
            Id = _ar.Id;
            Creator = _ar.Creator;
            Text = _ar.Text;
            Title = _ar.Title;
            Date = _ar.Date;
        }

        public int Id { get; set; }
        public Author Creator { get; set; }
        public String Text { get; set; }
        public String Title { get; set; }
        public DateTime Date { get; set; }
        public string DateString
        {
            get { return Date.ToString("dd/MM/yyyy"); }
        }
        public string ReturnCreatorName
        {
            get
            {
                return this.Creator.LastName + " " + this.Creator.FirstName;
            }
            set
            {
                value = this.Creator.LastName + " " + this.Creator.FirstName;
            }
        }
        public override string ToString()
        {
            return Title;
        }
    }
}
