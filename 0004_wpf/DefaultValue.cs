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
        public string DefaultSurName
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

    }
}
