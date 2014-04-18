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
    public partial class AddComment : Window
    {
        Effects effs = new Effects();
        DefaultValue dv = new DefaultValue();
        List<Commenter> commenters;
        List<Article> articles;

        public AddComment()
        {
            InitializeComponent();
            commenters = new List<Commenter>();
            articles = new List<Article>();
            UpdateLists();
        }

        private void comment_GotFocus(object sender, RoutedEventArgs e)
        {
            effs.TextBoxGotFocus(sender as TextBox, dv.DefaultComm);
        }
        private void comment_LostFocus(object sender, RoutedEventArgs e)
        {
            effs.TextBoxLostFocus(sender as TextBox, dv.DefaultComm);
        }
        private void comment_TextChanged(object sender, TextChangedEventArgs e)
        {
            effs.TextBoxTextChanged(sender as TextBox, symNumComment, dv.MaxLengthComment);
        }
        private void setDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (arcticle.SelectedItem != null)
            {
                DateTime dte = (arcticle.SelectedItem as Article).Date;
                effs.DatePickerSelectedDateChanged(sender as DatePicker, dv.DateStart);
            }
        }

        private void UpdateLists()
        {
            commenters.Clear();
            articles.Clear();
            FillCommentersList();
            FillArticlesList();
            nickName.ItemsSource = commenters;
            arcticle.ItemsSource = articles;
        }
        private void FillCommentersList()
        {
            string ConString = ConfigurationManager.ConnectionStrings["NewspaperDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                string strSQL = @"SELECT * FROM COMMENTERS";

                SqlCommand cmd = new SqlCommand(strSQL, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Articles");
                sda.Fill(dt);
                Commenter cmmntr;
                foreach (DataRow row in dt.Rows)
                {
                    cmmntr = new Commenter();
                    cmmntr.Id = Convert.ToInt32(row["COMMENTER_ID"]);
                    cmmntr.Name = row["NICKNAME"].ToString();
                    commenters.Add(cmmntr);
                }
            }
        }
        private void FillArticlesList()
        {
            string ConString = ConfigurationManager.ConnectionStrings["NewspaperDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                string strSQL = @"SELECT ART_ID, TITLE, TIME FROM ARTICLES";

                SqlCommand cmd = new SqlCommand(strSQL, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Articles");
                sda.Fill(dt);
                Article artcl;
                foreach (DataRow row in dt.Rows)
                {
                    artcl = new Article();
                    artcl.Id = Convert.ToInt32(row["ART_ID"]);
                    artcl.Title = row["TITLE"].ToString().Trim();
                    artcl.Date = Convert.ToDateTime(row["TIME"]);
                    articles.Add(artcl);
                }
            }
        }

        private void arcticle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (arcticle.SelectedItem != null)
            {
                dv.DateStart = (arcticle.SelectedItem as Article).Date;
                effs.DatePickerSelectedDateChanged(setDate, dv.DateStart);
            }
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            if (comment.Foreground == Brushes.Gray || nickName.SelectedItem == null || arcticle.SelectedItem == null
                || String.IsNullOrWhiteSpace(comment.Text))
            {
                MessageBox.Show("Заповніть всі поля.", "Увага");
                return;
            }

            string ConString = ConfigurationManager.ConnectionStrings["NewspaperDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                string strSQL = @"INSERT INTO COMMENTS (TIME, TEXT, COMMENTER_ID) "
                                    + "VALUES ((@TIME), (@TEXT), (@COMMENTER_ID))";
                SqlCommand cmd = new SqlCommand(strSQL, con);
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@TIME",
                    Value = setDate.SelectedDate,
                    SqlDbType = SqlDbType.SmallDateTime
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@TEXT",
                    Value = comment.Text.Trim(),
                    SqlDbType = SqlDbType.NVarChar
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@COMMENTER_ID",
                    Value = (nickName.SelectedItem as Commenter).Id,
                    SqlDbType = SqlDbType.Int
                });
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Articles");
                sda.Fill(dt);

                strSQL = @"SELECT IDENT_CURRENT('COMMENTS')";
                cmd = new SqlCommand(strSQL, con);
                sda = new SqlDataAdapter(cmd);
                dt = new DataTable("Articles");
                sda.Fill(dt);

                int commId = 0;
                commId = Convert.ToInt32(dt.Rows[0][0]);

                strSQL = @"INSERT INTO ARTICLES_COMMENTS (ART_ID, COMM_ID) "
                                    + "VALUES ((@ART_ID), (@COMM_ID))";
                cmd = new SqlCommand(strSQL, con);
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@ART_ID",
                    Value = (arcticle.SelectedItem as Article).Id,
                    SqlDbType = SqlDbType.Int
                });
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@COMM_ID",
                    Value = commId,
                    SqlDbType = SqlDbType.Int
                });
                sda = new SqlDataAdapter(cmd);
                dt = new DataTable("Articles");
                sda.Fill(dt);

            }
            MessageBox.Show("Коментар успішно збережено.", "Збереження");
            UpdateLists();
        }

        private void helpButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Додати коментар можливо, якщо заповнені всі поля (обраний автор коментаря, стаття, час та текст запису).\nТекст коментаря може містити лише 1000 символів.", "Довідка");
        }
    }
}