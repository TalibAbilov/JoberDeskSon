using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoberDesk.Business.Helpers.Extensions
{
    public static class FileExtension
    {
        public static string Upload(this IFormFile file, string root, string folder)
        {
            string filname = Guid.NewGuid() + file.FileName;
            if (filname.Length > 100)
            {
                filname = filname.Substring(0, 100);
            }
            string path = Path.Combine(root, folder, filname);
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return filname;
        }

        public static bool Delete(string root, string folder, string filName)
        {
            string path = Path.Combine(root, folder, filName);
            if (!File.Exists(path))
            {
                return false;
            }
            File.Delete(path);
            return true;
        }
    }
}
