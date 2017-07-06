using Android.App;
using Android.Widget;
using Android.OS;
using Android.Gms.Maps;
using System;
using Android.Gms.Maps.Model;
using Android.Locations;
using Android.Runtime;
using Android.Content;
using Android.Graphics;

namespace XamarinGoogleMapDemo
{
    [Activity(Label = "XamarinGoogleMapDemo", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity, IOnMapReadyCallback, GoogleMap.IOnMapLongClickListener
    {
        GoogleMap map;

        public void OnMapLongClick(LatLng point)
        {
            CameraUpdate camera = CameraUpdateFactory.NewLatLngZoom(point, 15);
            map.MoveCamera(camera);
            string LatLong = "Latitude: ";
            LatLong += Convert.ToString(point.Latitude);
            LatLong += "\nLongitude: ";
            LatLong += Convert.ToString(point.Longitude);
            MarkerOptions options = new MarkerOptions()
                .SetPosition(point)
                .SetTitle(LatLong);

            map.AddMarker(options);

            StartActivity(typeof(GetPhotosFromSN)); 
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            map = googleMap;

            MarkerOptions markerOptions = new MarkerOptions();
            markerOptions.SetPosition(new LatLng(16.03, 108));
            markerOptions.SetTitle("My position");
            googleMap.AddMarker(markerOptions);

            //Optional
            googleMap.UiSettings.ZoomControlsEnabled = true;
            googleMap.UiSettings.CompassEnabled = true;
            googleMap.MoveCamera(CameraUpdateFactory.ZoomIn());

            //LongClick on Map
            map.SetOnMapLongClickListener(this);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            MapFragment mapFragment = (MapFragment)FragmentManager.FindFragmentById(Resource.Id.map);
            mapFragment.GetMapAsync(this);
        }
    }
}

