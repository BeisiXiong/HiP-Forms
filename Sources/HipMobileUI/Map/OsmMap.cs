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
// limitations under the License.using System;

using de.upb.hip.mobile.pcl.BusinessLayer.Models;
using Xamarin.Forms;

namespace HipMobileUI.Map {
    public class OsmMap : View {

        public static readonly BindableProperty ExhibitSetProperty =
            BindableProperty.Create ("ExhibitSet", typeof (ExhibitSet), typeof (OsmMap), null, propertyChanged: ExhibitPropertyChanged);

        public static readonly BindableProperty GpsLocationProperty = BindableProperty.Create ("GPSLocation",typeof(GeoLocation),typeof(OsmMap),null,propertyChanged: GpsLocationPropertyChanged);
        
        public static readonly BindableProperty DetailsRouteProperty = BindableProperty.Create ("DetailsRoute",typeof(Route),typeof(OsmMap),null, propertyChanged: DetailsRoutePropertyChanged);

        public static readonly BindableProperty ShowDetailsRouteProperty = BindableProperty.Create ("ShowDetailsRoute",typeof(bool),typeof(OsmMap),false);

        // Property accessor
        public ExhibitSet ExhibitSet {
            get { return (ExhibitSet) GetValue (ExhibitSetProperty); }
            set { SetValue (ExhibitSetProperty, value); }
        }

        public GeoLocation GpsLocation {
            get { return (GeoLocation) GetValue (GpsLocationProperty); }
            set {  SetValue (GpsLocationProperty,value);}
        }

        public Route DetailsRoute {
            get { return (Route) GetValue (DetailsRouteProperty); }
            set { SetValue (DetailsRouteProperty,value);}
        }

        public bool ShowDetailsRoute {
            get { return (bool) GetValue (ShowDetailsRouteProperty); }
            set { SetValue (ShowDetailsRouteProperty,value);}
        }


        // method listening for changes of the property
        private static void ExhibitPropertyChanged (BindableObject bindable, object oldValue, object newValue)
        {
            // check if the property really changed
            if (oldValue == null || !oldValue.Equals (newValue))
            {
                // inform all listeners that the ExhibitSet changed
                var map = (OsmMap) bindable;
                map.ExhibitSetChanged?.Invoke (map.ExhibitSet);
            }
        }

        private static void GpsLocationPropertyChanged (BindableObject bindable, object oldValue, object newValue)
        {
            if (oldValue == null || !oldValue.Equals (newValue))
            {
                var map = (OsmMap) bindable;
                map.GpsLocationChanged?.Invoke (map.GpsLocation);
            }
        }

        private static void DetailsRoutePropertyChanged (BindableObject bindable, object oldValue, object newValue)
        {
            if (oldValue == null || !oldValue.Equals (newValue))
            {
                var map = (OsmMap) bindable;
                map.DetailsRouteChanged?.Invoke (map.DetailsRoute);
            }
        }


        public delegate void ExhibitSetChangedHandler (ExhibitSet set);

        public event ExhibitSetChangedHandler ExhibitSetChanged;

        public delegate void GpslocationChangedHandler (GeoLocation location);

        public event GpslocationChangedHandler GpsLocationChanged;

        public delegate void DetailsRouteChangedHandler (Route route);

        public event DetailsRouteChangedHandler DetailsRouteChanged;



    }
}