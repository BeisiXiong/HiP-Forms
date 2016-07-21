using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Content.Res;
using Android.Views;
using Android.Widget;
using de.upb.hip.mobile.droid.Helpers;
using de.upb.hip.mobile.pcl.BusinessLayer.Models;
using Org.Osmdroid.Api;
using Org.Osmdroid.Bonuspack.Overlays;
using Org.Osmdroid.Bonuspack.Routing;
using Org.Osmdroid.Tileprovider;
using Org.Osmdroid.Tileprovider.Tilesource;
using Org.Osmdroid.Util;
using Org.Osmdroid.Views;
using Marker = Org.Osmdroid.Bonuspack.Overlays.Marker;
using Polyline = Org.Osmdroid.Bonuspack.Overlays.Polyline;

namespace de.upb.hip.mobile.droid.Activities {
    [Activity (Theme = "@style/AppTheme",Label = "RouteNavigationActivity")]
    public class RouteNavigationActivity : Activity {

        protected MapView mapView;
        private ProgressDialog progressDialog;
        private GeoPoint geoLocation;

        protected override void OnCreate (Bundle savedInstanceState)
        {
            base.OnCreate (savedInstanceState);
            SetContentView (Resource.Layout.activity_route_navigation);

            geoLocation = new GeoPoint (51.71352, 8.74021);

            SetUpMap ();

            ShowRoute ();
        }


        private void SetUpMap ()
        {
            // init progress dialog
            progressDialog = new ProgressDialog(this);
            progressDialog.SetCancelable(true);

            // getting the map
            GenericMapView genericMap = (GenericMapView)FindViewById(Resource.Id.routeNavigationMap);
            MapTileProviderBasic bitmapProvider = new MapTileProviderBasic(this);
            genericMap.SetTileProvider(bitmapProvider);
            mapView = genericMap.GetMapView();
            mapView.SetBuiltInZoomControls(true);
            mapView.SetMultiTouchControls(true);

            mapView.SetTileSource(TileSourceFactory.Mapnik);
            mapView.TilesScaledToDpi = (true);
            //mapView.SetMaxZoomLevel(RouteDetailsActivity.MAX_ZOOM_LEVEL);

            // mMap prefs:
            IMapController mapController = mapView.Controller;
            mapController.SetZoom(13);
            mapController.SetCenter(geoLocation);
        }

        private void ShowRoute ()
        {
            StrictMode.ThreadPolicy policy = new StrictMode.ThreadPolicy.Builder().PermitAll().Build();
            StrictMode.SetThreadPolicy(policy);

            //1. "Hello, Routing World"
            //2. Playing with the RoadManager
            RoadManager roadManager = new MapQuestRoadManager("WRWdd9j02K8tGtERI2LtFiCLsRUKyJnJ");
            roadManager.AddRequestOption("routeType=pedestrian");
            GeoPoint startPoint = new GeoPoint(51.71352, 8.74021);
            List<GeoPoint> waypoints = new List<GeoPoint>();
            waypoints.Add(startPoint);
            GeoPoint endPoint = new GeoPoint(51.7199006, 8.754952000000003);
            waypoints.Add(endPoint);
            Road road = roadManager.GetRoad (waypoints);
            if (road.MStatus != Road.StatusOk)
                Toast.MakeText(Application.Context, "Error when loading the road - status=" + road.MStatus, ToastLength.Short).Show();

            Polyline roadOverlay = RoadManager.BuildRoadOverlay(road, Application.Context);
            mapView.Overlays.Add(roadOverlay);

            //3. Showing the Route steps on the map
            FolderOverlay roadMarkers = new FolderOverlay(Application.Context);
            mapView.Overlays.Add(roadMarkers);
            Drawable nodeIcon = ResourcesCompat.GetDrawable(Resources, Resource.Drawable.marker_node, null);
            for (int i = 0; i < road.MNodes.Count; i++)
            {
                RoadNode node = (RoadNode)road.MNodes[i];
                Marker nodeMarker = new Marker(mapView);
                nodeMarker.Position = node.MLocation;
                nodeMarker.SetIcon(nodeIcon);
                

                //4. Filling the bubbles
                nodeMarker.Title = ("Step " + i);
                nodeMarker.Snippet = (node.MInstructions);
                nodeMarker.SubDescription = (Road.GetLengthDurationText(node.MLength, node.MDuration));
                Drawable iconContinue = ResourcesCompat.GetDrawable(Resources, Resource.Drawable.ic_continue, null);
                nodeMarker.Image = (iconContinue);
                //4. end

                roadMarkers.Add(nodeMarker);
            }
        }

    }
}