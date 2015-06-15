namespace ThesesSystem.Web.Infrastructure.Storage
{
    public interface IStorage
    {
        byte[] DownloadFile(string path);

        string UploadFile(byte[] content, string filename, string target);

        void DeleteFile(string path);
    }
}
