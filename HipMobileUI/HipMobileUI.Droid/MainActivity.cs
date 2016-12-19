﻿using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using de.upb.hip.mobile.pcl.Common;
using de.upb.hip.mobile.pcl.Common.Contracts;
using HipMobileUI.Droid.Contracts;
using HipMobileUI.Navigation;
using HipMobileUI.Pages;
using Microsoft.Practices.Unity;

namespace HipMobileUI.Droid
{
    [Activity(Label = "HipMobileUI", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            IoCManager.UnityContainer.RegisterType<IImageDimension, AndroidImageDimensions>();
            var mediaplayer = new AndroidMediaPlayer ();
            mediaplayer.Setup ();
            IoCManager.UnityContainer.RegisterInstance(typeof(IMediaPlayer), mediaplayer);

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            // Init Navigation
            NavigationService.Instance.RegisterViewModels(typeof(MainPage).Assembly);
            IoCManager.UnityContainer.RegisterInstance(typeof(INavigationService), NavigationService.Instance);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            Xamarin.FormsMaps.Init(this, bundle);
            LoadApplication(new App());
        }
    }
}

