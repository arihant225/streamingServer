namespace StreamingServer.Extensions
{
    public static class CorsExtensions
    {
        public static void ConfigureCors(this WebApplicationBuilder builder)
        {
  
            builder.Services.AddCors(
            cors =>
            {
            cors.AddPolicy(ConfigurationKeys.CorsPolicy, builder => { builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); });
            });

        }
    }
}
