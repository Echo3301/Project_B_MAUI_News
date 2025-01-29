using MauiProjectBNews.Models;
using MauiProjectBNews.Views;

namespace MauiProjectBNews
{
    public partial class AppShell : Shell
    {
        List<NewsCategory> _newsCategories = Enum.GetValues<NewsCategory>().ToList();
        public AppShell()
        {
            InitializeComponent();

            foreach (var item in _newsCategories)
            {
                var shellC = new ShellContent()
                {
                    Title = item.ToString(),
                    ContentTemplate = new DataTemplate(() => new NewsPage(item))
                };
                this.Items.Add(shellC);
            }
        }
    }
}
