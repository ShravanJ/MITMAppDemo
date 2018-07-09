using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Newtonsoft.Json;
using System.Net.Http;
using Newtonsoft.Json.Linq;
namespace MyTestApp
{
    public partial class MainPage : ContentPage
    {

        string serviceURL = "http://192.168.1.2:3000/posts";
        string contentType = "application/json"; 

        public MainPage()
        {
            InitializeComponent();
            Title = "MITM Demo App";
        }

        async void getButton_Clicked(object sender, System.EventArgs e)
        {
            var client = new HttpClient();
            var response = await client.GetAsync(serviceURL);
            string jsonListString = await response.Content.ReadAsStringAsync();
            if(jsonListString != "")
            {
                MyJSONObject o = JsonConvert.DeserializeObject<MyJSONObject>(jsonListString.Substring(1, jsonListString.Length - 2));
                string objData = "ID: " + o.id + " Author: " + o.author + " Title: " + o.title;
                await DisplayAlert("Response", objData , "OK");
            }
        }

        async void postButton_Clicked(object sender, System.EventArgs e)
        {
            JObject jsonObj = new JObject();
            jsonObj.Add("id", 2);
            jsonObj.Add("title", "TEST TITLE");
            jsonObj.Add("author", "TEST AUTHOR");
            var client = new HttpClient();
            var response = await client.PostAsync(serviceURL, new StringContent(jsonObj.ToString(), Encoding.UTF8, contentType));
            if(response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                await DisplayAlert("Response", "POST Successful", "OK");
            }
        }
    }

    public class MyJSONObject
    {
        public int id { get; set; }
        public string title { get; set; }
        public string author { get; set; }
    }

}
