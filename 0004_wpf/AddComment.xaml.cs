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
    /// <summary>
    /// Логика взаимодействия для AddComment.xaml
    /// </summary>
    public partial class AddComment : Window
    {
        Effects effs = new Effects();
        DefaultValue dv = new DefaultValue();

        public AddComment()
        {
            InitializeComponent();

        }

        private void comment_GotFocus(object sender, RoutedEventArgs e)
        {
            effs.RichTextBoxGotFocus(sender as RichTextBox, dv.DefaultComm);
        }

        private void comment_LostFocus(object sender, RoutedEventArgs e)
        {
            effs.RichTextBoxLostFocus(sender as RichTextBox, dv.DefaultComm);
        }
    }
}
