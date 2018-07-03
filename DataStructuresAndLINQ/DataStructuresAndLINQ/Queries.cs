using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace DataStructuresAndLINQ
{
    static class Queries
    {
        public static string Path = "https://5b128555d50a5c0014ef1204.mockapi.io/";

        public static List<User> GetUsersInfo()
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(Path + "/users").Result; ;
                return JsonConvert.DeserializeObject<List<User>>(response.Content.ReadAsStringAsync().Result);
            }
        }

        public static string GetPostsInfo()
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(Path + "/posts").Result;
                return response.Content.ReadAsStringAsync().Result;
            }
        }

        public static string GetCommentsInfo()
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(Path + "/comments").Result;
                return response.Content.ReadAsStringAsync().Result;
            }
        }

        public static string GetTodosInfo()
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(Path + "/todos").Result;
                return response.Content.ReadAsStringAsync().Result;
            }
        }
        public static string GetAddressInfo()
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(Path + "/address").Result;
                return response.Content.ReadAsStringAsync().Result;
            }
        }
    }
}
