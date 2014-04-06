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
    public partial class AddAuthor : Window
    {
        Effects effs = new Effects();
        DefaultValue dv = new DefaultValue();

        public AddAuthor()
        {
            InitializeComponent();
        }

        private void authorFirstName_GotFocus(object sender, RoutedEventArgs e)
        {
            effs.TextBoxGotFocus(sender as TextBox, dv.DefaultFirstName);
        }

        private void authorSecondName_GotFocus(object sender, RoutedEventArgs e)
        {
            effs.TextBoxGotFocus(sender as TextBox, dv.DefaultSurName);
        }

        private void authorSecondName_LostFocus(object sender, RoutedEventArgs e)
        {
            effs.TextBoxLostFocus(sender as TextBox, dv.DefaultSurName);
        }

        private void authorFirstName_LostFocus(object sender, RoutedEventArgs e)
        {
            effs.TextBoxLostFocus(sender as TextBox, dv.DefaultFirstName);
        }
    }
}
