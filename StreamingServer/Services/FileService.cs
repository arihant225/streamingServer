
using System.Diagnostics;
using System.Text;
using StreamingServer.Interfaces;

namespace StreamingServer.Services
{
    public class FileService():IFileService
    {

        public readonly string basepath="" ;

        public FileService(IConfiguration configuration):this() 
        {
            this.basepath = configuration.GetValue<string>(ConfigurationKeys.filePath)??"";
        }



        public FileStream GetMedia(string index)
        {
            var indexpath = basepath + @"index/";
            string url = indexpath + index;
            if (!url.Contains(".", StringComparison.InvariantCultureIgnoreCase))
                url += ".m3u8";

            if (File.Exists(url))
            {
                var fileStream=System.IO.File.OpenRead(url);
                return fileStream;
            }
            throw new Exception("File not found");

        }

        string GetExtension(string name)
        {
            return name.Split(".")[name.Split(".").Length - 1];
        }
        public async Task<string> saveFile(IFormFile file)
        {
            if (file == null) throw new Exception("file not found");
            string name = Guid.NewGuid().ToString() + "." + GetExtension(file.FileName);

            var path=basepath + name;

            handlePath(basepath);

            using (FileStream stream=new(path,FileMode.Create)) {
               
                await file.CopyToAsync(stream);
                await createM3U8File(name);
              


            }
            return path;
            

        }
        private void handlePath(String path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        public async Task createM3U8File(String filename)
        {
            var indexpath = basepath + @"index/";
            handlePath(indexpath);

            var command = $"ffmpeg -i {basepath + filename} " +
       "-map v:0 -b:v 500k -s 640x360 -c:v libx264 -preset fast -flags +global_header " +
       "-map a:0 -c:a aac -b:a 128k " +
       $"-hls_segment_filename {indexpath}{filename.Split('.')[0]}_360p_segment_%03d.ts -f hls " +
       "-hls_flags append_list -hls_list_size 0 " +
       $"{indexpath}{filename.Split('.')[0]}_360p.m3u8 " +
       "-map v:0 -b:v 1000k -s 1280x720 -c:v libx264 -preset fast -flags +global_header " +
       "-map a:0 -c:a aac -b:a 128k " +
       $"-hls_segment_filename {indexpath}{filename.Split('.')[0]}_720p_segment_%03d.ts -f hls " +
       "-hls_flags append_list -hls_list_size 0 " +
       $"{indexpath}{filename.Split('.')[0]}_720p.m3u8";

            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = $"/C {command}", // /C runs the command and exits
                    RedirectStandardOutput = false,
                    RedirectStandardError = false,
                    UseShellExecute = false,
                    CreateNoWindow = true
                },
                EnableRaisingEvents=true
            };
            process.Exited += (sender, e) => Console.WriteLine("FFmpeg processing completed.");
            process.Start();
            
           
        }



    }
}

