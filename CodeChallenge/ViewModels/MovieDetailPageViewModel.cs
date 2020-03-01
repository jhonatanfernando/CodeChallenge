// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MovieDetailPageViewModel.cs" company="ArcTouch LLC">
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
//   Defines the MovieDetailPageViewModel type.
// </summary>
//  --------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.ObjectModel;
using Prism.Navigation;
using Image = CodeChallenge.Models.Image;

namespace CodeChallenge.ViewModels
{
    public class MovieDetailPageViewModel : BaseViewModel, INavigationAware
    {
        private MovieItemViewModel movie;
        public MovieItemViewModel Movie
        {
            get { return movie; }
            set { SetProperty(ref movie, value); }
        }

        
        private readonly INavigationService navigationService;

        public ObservableCollection<Image> Backdrops { get; set; } = new ObservableCollection<Image>();


        public MovieDetailPageViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }

        public async void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public async void OnNavigatedTo(INavigationParameters parameters)
        {
            Movie = parameters.GetValue<MovieItemViewModel>("movie");
            Title = Movie.Title;

            Backdrops.Clear();
            Backdrops.Add(new Image
            {
                FilePath = movie.BackdropPath
            });
        }
        
    }
}
