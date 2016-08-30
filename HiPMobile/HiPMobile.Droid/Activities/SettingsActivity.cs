// Copyright (C) 2016 History in Paderborn App - Universit�t Paderborn
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

using Android.App;
using Android.OS;
using de.upb.hip.mobile.droid.fragments;

namespace de.upb.hip.mobile.droid.Activities {

    [Activity (Label = "SettingsActivity")]
    public class SettingsActivity : Activity {

        protected override void OnCreate (Bundle savedInstanceState)
        {
            base.OnCreate (savedInstanceState);

            // add settings fragment
            FragmentManager.BeginTransaction ()
                           .Replace (Android.Resource.Id.Content, new SettingsFragment ())
                           .Commit ();
        }

    }
}