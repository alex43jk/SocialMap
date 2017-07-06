using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Square.Picasso;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace XamarinGoogleMapDemo
{
    [Activity(Label = "GetPhotosFromSN")]
    public class GetPhotosFromSN : Activity
    {
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.GetPhotosFromSN);

            double lat = 46.7404283551898, lng = 28.8811472434254;
            string request = "https://api.vk.com/method/photos.search?lat=";
            string url = request +
                Convert.ToString(lat) +
                "&long=" +
                Convert.ToString(lng) +
                "&v=5.65";

            //Serialize
            var photo = await GetPhotoList(url);
            //string asdas = "23423";


            //Add photos in layout
            Picasso.With(this)
                .Load(photo.photo_130.ToString())
                .Into(FindViewById<ImageView>(Resource.Id.imageView1));
        }

        private async Task<Photo> GetPhotoList(string url)
        {
            var result = new List<Photo>();
            var httpClient = new HttpClient(new HttpClientHandler())
            {
                BaseAddress = new Uri(url)
            };
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var resp = await httpClient.GetAsync(url).ConfigureAwait(false);

            if (resp.IsSuccessStatusCode)
            {
                var content = resp.Content;
                string task = await content.ReadAsStringAsync().ConfigureAwait(false);

                return JsonConvert.DeserializeObject<Photo>(task);
            }
            return new Photo();
        }

        public class Photo
        {
            public string photo_130 { get; set; }

            public List<Photo> photo { get; set; }
        }
    }
}