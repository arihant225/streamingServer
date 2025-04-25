using StreamingServer.Interfaces;
using StreamingServer.Services;

namespace StreamingServer.Extensions
{
    public static class DIConfigurationExtensions
    {
        public static void ConfigureDI(this WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<IFileService, FileService>();
        }   
    }
}
