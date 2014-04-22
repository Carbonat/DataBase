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
using System.Data;
using System.Configuration;

namespace _0004_wpf
{
    public partial class MainWindow : Window
    {
        AddArticle addArticle;
        OpenArticle openArticle;
        AddAuthor addAuthor;
        AddCommenter addCommenter;
        AddComment addComment;
        Search search;

        List<Article> articles;

        public MainWindow()
        {
            InitializeComponent();

            articles = new List<Article>();

            UpdateListView();
        }

        private void menu_addArticle_Click(object sender, RoutedEventArgs e)
        {
            addArticle = new AddArticle();
            addArticle.Show();
        }
        private void menu_addAuthor_Click(object sender, RoutedEventArgs e)
        {
            addAuthor = new AddAuthor();
            addAuthor.Show();
        }
        private void menu_addCommenter_Click(object sender, RoutedEventArgs e)
        {
            addCommenter = new AddCommenter();
            addCommenter.Show();
        }
        private void menu_addComment_Click(object sender, RoutedEventArgs e)
        {
            addComment = new AddComment();
            addComment.Show();
        }
        private void FillListFromDataSet()
        {
            string ConString = ConfigurationManager.ConnectionStrings["NewspaperDB"].ConnectionString;
            string CmdString = string.Empty;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                CmdString = "SELECT ARTICLES.ART_ID, ARTICLES.TITLE, ARTICLES.TEXT, ARTICLES.TIME, AUTHORS.AU_ID, AUTHORS.FIRST_NAME, AUTHORS.LAST_NAME FROM ARTICLES, AUTHORS, ARTICLES_AUTHORS WHERE ARTICLES.ART_ID = ARTICLES_AUTHORS.ART_ID AND ARTICLES_AUTHORS.AU_ID = AUTHORS.AU_ID";
                SqlCommand cmd = new SqlCommand(CmdString, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Articles");
                sda.Fill(dt);
                Article ar;
                foreach (DataRow row in dt.Rows)
                {
                    ar = new Article();
                    ar.Id = Convert.ToInt32(row["ART_ID"]);
                    ar.Title = row["TITLE"].ToString().Trim();
                    ar.Creator = new Author { FirstName = row["FIRST_NAME"].ToString().Trim(), LastName = row["LAST_NAME"].ToString().Trim() };
                    ar.Text = row["TEXT"].ToString().Trim();
                    ar.Date = Convert.ToDateTime(row["TIME"]);
                    articles.Add(ar);
                }
            }
        }
        private void FillListView()
        {
            if (articles.Count == 0)
                return;
            
            for (int i = 0; i < articles.Count; ++i)
            {
                articlesListView.Items.Add(articles[i]);
            }
        }

        private void UpdateListView()
        {
            articlesListView.Items.Clear();
            articles.Clear();
            FillListFromDataSet();
            FillListView();
        }

        private void articlesListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListView lv = sender as ListView;
            Article ar = lv.SelectedItem as Article;
            openArticle = new OpenArticle(ar);
            openArticle.Show();
        }

        private void menu_find_Click(object sender, RoutedEventArgs e)
        {
            search = new Search();
            search.Show();
        }
    }
}
