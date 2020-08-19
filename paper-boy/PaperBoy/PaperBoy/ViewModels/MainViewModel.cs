using PaperBoy.Common;
using PaperBoy.Models.News;
using PaperBoy.Models;
using PaperBoy.Helpers;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using PaperBoy.Interfaces;
using PaperBoy.Data;
using PaperBoy.Extensions;

namespace PaperBoy.ViewModels
{
    public class MainViewModel:ObservableBase
    {
        private ObservableCollection<NewsInformation> _searchResult;
        public ObservableCollection<NewsInformation> SearchResult
        {
            get => _searchResult;
            set { SetProperty(ref this._searchResult, value); }
        }

        private ObservableCollection<NewsInformation> _technologyNews;
        public ObservableCollection<NewsInformation> TechnologyNews
        {
            get => _technologyNews;
            set { SetProperty(ref this._technologyNews,value); }
        }

        private ObservableCollection<NewsInformation> _trendingNews;
        public ObservableCollection<NewsInformation>TrendingNews
        {
            get => _trendingNews;
            set { SetProperty(ref this._trendingNews,value); }
        }

        private UserInformation _currentUser;
        public UserInformation CurrentUser
        {
            get => _currentUser;
            set { SetProperty(ref this._currentUser,value); }
        }

        private string _searchQuery;

        public string SearchQuery
        {
            get => _searchQuery;
            set => SetProperty(ref _searchQuery, value);
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set { SetProperty(ref this._isBusy, value); }
        }

        private string _platformLabel;
        public string PlatformLabel
        {
            get => _platformLabel;
            set { SetProperty(ref this._platformLabel,value); }
        }

        private string _extendedPlatformLabel;
        public string ExtendedPlatformLabel
        {
            get => _extendedPlatformLabel;
            set { SetProperty(ref this._extendedPlatformLabel, value); }
        }

        private DeviceOrientations _deviceOrientation;
        public DeviceOrientations DeviceOrientation
        {
            get => _deviceOrientation;
            set { SetProperty(ref this._deviceOrientation, value); }
        }

        private FavoritesCollection _favorites;
        public FavoritesCollection Favorites
        {
            get => _favorites;
            set { SetProperty(ref this._favorites, value); }
        }
        public MainViewModel()
        {
            SearchResult = new ObservableCollection<NewsInformation>();
            TechnologyNews = new ObservableCollection<NewsInformation>();
            TrendingNews = new ObservableCollection<NewsInformation>();

            Favorites = new FavoritesCollection();
            CurrentUser = new UserInformation()
            {
                DisplayName = "Ata Mahmoud",
                BioContent = "Egyptian Computer scienece student and Xamarin developer"
            };
        }

        public async void RefreshNewsAsync()
        {
            this.IsBusy = true;

            await RefreshTrendingNewsAsync();
            await RefreshTechnologyNewsAsync();

            this.IsBusy = false;
        }

        public async Task RefreshFavoritesAsync()
        {
            this.IsBusy = true;

            Favorites.Clear();

            var favorites = await FavoriteManager.DefaultManager.GetFavoritesAsync();

            foreach (var favorite in favorites)
            {
                this.Favorites.Add(favorite.AsFavorite("Technology"));
            }

            this.IsBusy = false;
        }
        public async Task RefreshTrendingNewsAsync()
        {
            TrendingNews.Clear();

            var trendingNews = await NewsHelper.GetTrendingAsync();
            
            foreach (var item in trendingNews)
            {
                TrendingNews.Add(item);
            }
        }

        public async Task RefreshTechnologyNewsAsync()
        {
            TechnologyNews.Clear();

            var technologyNews = await NewsHelper.GetByCategoryAsync(NewsCategoryType.ScienceAndTechnology);

            foreach (var item in technologyNews)
            {
                TechnologyNews.Add(item);
            }
        }

        public async Task RefreshSearchResultAsync()
        {
            this.IsBusy = true;

            SearchResult.Clear();

            string query = this.SearchQuery;
            var searchResults = await NewsHelper.GetSearchAsync(query);

            foreach (var item in searchResults)
            {
                SearchResult.Add(item);
            }

            this.IsBusy = false;
        }
    }
}
