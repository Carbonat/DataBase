using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace _0004_wpf
{
    public class DefaultValue
    {
        public string DefaultFirstName
        {
            get
            {
                return "Ім'я автора";
            }
        }
        public string DefaultLastName
        {
            get
            {
                return "Прізвище автора";
            }
        }
        public string DefaultTitle
        {
            get
            {
                return "Заголовок";
            }
        }
        public string DefaultArticle
        {
            get
            {
                return "Текст статті";
            }
        }
        public string DefaultDate
        {
            get
            {
                return "Час публікації";
            }
        }
        public string DefaultComment
        {
            get
            {
                return "Немає жодного коментаря";
            }
        }
        public string DefaultComm
        {
            get
            {
                return "Текст коментара";
            }
        }
        public string DefaultNickName
        {
            get
            {
                return "Ім'я коментатора";
            }
        }
        public string TodaysDateStr
        {
            get
            {
                DateTime dt = DateTime.Today;
                return dt.ToString();
            }
        }
        public DateTime TodaysDate
        {
            get { return DateTime.Today; }
        }
        public string DefaultAddArtTitle
        {
            get
            {
                return "Додати статтю";
            }
        }
        public string DefaultChangeArtTitle
        {
            get
            {
                return "Змінити статтю";
            }
        }
        public int MaxLengthTitle
        {
            get { return 150; }
        }
        public int MaxLengthAuthName
        {
            get { return 50; }
        }
        public int MaxLengthNickName
        {
            get { return 50; }
        }
        public int MaxLengthComment
        {
            get { return 1000; }
        }
        public string HelpText
        {
            get { return "Довідка"; }
        }
        public DateTime DateStart { get; set; }
    }
}