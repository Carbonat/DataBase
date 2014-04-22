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
    public partial class Search : Window
    {
        public class Category
        {
            public int Id { get; set; }
            public string Str { get; set; }

            public override string ToString()
            {
                return Str;
            }
        }
        public class CommArt
        {
            public int Art_id { get; set; }
            public DateTime Time { get; set; }
            public string TitleOrComment { get; set; }
            public string AuthorOrCommenter { get; set; }
            public string Title { get; set; }
            public string DateString
            {
                get
                {
                    return Time.ToString("dd/MM/yyyy");
                }
            }
        }

        List<Category> whatFind = new List<Category>();
        DefaultValue dv = new DefaultValue();
        Effects effs = new Effects();
        List<CommArt> articlesComms = new List<CommArt>();
        OpenArticle openArticle;

        string SqlComArticle
        {
            get
            {
                return @"SELECT ARTICLES.ART_ID, ARTICLES.TITLE, ARTICLES.TIME, AUTHORS.FIRST_NAME, AUTHORS.LAST_NAME " + 
                        "FROM ARTICLES, AUTHORS, ARTICLES_AUTHORS " +
                        "WHERE ARTICLES.ART_ID=ARTICLES_AUTHORS.ART_ID AND " +
                            "ARTICLES_AUTHORS.AU_ID=AUTHORS.AU_ID";
            }
        }
        string SQLComComment
        {
            get
            {
                return @"SELECT COMMENTS.TIME, COMMENTERS.NICKNAME, COMMENTS.TEXT, ARTICLES.TITLE, ARTICLES_COMMENTS.ART_ID " +
                        "FROM COMMENTS, COMMENTERS, ARTICLES, ARTICLES_COMMENTS " +
                        "WHERE COMMENTS.COMMENTER_ID=COMMENTERS.COMMENTER_ID AND " +
                            "COMMENTS.COMM_ID=ARTICLES_COMMENTS.COMM_ID AND " +
                            "ARTICLES_COMMENTS.ART_ID=ARTICLES.ART_ID";
            }
        }

        public Search()
        {
            InitializeComponent();
            FillListCategory();
            whatFindCB.ItemsSource = whatFind;
        }

        private void FillListCategory()
        {
            whatFind.Clear();
            List<string> ctgries = new List<string>() { "Автор статті", "Заголовок статті", "Текст статті", "Коментатор", "Коментарі", "Дата" };
            Category cat;
            for (int i = 0; i < ctgries.Count; ++i)
            {
                cat = new Category();
                cat.Id = i;
                cat.Str = ctgries[i];
                whatFind.Add(cat);
            }
        }

        private void FillCommentListView(DataTable dt)
        {
            List<CommArt> commArtList = new List<CommArt>();
            CommArt commArt;
            foreach (DataRow row in dt.Rows)
            {
                commArt = new CommArt();
                commArt.Art_id = Convert.ToInt32(row["ART_ID"]);
                commArt.Title = row["TITLE"].ToString().Trim();
                commArt.Time = Convert.ToDateTime(row["TIME"]);
                commArt.AuthorOrCommenter = row["NICKNAME"].ToString();
                commArt.TitleOrComment = row["TEXT"].ToString().Trim();
                commArtList.Add(commArt);
            }

            if(commArtList.Count < 1)
            {
                MessageBox.Show("Нічого не знайдено.", "Результат пошуку");
                return;
            }

            GridView myGridView = new GridView();

            GridViewColumn gvc1 = new GridViewColumn();
            gvc1.DisplayMemberBinding = new Binding("DateString");
            gvc1.Header = "Дата";
            gvc1.Width = 70;
            myGridView.Columns.Add(gvc1);
            GridViewColumn gvc2 = new GridViewColumn();
            gvc2.DisplayMemberBinding = new Binding("AuthorOrCommenter");
            gvc2.Header = "Коментатор";
            myGridView.Columns.Add(gvc2);
            GridViewColumn gvc3 = new GridViewColumn();
            gvc3.DisplayMemberBinding = new Binding("TitleOrComment");
            gvc3.Header = "Коментар";
            myGridView.Columns.Add(gvc3);
            GridViewColumn gvc4 = new GridViewColumn();
            gvc4.DisplayMemberBinding = new Binding("Title");
            gvc4.Header = "Заголовок";
            myGridView.Columns.Add(gvc4);
            resListView.View = myGridView;

            resListView.ItemsSource = commArtList;

        }
        private void FillArticleListView(DataTable dt)
        {
            List<Article> articles = new List<Article>();
            Article ar;
            foreach (DataRow row in dt.Rows)
            {
                ar = new Article();
                ar.Id = Convert.ToInt32(row["ART_ID"]);
                ar.Title = row["TITLE"].ToString().Trim();
                ar.Creator = new Author { FirstName = row["FIRST_NAME"].ToString().Trim(), LastName = row["LAST_NAME"].ToString().Trim() };
                ar.Date = Convert.ToDateTime(row["TIME"]);
                articles.Add(ar);
            }

            if(articles.Count < 1)
            {
                MessageBox.Show("Нічого не знайдено.", "Результат пошуку");
                return;
            }


            GridView myGridView = new GridView();

            GridViewColumn gvc1 = new GridViewColumn();
            gvc1.DisplayMemberBinding = new Binding("DateString");
            gvc1.Header = "Дата";
            gvc1.Width = 70;
            myGridView.Columns.Add(gvc1);
            GridViewColumn gvc2 = new GridViewColumn();
            gvc2.DisplayMemberBinding = new Binding("Title");
            gvc2.Header = "Заголовок";
            myGridView.Columns.Add(gvc2);
            GridViewColumn gvc3 = new GridViewColumn();
            gvc3.DisplayMemberBinding = new Binding("ReturnCreatorName");
            gvc3.Header = "Автор";
            myGridView.Columns.Add(gvc3);

            resListView.View = myGridView;

            resListView.ItemsSource = articles;
        }

        private void FillDateListView(DataTable dt, bool isArticle)
        {
            if(isArticle)
            {
                CommArt commArt;
                foreach (DataRow row in dt.Rows)
                {
                    commArt = new CommArt();
                    commArt.Time = Convert.ToDateTime(row["TIME"]);
                    commArt.Art_id = Convert.ToInt32(row["ART_ID"]);
                    commArt.TitleOrComment = row["TITLE"].ToString().Trim();
                    commArt.AuthorOrCommenter = row["LAST_NAME"].ToString().Trim() + " " + row["FIRST_NAME"].ToString().Trim();
                    articlesComms.Add(commArt);
                }
                return;
            }

            CommArt comm;
            foreach (DataRow row in dt.Rows)
            {
                comm = new CommArt();
                comm.Art_id = Convert.ToInt32(row["ART_ID"]);
                comm.AuthorOrCommenter = row["NICKNAME"].ToString().Trim();
                comm.TitleOrComment = row["TEXT"].ToString().Trim();
                comm.Time = Convert.ToDateTime(row["TIME"]);
                articlesComms.Add(comm);
            }

            if (articlesComms.Count < 1)
            {
                MessageBox.Show("Нічого не знайдено.", "Результат пошуку");
                return;
            }

            GridView myGridView = new GridView();

            GridViewColumn gvc1 = new GridViewColumn();
            gvc1.DisplayMemberBinding = new Binding("DateString");
            gvc1.Header = "Дата";
            gvc1.Width = 70;
            myGridView.Columns.Add(gvc1);
            GridViewColumn gvc2 = new GridViewColumn();
            gvc2.DisplayMemberBinding = new Binding("TitleOrComment");
            gvc2.Header = "Заголовок / коментар";
            myGridView.Columns.Add(gvc2);
            GridViewColumn gvc3 = new GridViewColumn();
            gvc3.DisplayMemberBinding = new Binding("AuthorOrCommenter");
            gvc3.Header = "Автор / коментатор";
            myGridView.Columns.Add(gvc3);

            resListView.View = myGridView;

            resListView.ItemsSource = articlesComms;
        }

        private void queryTB_GotFocus(object sender, RoutedEventArgs e)
        {
            effs.TextBoxGotFocus(sender as TextBox, dv.DefaultFindQuery);
        }
        private void queryTB_LostFocus(object sender, RoutedEventArgs e)
        {
            effs.TextBoxLostFocus(sender as TextBox, dv.DefaultFindQuery);
        }

        private void FindAuthor(string first_name, string last_name)
        {
            string ConString = ConfigurationManager.ConnectionStrings["NewspaperDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                string strSQL = SqlComArticle;
                bool isFN = !String.Equals(first_name, "*");
                bool isLN = !String.Equals(last_name, "*");
                if(isFN)
                    strSQL += " AND AUTHORS.FIRST_NAME=@FIRST_NAME";
                if(isLN)
                    strSQL += " AND AUTHORS.LAST_NAME=@LAST_NAME";
                SqlCommand cmd = new SqlCommand(strSQL, con);
                if(isFN)
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "@FIRST_NAME",
                        Value = first_name,
                        SqlDbType = SqlDbType.NVarChar
                    });
                if(isLN)
                    cmd.Parameters.Add(new SqlParameter
                    {
                        ParameterName = "@LAST_NAME",
                        Value = last_name,
                        SqlDbType = SqlDbType.NVarChar
                    });
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Articles");
                sda.Fill(dt);

                FillArticleListView(dt);
            }
        }

        private void FindTitleOrText(string str, int id)
        {
            string ConString = ConfigurationManager.ConnectionStrings["NewspaperDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                string strSQL = SqlComArticle;
                if(id == 1) // TITLE
                    strSQL += " AND ARTICLES.TITLE LIKE @STR";
                else if(id == 2) // TEXT
                    strSQL += " AND ARTICLES.TEXT LIKE @STR";
                else if(id == 3) // Commenter
                    strSQL = SQLComComment + " AND COMMENTERS.NICKNAME LIKE @STR";
                else if(id == 4) // Comment
                    strSQL = SQLComComment + " AND COMMENTS.TEXT LIKE @STR";
                SqlCommand cmd = new SqlCommand(strSQL, con);
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@STR",
                    Value = "%"+ str + "%",
                    SqlDbType = SqlDbType.NVarChar
                });
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Articles");
                sda.Fill(dt);
                if (id == 1 || id == 2)
                    FillArticleListView(dt);
                else
                    FillCommentListView(dt);
            }
        }

        private void FindDate(DateTime date)
        {
            articlesComms.Clear();
            string ConString = ConfigurationManager.ConnectionStrings["NewspaperDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                string strSQL = SqlComArticle + " AND ARTICLES.TIME = @STR";

                SqlCommand cmd = new SqlCommand(strSQL, con);
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@STR",
                    Value = date,
                    SqlDbType = SqlDbType.SmallDateTime
                });
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Articles");
                sda.Fill(dt);
                FillDateListView(dt, true);

                strSQL = SQLComComment + " AND COMMENTS.TIME = @STR";
                cmd = new SqlCommand(strSQL, con);
                cmd.Parameters.Add(new SqlParameter
                {
                    ParameterName = "@STR",
                    Value = date,
                    SqlDbType = SqlDbType.SmallDateTime
                });
                sda = new SqlDataAdapter(cmd);
                dt = new DataTable("Articles");
                sda.Fill(dt);
                FillDateListView(dt, false);
            }
        }

        private void findButton_Click(object sender, RoutedEventArgs e)
        {
            if (queryTB.Foreground == Brushes.Gray || String.IsNullOrWhiteSpace(queryTB.Text)
                    || whatFindCB.SelectedItem == null)
            {
                MessageBox.Show("Заповніть всі поля", "Увага");
                return;
            }

            Category cat = whatFindCB.SelectedItem as Category;
            if (cat.Id == 5)// category "Date"
            {
                try
                {
                    DateTime date = Convert.ToDateTime(queryTB.Text);
                    FindDate(date);
                }
                catch (FormatException ex)
                {
                    MessageBox.Show("Дату задано неправильно!\nФормат дати: дд/мм/рррр", "Увага");
                    return;
                }
            }
            else
            {
                string query = queryTB.Text.Trim().ToLower();
                if (cat.Id == 0)// category "Author"
                {
                    string[] str = query.Split(' ');
                    if (str.Length < 2)
                    {
                        MessageBox.Show("Введіть ім'я та прізвище автора", "Увага");
                        return;
                    }
                    string first_name = str[0], last_name = str[1];
                    FindAuthor(first_name, last_name);
                }
                else
                    FindTitleOrText(query, cat.Id);
            }
        }

        private void helpButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Для пошуку обиріть критерій з випадаючого списку.\nДля пошуку автора необхідно ввести його ім'я та прізвище через пробіл. У разі, якщо ви не достаменно не знаєте його імені або прізвища, замість них можете ввести \"*\" або \"* *\" якщо ви хочете вивести весь список.", "Довідка"); 
        }

        private void resListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            CommArt commArt = resListView.SelectedItem as CommArt;
            if (commArt == null)
            {
                Article art = resListView.SelectedItem as Article;
                openArticle = new OpenArticle(art.Id);
                openArticle.Show();
            }
            else
            {
                openArticle = new OpenArticle(commArt.Art_id);
                openArticle.Show();
            }
        }
    }
}