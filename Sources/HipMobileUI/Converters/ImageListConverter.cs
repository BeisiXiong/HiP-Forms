﻿// Copyright (C) 2016 History in Paderborn App - Universität Paderborn
//  
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//  
//      http://www.apache.org/licenses/LICENSE-2.0
//  
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections.ObjectModel;
using System.Globalization;
using Xamarin.Forms;

namespace PaderbornUniversity.SILab.Hip.Mobile.UI.Converters
{
    /// <summary>
    /// Converter for ItemTappedEventArgs to the tapped item
    /// </summary>
    public class ImageListConverter : IValueConverter
    {
        /// <summary>
        /// Converts the event args to the tapped item
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var list = (ObservableCollection<ImageSource>) value;
            var resultList = new ObservableCollection<View> ();

            foreach (var imageSource in list)
            {
                resultList.Add (new Image {Source = imageSource});
            }

            return resultList;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}