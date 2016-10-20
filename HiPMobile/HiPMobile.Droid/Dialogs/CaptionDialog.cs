﻿// Copyright (C) 2016 History in Paderborn App - Universität Paderborn
//  
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//  
//       http://www.apache.org/licenses/LICENSE-2.0
//  
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections.Generic;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Views;
using Android.Widget;
using de.upb.hip.mobile.droid.Adapters;
using de.upb.hip.mobile.droid.fragments;

namespace de.upb.hip.mobile.droid.Dialogs
{
    public class CaptionDialog : DialogFragment
    {
        private readonly Action<object, EventArgs> onCloseAction;
        private readonly List<Fragment> fragments;
        private readonly List<string> titles;

        public CaptionDialog(Action<object, EventArgs> onCloseAction, List<Fragment> fragments, List<string> titles)
        {
            this.onCloseAction = onCloseAction;
            this.fragments = fragments;
            this.titles = titles;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle);

            var view = inflater.Inflate(Resource.Layout.dialog_exhibit_details_caption, container);

            var viewPager = view.FindViewById<ViewPager>(Resource.Id.captionDialogViewPager);
            var adapter = new CaptionDialogFragmentTabsAdapter(ChildFragmentManager, fragments, titles);
            viewPager.Adapter = adapter;

            var tabLayout = view.FindViewById<TabLayout>(Resource.Id.captionDialogTabLayout);
            tabLayout.SetupWithViewPager(viewPager);

            var closeBtn = view.FindViewById<Button>(Resource.Id.captionDialogCloseButton);
            closeBtn.Click += (sender, args) =>
            {
                onCloseAction(sender, args);
                Dismiss();
            };

            return view;
        }

        public override void OnResume()
        {
            // Prevent dialogue from being too small
            var metrics = Resources.DisplayMetrics;
            var width = metrics.WidthPixels;
            var height = metrics.HeightPixels;
            Dialog.Window.SetLayout((6 * width) / 7, (4 * height) / 5);

            base.OnResume();
        }
    }
}