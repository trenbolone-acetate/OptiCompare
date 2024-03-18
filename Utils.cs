namespace OptiCompare;

public class Utils
{
    public async Task<string> GetResult(string search)
    {
        search = search.Replace(" ", "%20");
        try
        {
            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri($"https://api.techspecs.io/v4/product/search?query={search}"),
                    Headers =
                    {
                        { "accept", "application/json" },
                        { "Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6ImN1c19QQm1GbjdKbmRram4zViIsIm1vZXNpZlByaWNpbmdJZCI6InByaWNlXzFNUXF5dkJESWxQbVVQcE1SWUVWdnlLZSIsImlhdCI6MTcwMjU5NzkyNX0.6h1xEEDEk1mhyyU9ZlmyuGQo0O59cOjS2ZoLY6laSxI" },
                    },
                };

                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    return await response.Content.ReadAsStringAsync();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            // Log the exception or take appropriate action
        }

        return "not found";
    }
}