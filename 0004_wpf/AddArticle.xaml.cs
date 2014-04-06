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
        Effects effs = new Effects();
        DefaultValue dv = new DefaultValue();
        
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
            effs.TextBoxGotFocus(sender as TextBox, dv.DefaultFirstName);
        }

        private void firstName_LostFocus(object sender, RoutedEventArgs e)
        {
            effs.TextBoxLostFocus(sender as TextBox, dv.DefaultFirstName);
        }

        private void secondName_LostFocus(object sender, RoutedEventArgs e)
        {
            effs.TextBoxLostFocus(sender as TextBox, dv.DefaultSurName);
        }

        private void secondName_GotFocus(object sender, RoutedEventArgs e)
        {
            effs.TextBoxGotFocus(sender as TextBox, dv.DefaultSurName);
        }

        private void date_GotFocus(object sender, RoutedEventArgs e)
        {
            effs.TextBoxGotFocus(sender as TextBox, dv.DefaultDate);
        }

        private void date_LostFocus(object sender, RoutedEventArgs e)
        {
            effs.TextBoxLostFocus(sender as TextBox, dv.DefaultDate);
        }

        private void title_GotFocus(object sender, RoutedEventArgs e)
        {
            effs.TextBoxGotFocus(sender as TextBox, dv.DefaultTitle);
        }

        private void title_LostFocus(object sender, RoutedEventArgs e)
        {
            effs.TextBoxLostFocus(sender as TextBox, dv.DefaultTitle);
        }

        private void article_GotFocus(object sender, RoutedEventArgs e)
        {
            effs.RichTextBoxGotFocus(sender as RichTextBox, dv.DefaultArticle);
        }

        private void article_LostFocus(object sender, RoutedEventArgs e)
        {
            effs.RichTextBoxLostFocus(sender as RichTextBox, dv.DefaultArticle);
        }
    }
}
