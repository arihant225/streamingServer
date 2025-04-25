using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace StreamingServer.DTOs
{
    public class UploadFile
    {

        [Required]
        public FormFile? File;

    }
}
