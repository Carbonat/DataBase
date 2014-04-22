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
    public partial class OpenArticle : Window
    {
        Effects effs = new Effects();
        DefaultValue dv = new DefaultValue();
        Article ar = new Article();
        
        public List<Comment> comments = new List<Comment>();
        public List<Comment> Comments
        {
            get
            {
                return comments;
            }
        }
        
        public OpenArticle(Article _ar)
        {
            InitializeComponent();
            ar = new Article(_ar);
            DataContext = this;
            UpdateListView();
        }

        public OpenArticle(int art_id)
        {
            InitializeComponent();
            ar = new Article();
            GetArticle(art_id);
            DataContext = this;
            UpdateListView();
        }

        public string FirstName
        {
            get { return ar.Creator.FirstName; }
        }
        public string LastName
        {
            get { return ar.Creator.LastName; }
        }
        public string Text
        {
            get { return ar.Text; }
        }
        public string TitleTb
        {
            get { return ar.Title; }
        }
        public string Date
        {
            get { return ar.Date.ToString("dd/MM/yyyy"); }
        }

        private void FillListFromDataSet()
        {
            string ConString = ConfigurationManager.ConnectionStrings["NewspaperDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                string strSQL = @"SELECT COMMENTERS.NICKNAME, COMMENTS.TEXT, COMMENTS.TIME "
                                    + "FROM COMMENTERS, COMMENTS, ARTICLES_COMMENTS "
                                    + "WHERE ART_ID=(@ART_ID) "
                                        + "AND ARTICLES_COMMENTS.COMM_ID = COMMENTS.COMM_ID "
                                        + "AND COMMENTS.COMMENTER_ID = COMMENTERS.COMMENTER_ID";

                SqlCommand cmd = new SqlCommand(strSQL, con);
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@ART_ID",
                    Value = ar.Id,
                    SqlDbType = SqlDbType.Int
                });
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Articles");
                sda.Fill(dt);
                Comment comm;
                foreach (DataRow row in dt.Rows)
                {
                    comm = new Comment();
                    comm.Comm = row["TEXT"].ToString().Trim();
                    comm.Commenter = row["NICKNAME"].ToString().Trim();
                    comm.Time = Convert.ToDateTime(row["TIME"]);

                    comments.Add(comm);
                }
            }
        }
        private void FillListView()
        {
            if (comments.Count == 0)
                return;

            for (int i = 0; i < comments.Count; ++i)
                commentsList.Items.Add(comments[i]);
        }
        private void UpdateListView()
        {
            commentsList.Items.Clear();
            comments.Clear();
            FillListFromDataSet();
            FillListView();
        }

        private void GetArticle(int art_id)
        {
            string ConString = ConfigurationManager.ConnectionStrings["NewspaperDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                string CmdString = "SELECT ARTICLES.ART_ID, ARTICLES.TITLE, ARTICLES.TEXT, ARTICLES.TIME, AUTHORS.AU_ID, AUTHORS.FIRST_NAME, AUTHORS.LAST_NAME FROM ARTICLES, AUTHORS, ARTICLES_AUTHORS WHERE ARTICLES.ART_ID = ARTICLES_AUTHORS.ART_ID AND ARTICLES_AUTHORS.AU_ID = AUTHORS.AU_ID AND ARTICLES.ART_ID = @ART_ID";
                SqlCommand cmd = new SqlCommand(CmdString, con);
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@ART_ID",
                    Value = art_id,
                    SqlDbType = SqlDbType.Int
                });
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Articles");
                sda.Fill(dt);

                ar.Id = Convert.ToInt32(dt.Rows[0]["ART_ID"]);
                ar.Title = dt.Rows[0]["TITLE"].ToString().Trim();
                ar.Creator = new Author { FirstName = dt.Rows[0]["FIRST_NAME"].ToString().Trim(), LastName = dt.Rows[0]["LAST_NAME"].ToString().Trim() };
                ar.Text = dt.Rows[0]["TEXT"].ToString().Trim();
                ar.Date = Convert.ToDateTime(dt.Rows[0]["TIME"]);
            }
        }

        private void helpButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Дане вікно призначене лише для перегляду статей.\nДля перегляду зміненої статті відкрийте її заново.", "Довідка");
        }
    }
}
