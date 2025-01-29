using System.Web;

namespace MauiProjectBNews.Views
{
    public partial class ArticleView : ContentPage
    {
        public ArticleView()
        {
            InitializeComponent();
        }
        public ArticleView(string Url)
        {
            InitializeComponent();
            BindingContext = new UrlWebViewSource
            {
                Url = HttpUtility.UrlDecode(Url)
            };
        }
    }
}
