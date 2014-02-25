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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;

namespace _0004_wpf
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void menu_createDB_Click(object sender, RoutedEventArgs e)
        {
            //    String str;
            //    SqlConnection myConn = new SqlConnection("Server=localhost;Integrated security=SSPI;database=master");

            //    str = "CREATE DATABASE MyDatabase ON PRIMARY " +
            //        "(NAME = MyDatabase_Data, " +
            //        "FILENAME = 'C:\\MyDatabaseData.mdf', " +
            //        "SIZE = 2MB, MAXSIZE = 10MB, FILEGROWTH = 10%) " +
            //        "LOG ON (NAME = MyDatabase_Log, " +
            //        "FILENAME = 'C:\\MyDatabaseLog.ldf', " +
            //        "SIZE = 1MB, " +
            //        "MAXSIZE = 5MB, " +
            //        "FILEGROWTH = 10%)";

            //    SqlCommand myCommand = new SqlCommand(str, myConn);
            //    try
            //    {
            //        myConn.Open();
            //        myCommand.ExecuteNonQuery();
            //        MessageBox.Show("DataBase is Created Successfully", "MyProgram", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    }
            //    catch (System.Exception ex)
            //    {
            //        MessageBox.Show(ex.ToString(), "MyProgram", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    }
            //    finally
            //    {
            //        if (myConn.State == ConnectionState.Open)
            //        {
            //            myConn.Close();
            //        }
            //    }
            //}
        }

        private void menu_addArticle_Click(object sender, RoutedEventArgs e)
        {
            AddArticle addArticle = new AddArticle();
            addArticle.Show();
        }
    }
}
