namespace ThesesSystem.Web.Infrastructure.StorageFiles
{
    using System.Web;

    public interface IStorage
    {
        string Save(HttpPostedFileBase file, string directory);
    }
}