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
using System.ComponentModel;

namespace _0004_wpf
{
    /// <summary>
    /// Логика взаимодействия для Example.xaml
    /// </summary>
    public partial class Example : Window//, INotifyPropertyChanged
    {
        private string _str = "321";

        public Example()
        {
            InitializeComponent();
            DataContext = this;
        }

        public string Str
        {
            get
            {
                return _str;
            }
            set
            {
                if (value == _str) return;
                
                _str = value;
               // OnPropertyChanged("Str");
            }
        }
        //public event PropertyChangedEventHandler PropertyChanged;

        //protected virtual void OnPropertyChanged(string propertyName)
        //{
        //    PropertyChangedEventHandler handler = PropertyChanged;
        //    if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        //}
    }
}
