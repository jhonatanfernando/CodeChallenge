// --------------------------------------------------------------------------------------------------------------------
// <copyright file="App.xaml.cs" company="ArcTouch LLC">
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
//   Defines the App type.
// </summary>
//  --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using CodeChallenge.Models;
using CodeChallenge.Views;
using CodeChallenge.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Prism.DryIoc;
using Prism.Ioc;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace CodeChallenge
{
    public partial class App : PrismApplication
    {
        public static List<Genre> Genres { get; private set; }

        protected override void OnInitialized()
        {
            InitializeComponent();

            NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(HomePage)}");
        }

        protected override async void OnStart()
        {
            var genreResponse = await new MovieService().GetGenres();
            Genres = genreResponse.Genres;
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<HomePage>();
            containerRegistry.RegisterForNavigation<SearchMoviesPage>();
            containerRegistry.RegisterForNavigation<MovieDetailPage>();
        }

    }
}
