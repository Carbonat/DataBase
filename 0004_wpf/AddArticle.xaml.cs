using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    public partial class AddArticle : Window
    {
        Effects effs = new Effects();
        DefaultValue dv = new DefaultValue();
        Article _article = new Article();
        List<Author> authors;
        List<string> titles;

        public AddArticle()
        {
            authors = new List<Author>();
            titles = new List<string>();
            InitializeComponent();
            this.DataContext = this;
            UpdateLists();
        }

        private void title_GotFocus(object sender, RoutedEventArgs e)
        {
            effs.TextBoxGotFocus(sender as TextBox, dv.DefaultTitle);
        }
        private void title_LostFocus(object sender, RoutedEventArgs e)
        {
            effs.TextBoxLostFocus(sender as TextBox, dv.DefaultTitle);
        }
        private void firstName_GotFocus(object sender, RoutedEventArgs e)
        {
            effs.TextBoxGotFocus(sender as TextBox, dv.DefaultFirstName);
        }
        private void firstName_LostFocus(object sender, RoutedEventArgs e)
        {
            effs.TextBoxLostFocus(sender as TextBox, dv.DefaultFirstName);
        }
        private void lastName_LostFocus(object sender, RoutedEventArgs e)
        {
            effs.TextBoxLostFocus(sender as TextBox, dv.DefaultLastName);
        }
        private void lastName_GotFocus(object sender, RoutedEventArgs e)
        {
            effs.TextBoxGotFocus(sender as TextBox, dv.DefaultLastName);
        }
        private void title_TextChanged(object sender, TextChangedEventArgs e)
        {
            effs.TextBoxTextChanged(sender as TextBox, symNumTitle, dv.MaxLengthTitle);
        }
        private void date_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            effs.DatePickerSelectedDateChanged(sender as DatePicker);
        }
        private void article_GotFocus(object sender, RoutedEventArgs e)
        {
            effs.TextBoxGotFocus(sender as TextBox, dv.DefaultArticle);
        }
        private void article_LostFocus(object sender, RoutedEventArgs e)
        {
            effs.TextBoxLostFocus(sender as TextBox, dv.DefaultArticle);
        }

        private void UpdateLists()
        {
            authors.Clear();
            titles.Clear();
            FillAuthorsList();
            FillTitlesList();
            FillAuthorsComboBox();
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
                Author auth;
                foreach (DataRow row in dt.Rows)
                {
                    auth = new Author();
                    auth.Id = Convert.ToInt32(row["AU_ID"]);
                    auth.FirstName = row["FIRST_NAME"].ToString();
                    auth.LastName = row["LAST_NAME"].ToString();
                    authors.Add(auth);
                }
            }
        }

        private void FillTitlesList()
        {
            string ConString = ConfigurationManager.ConnectionStrings["NewspaperDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                string strSQL = @"SELECT TITLE FROM ARTICLES";

                SqlCommand cmd = new SqlCommand(strSQL, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Articles");
                sda.Fill(dt);
                string ttl;
                foreach (DataRow row in dt.Rows)
                {
                    ttl = row["TITLE"].ToString().Trim().ToLower();
                    titles.Add(ttl);
                }
            }
        }

        private void FillAuthorsComboBox()
        {
            authorComboBox.ItemsSource = authors;
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            if (title.Foreground == Brushes.Gray || article.Foreground == Brushes.Gray || authorComboBox.SelectedItem == null ||
                String.IsNullOrWhiteSpace(title.Text) || String.IsNullOrWhiteSpace(article.Text))
            {
                MessageBox.Show("Заповніть всі поля", "Помилка");
                return;
            }
            int i = titles.IndexOf(title.Text.Trim().ToLower());
            if (i != -1)
            {
                MessageBox.Show("Стаття з даним заголовком вже існує", "Увага");
                return;
            }


            string ConString = ConfigurationManager.ConnectionStrings["NewspaperDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                string strSQL = @"INSERT INTO ARTICLES (TITLE, TEXT, TIME) "
                                    + "VALUES ((@TITLE), (@TEXT), (@TIME))";

                SqlCommand cmd = new SqlCommand(strSQL, con);
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@TITLE",
                    Value = title.Text.Trim(),
                    SqlDbType = SqlDbType.NVarChar
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@TEXT",
                    Value = article.Text.Trim(),
                    SqlDbType = SqlDbType.NVarChar
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@TIME",
                    Value = date.SelectedDate,
                    SqlDbType = SqlDbType.SmallDateTime
                });
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Articles");
                sda.Fill(dt);

                strSQL = @"SELECT IDENT_CURRENT('Articles')";
                cmd = new SqlCommand(strSQL, con);
                sda = new SqlDataAdapter(cmd);
                dt = new DataTable("Articles");
                sda.Fill(dt);

                int artId = 0;

                foreach (DataRow row in dt.Rows)
                    artId = Convert.ToInt32(row[0]);

                strSQL = @"INSERT INTO ARTICLES_AUTHORS (ART_ID, AU_ID) "
                                    + "VALUES ((@ART_ID), (@AU_ID))";
                cmd = new SqlCommand(strSQL, con);
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@ART_ID",
                    Value = artId,
                    SqlDbType = SqlDbType.Int
                });
                Author auth = authorComboBox.SelectedItem as Author;

                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@AU_ID",
                    Value = auth.Id,
                    SqlDbType = SqlDbType.Int
                });
                sda = new SqlDataAdapter(cmd);
                dt = new DataTable("Articles");
                sda.Fill(dt);
                MessageBox.Show("Статтю успішно збережено", "Додавання");
                UpdateLists();
            }
        }

        private void helpButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Для додавання статті необхідно ввести загаловок статті, обрати автора із випадаючого списку, визначити дату публікації та текст статті.\nДодати статтю із тим же заголовком заборонено.", "Довідка");
        }
    }
}
