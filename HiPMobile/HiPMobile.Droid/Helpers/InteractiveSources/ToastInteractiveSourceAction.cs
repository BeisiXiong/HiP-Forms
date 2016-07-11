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

using Android.Content;
using Android.Widget;

namespace de.upb.hip.mobile.droid.Helpers.InteractiveSources {
    /// <summary>
    /// Implements IInteractiveSourceAction by displaying a Toast with 
    /// the source's text.
    /// </summary>
    public class ToastInteractiveSourceAction : IInteractiveSourceAction {

        private readonly Context context = null;

        /// <summary>
        /// Sets the context that is used to display the Toast.
        /// </summary>
        /// <param name="context"></param>
        public ToastInteractiveSourceAction (Context context)
        {
            this.context = context;
        }

        public void Display (string src)
        {
            Toast.MakeText (context, src, ToastLength.Long).Show ();
        }

    }
}