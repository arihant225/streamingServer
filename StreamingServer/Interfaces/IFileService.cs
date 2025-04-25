namespace StreamingServer.Interfaces
{
    public interface IFileService
    {

        public Task<string> saveFile(IFormFile file);
        public FileStream GetMedia(string index);
    }
}
