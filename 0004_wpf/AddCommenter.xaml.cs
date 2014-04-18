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
    public partial class AddCommenter : Window
    {
        DefaultValue dv = new DefaultValue();
        Effects effs = new Effects();
        List<string> commenters;

        public AddCommenter()
        {
            InitializeComponent();
            UpdateCommentersList();
        }

        private void nickName_GotFocus(object sender, RoutedEventArgs e)
        {
            effs.TextBoxGotFocus(sender as TextBox, dv.DefaultNickName);
        }
        private void nickName_LostFocus(object sender, RoutedEventArgs e)
        {
            effs.TextBoxLostFocus(sender as TextBox, dv.DefaultNickName);
        }
        private void nickName_TextChanged(object sender, TextChangedEventArgs e)
        {
            effs.TextBoxTextChanged(sender as TextBox, symNumNickName, dv.MaxLengthNickName);
        }

        private void UpdateCommentersList()
        {
            commenters = new List<string>();
            FillCommentsList();
        }
        private void FillCommentsList()
        {
            string ConString = ConfigurationManager.ConnectionStrings["NewspaperDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                string strSQL = @"SELECT NICKNAME FROM COMMENTERS";

                SqlCommand cmd = new SqlCommand(strSQL, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Articles");
                sda.Fill(dt);
                string cmmtr;
                foreach (DataRow row in dt.Rows)
                {
                    cmmtr = row["NICKNAME"].ToString().ToLower().Trim();
                    commenters.Add(cmmtr);
                }
            }
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            if (nickName.Foreground == Brushes.Gray || String.IsNullOrWhiteSpace(nickName.Text))
            {
                MessageBox.Show("Введіть ім'я коментатора", "Увага");
                return;
            }

            int i = commenters.IndexOf(nickName.Text.Trim().ToLower());
            if (i != -1)
            {
                MessageBox.Show("Коментатор з даним ім'ям вже існує.", "Увага");
                return;
            }

            string ConString = ConfigurationManager.ConnectionStrings["NewspaperDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                string strSQL = @"INSERT INTO COMMENTERS (NICKNAME) "
                                    + "VALUES (@NICKNAME)";

                SqlCommand cmd = new SqlCommand(strSQL, con);
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@NICKNAME",
                    Value = nickName.Text.Trim(),
                    SqlDbType = SqlDbType.NVarChar
                });
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Articles");
                sda.Fill(dt);

                MessageBox.Show("Коментатора успішно додано до бази.", "Додавання");
                UpdateCommentersList();
            }
        }

        private void helpButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Для додавання імені коментатора необхідно ввести його в поле.\nДовжина імені обмежена 50 символами.", "Довідка");
        }

    }
}
