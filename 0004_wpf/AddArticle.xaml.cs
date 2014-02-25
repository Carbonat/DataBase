using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace _0004_wpf
{
    public partial class AddArticle : Window
    {

        public const string defaultAdress = "Додати статтю автоматично ввівши посилання на неї";
        public const string defaultFirstName = "Ім'я автора";
        public const string defaultSecondName = "Прізвище автора";
        public const string defaultTitle = "Заголовок";
        public const string defaultArticle = "Текст статті";
        public const string defaultDate = "Час публікації";
        public const string defaultComment = "Немає жодного коментаря";

        public string DefaultAdress
        {
            get
            {
                return defaultAdress;
            }
        }
        public string DefaultFirstName
        {
            get
            {
                return defaultFirstName;
            }
        }
        public string DefaultSecondName
        {
            get
            {
                return defaultSecondName;
            }
        }
        public string DefaultTitle
        {
            get
            {
                return defaultTitle;
            }
        }
        public string DefaultArticle
        {
            get
            {
                return defaultArticle;
            }
        }
        public string DefaultDate
        {
            get
            {
                return defaultDate;
            }
        }
        public string DefaultComment
        {
            get
            {
                return defaultComment;
            }
        }

        public List<Comment> comments = new List<Comment>();
        public List<Comment> Comments
        {
            get
            {
                return comments;
            }
        }


        public AddArticle()
        {
            for (int i = 0; i < 5; ++i)
            {
                Comment com = new Comment();
                com.Comm = "Comment";
                com.Commenter = "Commenter";
                com.Time = new DateTime(2000 + i, 1 + i, 10 + i);
                comments.Add(com);
            }
            InitializeComponent();
            this.DataContext = this;
        }

        private void TextBoxGotFocus(TextBox tb, string defaultValue)
        {
            if (tb.Text.Equals(defaultValue, StringComparison.OrdinalIgnoreCase) && tb.Foreground == Brushes.Gray)
            {
                tb.Text = string.Empty;
                tb.Foreground = Brushes.Black;
            }
        }

        private void TextBoxLostFocus(TextBox tb, string defaultValue)
        {
            if (string.IsNullOrEmpty(tb.Text))
            {
                tb.Text = defaultValue;
                tb.Foreground = Brushes.Gray;
            }
        }

        private void firstName_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBoxGotFocus(sender as TextBox, defaultFirstName);
        }

        private void firstName_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBoxLostFocus(sender as TextBox, defaultFirstName);
        }

        private void secondName_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBoxLostFocus(sender as TextBox, defaultSecondName);
        }

        private void secondName_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBoxGotFocus(sender as TextBox, defaultSecondName);
        }

        private void date_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBoxGotFocus(sender as TextBox, defaultDate);
        }

        private void date_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBoxLostFocus(sender as TextBox, defaultDate);
        }

        private void title_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBoxGotFocus(sender as TextBox, defaultTitle);
        }

        private void title_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBoxLostFocus(sender as TextBox, defaultTitle);
        }

        private void article_GotFocus(object sender, RoutedEventArgs e)
        {
            RichTextBox rtb = sender as RichTextBox;
            TextRange tr = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
            if (tr.Text.Equals(defaultArticle + "\r\n", StringComparison.OrdinalIgnoreCase) && rtb.Foreground == Brushes.Gray)
            {
                FlowDocument document = new FlowDocument();
                Paragraph paragraph = new Paragraph();

                paragraph.Inlines.Add(string.Empty);
                document.Blocks.Add(paragraph);

                rtb.Document = document;
                rtb.Foreground = Brushes.Black;
            }
        }

        private void article_LostFocus(object sender, RoutedEventArgs e)
        {
            RichTextBox rtb = sender as RichTextBox;
            TextRange tr = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);

            if (string.IsNullOrWhiteSpace(tr.Text))
            {
                FlowDocument document = new FlowDocument();
                Paragraph paragraph = new Paragraph();

                paragraph.Inlines.Add(defaultArticle);
                document.Blocks.Add(paragraph);

                rtb.Document = document;
                rtb.Foreground = Brushes.Gray;
            }
        }
    }
}
