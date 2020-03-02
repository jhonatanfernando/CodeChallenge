﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MovieItemViewModel.cs" company="ArcTouch LLC">
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
//   Defines the MovieItemViewModel type.
// </summary>
//  --------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using CodeChallenge.Models;

namespace CodeChallenge.ViewModels
{
    public class MovieItemViewModel : INotifyPropertyChanged
    {
        private string posterPath;
        private string backdropPath;


        public MovieItemViewModel(Movie movie)
        {
            Title = movie.Title;
            OriginalTitle = movie.OriginalTitle;
            Overview = movie.Overview;
            VoteAverage = movie.VoteAverage;
            PosterPath = Utils.MovieImageUrlBuilder.BuildPosterUrl(movie.PosterPath);
            BackdropPath = Utils.MovieImageUrlBuilder.BuildPosterUrl(movie.BackdropPath);
            ReleaseDate = movie.ReleaseDate;
            Genres = string.Join(", ", movie.GenreIds.Select(m => App.Genres?.First(g => g.Id == m)?.Name));
        }

        public string Title { get; set; }

        public string OriginalTitle { get; set; }

        public string Overview { get; set; }

        public float VoteAverage { get; set; }

        public string PosterPath { get => this.posterPath; set => SetProperty(ref this.posterPath, value); }

        public string BackdropPath { get => this.backdropPath; set => SetProperty(ref this.backdropPath, value); }

        public DateTimeOffset? ReleaseDate { get; set; }

        public string Genres { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
            {
                return false;
            }

            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
