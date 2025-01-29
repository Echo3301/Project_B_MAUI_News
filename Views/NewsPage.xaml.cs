using MauiProjectBNews.Models;
using MauiProjectBNews.Services;
using System;
using System.Diagnostics;
using System.Linq;

namespace MauiProjectBNews.Views;

public partial class NewsPage : ContentPage
{
    //NewsService to fetch news data
    NewsService _service;

    //List of news categories converted from enum
    List<NewsCategory> _newsCategories = Enum.GetValues<NewsCategory>().ToList();

    //Tracker for category index
    int i = 0;

    //Construktor for NewsPage
    public NewsPage(NewsCategory _category)
	{
        InitializeComponent();
        //Sets category index based on the last passed category
        i = _newsCategories.IndexOf(_category);

        //Initialize the NewsService instance to fetch news data
        _service = new NewsService();

    }
    //Overrides the method to set page title and load news when the page appears
    protected async override void OnAppearing()
    {
        base.OnAppearing();
        Title = $"News for {_newsCategories[i].ToString()}";
        await LoadNews();
    }

    //Button clicked event handler to refresh news
    private async void Button_Clicked(object sender, EventArgs e)
    {
        try
        {
            await LoadNews();
            StatusLabel.Text = "Forecast refreshed successfully";

            await Task.Delay(1000);
            StatusLabel.Text = "Refresh";
        }
        catch (Exception ex)
        {
            //If error occurs, loggs it and show an aleart to the user
            Debug.WriteLine($"An error has occurred: {ex.Message}");
            await DisplayAlert("Error", "An error has occurred when news tried to refrech", "OK");
        }
    }
    //Event handler when a news artical is tapped
    private async void News_Clicked(object sender, EventArgs d)
    {
        try
        {
            //Gets the tapped news artical from the senders binding context
            var tappedNews = (sender as ViewCell).BindingContext as NewsArticle;

            if (tappedNews != null)
            {
                string url = tappedNews.Url;
                await Navigation.PushAsync(new ArticleView(url));
            }
            else
            {
                //If the artical is null, displays an error alert
                await DisplayAlert("Error", "An error has occurred when news artical tried to load", "OK");
            }
        }
        catch (Exception ex)
        {
            //If error occurs, loggs it and show an aleart to the user
            Debug.WriteLine($"An error has occurred {ex.Message}");
            await DisplayAlert("Error", "An error has occurred when news artical tried to load", "OK");
        }
    }
    //Method to load the news based on the choosen category
    private async Task LoadNews()
    {
        try
        {
            //Fetched news based on the choosen category
            var response = await _service.GetNewsAsync(_newsCategories[i]);

            if (response != null && response.Articles != null)
            {
            var NewResponse = response.Articles;

            newsListView.ItemsSource = NewResponse;
            }
            else
            {
                //If data is not found, displays an error alert
                await DisplayAlert("Error", "No data found", "OK");
            }
        }
        catch (Exception ex)
        {
            //If error occurs, loggs it and show an aleart to the user
            Debug.WriteLine($"An error has occurred {ex.Message}");
            await DisplayAlert("Error", "An error has occurred when news tried to load", "OK");            
        }
    }
}