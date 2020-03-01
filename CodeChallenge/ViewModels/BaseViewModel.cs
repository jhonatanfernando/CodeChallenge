// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseViewModel.cs" company="ArcTouch LLC">
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
//   Defines the BaseViewModel type.
// </summary>
//  --------------------------------------------------------------------------------------------------------------------
using System;
using Prism.Mvvm;
using Prism.Services;

namespace CodeChallenge.ViewModels
{
    public class BaseViewModel : BindableBase
    {
        /// <summary>
        /// Private backing field to hold the busy.
        /// </summary>
        bool isBusy = false;
        /// <summary>
        /// Public property to set and get the state as busy.
        /// </summary>
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        /// <summary>
        /// Private backing field to hold the title.
        /// </summary>
        string title = string.Empty;
        /// <summary>
        /// Public property to set and get the title of the item.
        /// </summary>
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }
    }
}
