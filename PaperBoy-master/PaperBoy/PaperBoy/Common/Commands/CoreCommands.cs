using PaperBoy.Pages;
using PaperBoy.Models.News;
using PaperBoy.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using PaperBoy.Extensions;
namespace PaperBoy.Common.Commands
{
    public class NavigateToSettingsCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }
        public void RaiseCamExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
        public void Execute(object parameter)
        {
            NavigateAsync();
        }

        private async void NavigateAsync()
        {
            await App.MainNavigation.PushAsync(new Pages.SettingPage(),true);
        }
    }

    public class RefreshNewsCommand : ICommand
    {
        private bool _isBusy=false;

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return !_isBusy;
        }
        public void RaiseCanChange()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
        public void Execute(object parameter)
        {
            RefreshNewsAsync((string)parameter);
        }
        public async void RefreshNewsAsync(string newsType)
        {
            this._isBusy = true;
            RaiseCanChange();
            App.viewModel.IsBusy = true;

            switch(newsType)
            {
                case "Search":
                    await App.viewModel.RefreshSearchResultAsync();
                    break;
                case "Trending":
                    await App.viewModel.RefreshTrendingNewsAsync();
                    break;
                case "Technology":
                    await App.viewModel.RefreshTechnologyNewsAsync();
                    break;
                case "Favorites":
                    await App.viewModel.RefreshFavoritesAsync();
                    break;
            }
            this._isBusy = false;
            RaiseCanChange();
            App.viewModel.IsBusy = false;
        }
    }

    public class NavigateToDetailCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public void Execute(object parameter)
        {
            NavigateToDetailAsync(parameter as NewsInformation);
        }

        private async void NavigateToDetailAsync(NewsInformation article)
        {
            await App.MainNavigation.PushAsync(new ItemDetailPage(article),true);
        }
    }

    public class SpeakCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }
         public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
        public void Execute(object parameter)
        {
            GeneralHelper.Speak((string)parameter);
        }
    }

    public class ToggleFavoriteCommand : ICommand
    {
        private bool _isBusy=false;
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return !_isBusy;
        }
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
        public void Execute(object parameter)
        {
            ToggleFavoriteAsync(parameter as NewsInformation);
        }
        public async void ToggleFavoriteAsync(NewsInformation articel)
        {
           _isBusy = true;
            RaiseCanExecuteChanged();
            App.viewModel.IsBusy = true;

            await App.viewModel.Favorites.AddAsync(await articel.AsFavorite("Technology"));

           _isBusy = false;
            RaiseCanExecuteChanged();
            App.viewModel.IsBusy = false;
        }

    }
}
