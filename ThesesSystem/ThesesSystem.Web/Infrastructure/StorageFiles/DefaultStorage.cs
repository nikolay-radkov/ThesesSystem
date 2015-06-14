namespace ThesesSystem.Web.Infrastructure.StorageFiles
{
    using System.IO;
    using System.Web;

    public class DefaultStorage : IStorage
    { 
        public string Save(HttpPostedFileBase file, string directory)
        {
           
            if (file != null && file.ContentLength > 0)
            {
                directory = directory.Replace(':', '-');

                if (!Directory.Exists(directory))
                {
                    System.IO.Directory.CreateDirectory(directory);
                }

          
                var fileName = Path.GetFileName(file.FileName);
                var fullpath = Path.Combine(directory, fileName);

                file.SaveAs(fullpath);

                return fullpath;
            }

            return null;
        }
    }
}