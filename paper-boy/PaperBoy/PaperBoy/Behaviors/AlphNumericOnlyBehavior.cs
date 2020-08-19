using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace PaperBoy.Behaviors
{
    public class AlphNumericOnlyBehavior:Behavior<SearchBar>
    {
        protected override void OnAttachedTo(SearchBar searchBar)
        {
            searchBar.TextChanged += OnSearchBarTextChanged;
            base.OnAttachedTo(searchBar);
        }

        protected override void OnDetachingFrom(SearchBar searchBar)
        {
            searchBar.TextChanged -= OnSearchBarTextChanged;
            base.OnDetachingFrom(searchBar);
        }

        private void OnSearchBarTextChanged(object sender, TextChangedEventArgs e)
        {
            bool isVaild = Regex.IsMatch(e.NewTextValue, @"^[a-zA-Z0-9\s]+$");
            ((SearchBar) sender).TextColor = isVaild ? Color.Default : Color.Red;
        }
    }
}
