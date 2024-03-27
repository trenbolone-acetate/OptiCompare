using OptiCompare.Models;

namespace OptiCompare.Utils;

public static class Utils
{
    public static string GetConnectionString(WebApplicationBuilder builder)
    {
        return builder.Configuration["phoneDB:ConStr"];
    }
    public static Dictionary<string,bool> ComparePhones(Phone phone1, Phone phone2)
    {
        Dictionary<string,bool> comparisons = new Dictionary<string,bool>();
        
        comparisons.Add("hasNetwork5GBands", true);
        // Add more comparisons as needed...

        return comparisons;
    }
}