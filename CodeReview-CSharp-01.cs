using System;
using System.Net.Http;
using System.Threading.Tasks;

class NetworkManager
{
    public async Task FetchAndDisplayUser(string id)
    {
        string url = "https://api.example.com/user/" + id;
        using (HttpClient httpClient = new HttpClient())
        {
            var response = httpClient.GetAsync(url).Result;
            string content = response.Content.ReadAsStringAsync().Result;
          
            if (content == null || content.Equals(""))
            {
                Console.WriteLine("Error: empty body");
                return null;
            }
          
            var user = Newtonsoft.Json.JsonConvert.DeserializeObject<User>(content);
            Console.WriteLine("User name: " + (user?.Name ?? "Unknown"));
        }
    }
}