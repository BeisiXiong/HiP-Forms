﻿using System.Collections.ObjectModel;
using de.upb.hip.mobile.pcl.BusinessLayer.Models;
using HipMobileUI.Helpers;
using Xamarin.Forms;

namespace HipMobileUI.Views
{
    /// <summary>
    /// Extension of a <see cref="StackLayout"/> to display a list of <see cref="RouteTags"/>.
    /// </summary>
    class RouteTagsView : StackLayout
    {
        public static readonly BindableProperty RouteTagsProperty = BindableProperty.Create(
            nameof(RouteTags), 
            typeof(ObservableCollection<RouteTag>), 
            typeof(RouteTagsView), 
            propertyChanged: RouteTagsPropertyChanged
        );

        /// <summary>
        /// Should be bound to the property of the viewmodel providing the route tags.
        /// </summary>
        public ObservableCollection<RouteTag> RouteTags
        {
            get { return (ObservableCollection<RouteTag>)GetValue(RouteTagsProperty); }
            set { SetValue(RouteTagsProperty, value); }
        }

        /// <summary>
        /// Called if the property <see cref="RouteTags"/> is set. 
        /// Clears the layout and adds a new entry for each route tag.
        /// </summary>
        /// <param name="bindable"></param>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        private static void RouteTagsPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var layout = bindable as RouteTagsView;
            var routeTags = newValue as ObservableCollection<RouteTag>;
            if (routeTags != null && layout != null)
            {
                layout.Children.Clear();
                foreach (var tag in routeTags)
                {
                    var tagLayout = new StackLayout { Orientation = StackOrientation.Horizontal };
                    tagLayout.Children.Add (new Xamarin.Forms.Image { Source = tag.Image.GetImageSource () });
                    tagLayout.Children.Add (new Label { Text = tag.Name });
                    layout.Children.Add(tagLayout);
                }
            }
        }
    }
}
