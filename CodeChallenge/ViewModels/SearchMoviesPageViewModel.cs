// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SearchMoviesPageViewModel.cs" company="ArcTouch LLC">
//   Copyright 2020 ArcTouch LLC.
//   All rights reserved.
//
//   This file, its contents, concepts, methods, behavior, and operation
//   (collectively the "Software") are protected by trade secret, patent,
//   and copyright laws. The use of the Software is governed by a license
//   agreement. Disclosure of the Software to third parties, in any form,
//   in whole or in part, is expressly prohibited except as authorized by
//   the license agreement.
// </copyright>
// <summary>
//   Defines the SearchMoviesPageViewModel type.
// </summary>
//  --------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CodeChallenge.Models;
using CodeChallenge.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace CodeChallenge.ViewModels
{
    public class SearchMoviesPageViewModel : BaseViewModel
    {
        // Variables to control of the pagination
        private int currentPage = 1;
        private int totalPage = 0;

        private readonly MovieService movieService = new MovieService();

        private string searchTerm;
        public string SearchTerm
        {
            get
            {
                return searchTerm;
            }
            set
            {
                SetProperty(ref searchTerm, value);
                SearchResults.Clear();
            }
        }

        public ObservableCollection<MovieItemViewModel> SearchResults { get; set; }

        public DelegateCommand SearchCommand { get; }
        public DelegateCommand<MovieItemViewModel> ShowMovieDetailCommand { get; }
        public DelegateCommand<MovieItemViewModel> ItemAppearingCommand { get; }

        private readonly INavigationService navigationService;
        private readonly IPageDialogService pageDialogService;
        public SearchMoviesPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
        {
            Title = "Search Movies";
            this.navigationService = navigationService;
            this.pageDialogService = pageDialogService;
            SearchResults = new ObservableCollection<MovieItemViewModel>();

            SearchCommand = new DelegateCommand(async () => await ExecuteSearchCommand().ConfigureAwait(false));
            ShowMovieDetailCommand = new DelegateCommand<MovieItemViewModel>(async (MovieItemViewModel movie) => await ExecuteShowMovieDetailCommand(movie).ConfigureAwait(false));
            ItemAppearingCommand = new DelegateCommand<MovieItemViewModel>(async (MovieItemViewModel movie) => await ExecuteItemAppearingCommand(movie).ConfigureAwait(false));
        }

        private async Task ExecuteSearchCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            try
            {
                SearchResults.Clear();
                currentPage = 1;
                await LoadAsync(currentPage).ConfigureAwait(true);
            }
            finally
            {
                IsBusy = false;
            }

            if (SearchResults.Count == 0)
            {
                await pageDialogService.DisplayAlertAsync("The Movie", "No results found.", "Ok").ConfigureAwait(false);
            }
        }

        private async Task ExecuteShowMovieDetailCommand(MovieItemViewModel movie)
        {
            var parameters = new NavigationParameters
            {
                { nameof(movie), movie }
            };
            await navigationService.NavigateAsync("MovieDetailPage", parameters).ConfigureAwait(false);
        }

        private async Task ExecuteItemAppearingCommand(MovieItemViewModel movie)
        {
            int itemLoadNextItem = 2;
            int viewCellIndex = SearchResults.IndexOf(movie);
            if (SearchResults.Count - itemLoadNextItem <= viewCellIndex)
            {
                await NextPageAsync().ConfigureAwait(false);
            }
        }

        private async Task NextPageAsync()
        {
            currentPage++;
            if (currentPage <= totalPage)
            {
                await LoadAsync(currentPage).ConfigureAwait(false);
            }
        }

        private async Task LoadAsync(int page)
        {
            UpcomingMoviesResponse upcomingMoviesResponse = await this.movieService.SearchMovies(searchTerm,page);

            totalPage = upcomingMoviesResponse.TotalPages;

            foreach (var movie in upcomingMoviesResponse.Results)
            {
                SearchResults.Add(ToMovieItemViewModel(movie));
            }
        }

        public Task OnDisappearing() => Task.CompletedTask;

        public MovieItemViewModel ToMovieItemViewModel(Movie result) => new MovieItemViewModel(result);
    }
}