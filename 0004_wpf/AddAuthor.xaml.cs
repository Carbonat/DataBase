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
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace _0004_wpf
{
    public partial class AddAuthor : Window
    {
        public class Pair<T, K>
        {
            public T First { get; set; }
            public K Last { get; set; }
        }
        Effects effs = new Effects();
        DefaultValue dv = new DefaultValue();
        List<Pair<string, string>> authors;


        public AddAuthor()
        {
            InitializeComponent();
            authors = new List<Pair<string, string>>();
            UpdateAuthorsList();
        }

        private void authorFirstName_GotFocus(object sender, RoutedEventArgs e)
        {
            effs.TextBoxGotFocus(sender as TextBox, dv.DefaultFirstName);
        }
        private void authorSecondName_GotFocus(object sender, RoutedEventArgs e)
        {
            effs.TextBoxGotFocus(sender as TextBox, dv.DefaultLastName);
        }
        private void authorSecondName_LostFocus(object sender, RoutedEventArgs e)
        {
            effs.TextBoxLostFocus(sender as TextBox, dv.DefaultLastName);
        }
        private void authorFirstName_LostFocus(object sender, RoutedEventArgs e)
        {
            effs.TextBoxLostFocus(sender as TextBox, dv.DefaultFirstName);
        }

        private void UpdateAuthorsList()
        {
            authors.Clear();
            FillAuthorsList();
        }

        private void FillAuthorsList()
        {
            string ConString = ConfigurationManager.ConnectionStrings["NewspaperDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                string strSQL = @"SELECT AU_ID, FIRST_NAME, LAST_NAME FROM AUTHORS";

                SqlCommand cmd = new SqlCommand(strSQL, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Articles");
                sda.Fill(dt);
                Pair<string, string> auth;
                foreach (DataRow row in dt.Rows)
                {
                    auth = new Pair<string, string>();
                    auth.First = row["FIRST_NAME"].ToString().ToLower().Trim();
                    auth.Last = row["LAST_NAME"].ToString().ToLower().Trim();

                    authors.Add(auth);
                }
            }
        }

        private bool IsListHas(Pair<string, string> n)
        {
            foreach (Pair<string, string> p in authors)
            {
                if (String.Equals(p.First, n.First) && String.Equals(p.Last, n.Last))
                    return true;
            }

            return false;
        }
        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            if (authorFirstName.Foreground == Brushes.Gray || authorLastName.Foreground == Brushes.Gray)
            {
                MessageBox.Show("Введіть ім'я та прізвище автора", "Увага");
                return;
            }
            if (string.IsNullOrWhiteSpace(authorFirstName.Text) || string.IsNullOrWhiteSpace(authorLastName.Text))
            {
                MessageBox.Show("Введено помилкові данні", "Помилка");
                return;
            }
            Pair<string, string> name = new Pair<string, string>();
            Pair<string, string> temp_name = new Pair<string, string>();
            
            name.First = authorFirstName.Text.Trim();
            name.Last = authorLastName.Text.Trim();
            temp_name.First = name.First.ToLower();
            temp_name.Last = name.Last.ToLower();

            bool j = IsListHas(temp_name);

            if (j)
            {
                MessageBox.Show("Даний автор вже присутній у базі", "Увага");
                return;
            }

            string ConString = ConfigurationManager.ConnectionStrings["NewspaperDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                string strSQL = @"INSERT INTO AUTHORS (FIRST_NAME, LAST_NAME) "
                                    + "VALUES ((@FIRST_NAME), (@LAST_NAME))";

                SqlCommand cmd = new SqlCommand(strSQL, con);
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@FIRST_NAME",
                    Value = name.First,
                    SqlDbType = SqlDbType.NVarChar
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@LAST_NAME",
                    Value = name.Last,
                    SqlDbType = SqlDbType.NVarChar
                });
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Articles");
                sda.Fill(dt);
            }
            MessageBox.Show("Автора успішно додано до бази", "Збереження");
            UpdateAuthorsList();
        }

        private void authorFirstName_TextChanged(object sender, TextChangedEventArgs e)
        {
            effs.TextBoxTextChanged(sender as TextBox, symNumFirstName, dv.MaxLengthAuthName);
        }

        private void authorLastName_TextChanged(object sender, TextChangedEventArgs e)
        {
            effs.TextBoxTextChanged(sender as TextBox, symNumLastName, dv.MaxLengthAuthName);
        }

        private void helpButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Дане вікно призначене для додавання нового автора статей.\nКількість символів кожного поля обмежена.", "Довідка");
        }
    }
}
