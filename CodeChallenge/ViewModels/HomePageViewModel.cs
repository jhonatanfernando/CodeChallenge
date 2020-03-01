// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HomePageViewModel.cs" company="ArcTouch LLC">
//   Copyright 2019 ArcTouch LLC.
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
//   Defines the HomePageViewModel type.
// </summary>
//  --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CodeChallenge.Models;
using CodeChallenge.Services;
using CodeChallenge.Views;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;

namespace CodeChallenge.ViewModels
{
    public class HomePageViewModel : BaseViewModel
    {
        // Variables to control of the pagination
        private int currentPage = 1;
        private int totalPage = 0;


        private readonly MovieService movieService = new MovieService();
        private ObservableCollection<MovieItemViewModel> movies;


        public DelegateCommand<MovieItemViewModel> ShowMovieDetailCommand { get; }
        public DelegateCommand<MovieItemViewModel> ItemAppearingCommand { get; }


        private readonly INavigationService navigationService;
        public HomePageViewModel(INavigationService navigationService)
        {
            this.movies = new ObservableCollection<MovieItemViewModel>();
            this.navigationService = navigationService;

            ShowMovieDetailCommand = new DelegateCommand<MovieItemViewModel>(async (MovieItemViewModel movie) => await ExecuteShowMovieDetailCommand(movie).ConfigureAwait(false));
            ItemAppearingCommand = new DelegateCommand<MovieItemViewModel>(async (MovieItemViewModel movie) => await ExecuteItemAppearingCommand(movie).ConfigureAwait(false));

        }

        public ObservableCollection<MovieItemViewModel> Movies
        {
            get => this.movies;
            set => SetProperty(ref this.movies, value);
        }

        public async Task OnAppearing()
        {
            UpcomingMoviesResponse upcomingMoviesResponse = await this.movieService.UpcomingMovies(1);

            totalPage = upcomingMoviesResponse.TotalPages;

            foreach (var movie in upcomingMoviesResponse.Results)
            {
                Movies.Add(ToMovieItemViewModel(movie));
            } 
        }

        private async Task ExecuteShowMovieDetailCommand(MovieItemViewModel movie)
        {
            var parameters = new NavigationParameters
            {
                { nameof(movie), movie }
            };

            await navigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(MovieDetailPage)}", parameters).ConfigureAwait(false);

            //await navigationService.NavigateAsync("NavigationPage/MovieDetailPage", parameters).ConfigureAwait(false);
        }

        private async Task ExecuteItemAppearingCommand(MovieItemViewModel movie)
        {
            int itemLoadNextItem = 2;
            int viewCellIndex = Movies.IndexOf(movie);
            if (Movies.Count - itemLoadNextItem <= viewCellIndex)
            {
                await NextPageUpcomingMoviesAsync().ConfigureAwait(false);
            }
        }

        public async Task NextPageUpcomingMoviesAsync()
        {
            currentPage++;
            if (currentPage <= totalPage)
            {
                UpcomingMoviesResponse upcomingMoviesResponse = await this.movieService.UpcomingMovies(currentPage);
                foreach (var movie in upcomingMoviesResponse.Results)
                {
                    Movies.Add(ToMovieItemViewModel(movie));
                }
            }
        }

        public Task OnDisappearing() => Task.CompletedTask;

        public MovieItemViewModel ToMovieItemViewModel(Movie result) => new MovieItemViewModel(result);
    }
}
