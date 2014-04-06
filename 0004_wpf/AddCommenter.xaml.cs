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
    /// Логика взаимодействия для AddCommenter.xaml
    /// </summary>
    public partial class AddCommenter : Window
    {
        DefaultValue dv = new DefaultValue();
        Effects effs = new Effects();

        public AddCommenter()
        {
            InitializeComponent();
        }

        private void nickName_GotFocus(object sender, RoutedEventArgs e)
        {
            effs.TextBoxGotFocus(sender as TextBox, dv.DefaultNickName);
        }

        private void nickName_LostFocus(object sender, RoutedEventArgs e)
        {
            effs.TextBoxLostFocus(sender as TextBox, dv.DefaultNickName);
        }
    }
}
