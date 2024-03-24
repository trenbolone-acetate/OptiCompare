namespace OptiCompare;

public static class Utils
{
    public static string GetConnectionString(WebApplicationBuilder builder)
    {
        return builder.Configuration["phoneDB:ConStr"];
    }
}