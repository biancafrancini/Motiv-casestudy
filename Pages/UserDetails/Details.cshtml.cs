using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace MotivWebApp.Pages.UserDetails
{
    public class DetailsModel : PageModel
    {
        public ResponseDetails User { get; private set; }

        // Fetches user details from an external API
        public async Task OnGetAsync()
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync("https://randomuser.me/api/");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    try
                    {
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };
                        var apiResponse = JsonSerializer.Deserialize<ApiResponse>(content, options);

                        if (apiResponse?.Results?.Count > 0)
                        {
                            User = apiResponse.Results[0];
                        }
                        else
                        {
                            Console.WriteLine("No results found in the API response.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Deserialization error: {ex.Message}");
                    }
                }
            }
        }
    }

    // Represents the response from the external API
    public class ApiResponse
    {
        public List<ResponseDetails> Results { get; set; }
        public Info Info { get; set; }
    }

    // Represents the details of a user
    public class ResponseDetails
    {
        public string Gender { get; set; }
        public UserName Name { get; set; }
        public Location Location { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Picture Picture { get; set; }
        public string Nat { get; set; }
    }

    // Represents the name of a user
    public class UserName
    {
        public string Title { get; set; }
        public string First { get; set; }
        public string Last { get; set; }
    }

    // Represents the location of a user
    public class Location
    {
        public Street Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public object Postcode { get; set; }
        public Coordinates Coordinates { get; set; }
        public Timezone Timezone { get; set; }
    }

    // Represents the street of a user's location
    public class Street
    {
        public int Number { get; set; }
        public string Name { get; set; }
    }

    // Represents the coordinates of a user's location
    public class Coordinates
    {
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }

    // Represents the timezone of a user's location
    public class Timezone
    {
        public string Offset { get; set; }
        public string Description { get; set; }
    }

    // Represents the picture of a user
    public class Picture
    {
        public string Large { get; set; }
        public string Medium { get; set; }
        public string Thumbnail { get; set; }
    }

    // Represents additional info about the API response
    public class Info
    {
        public string Seed { get; set; }
        public int Results { get; set; }
        public int Page { get; set; }
        public string Version { get; set; }
    }
}
