// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CustomFrameRenderer.cs" company="ArcTouch LLC">
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
//   Defines the CustomFrameRenderer type.
// </summary>
//  --------------------------------------------------------------------------------------------------------------------
using System;
using System.ComponentModel;
using CoreGraphics;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Frame), typeof(CodeChallenge.iOS.Renderes.CustomFrameRenderer))]
namespace CodeChallenge.iOS.Renderes
{
    public class CustomFrameRenderer : FrameRenderer
    {
        public CustomFrameRenderer()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement == null)
                return;
            UpdateShadow();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == "Elevation")
            {
                UpdateShadow();
            }
        }

        private void UpdateShadow()
        {
            Layer.ShadowRadius = 5;
            Layer.ShadowColor = UIColor.Gray.CGColor;
            Layer.ShadowOffset = new CGSize(2, 2);
            Layer.ShadowOpacity = 0.80f;
            Layer.ShadowPath = UIBezierPath.FromRect(Layer.Bounds).CGPath;
            Layer.MasksToBounds = false;
        }
    }
}
