namespace ThesesSystem.Web.Infrastructure.Storage
{
    using DropNet;
    using System;
    using System.Web;

    public class DropboxStorage : IStorage
    {
        private const string App_key = "g2hfafbhxjfb2e6";
        private const string App_secret = "mg5fjhl35rg23io";

        private const string OauthAccessTokenValue = "zqcsowz0wf5t90j3";

        private const string OauthAccessTokenSecret = "l8sfa2vtxwca6r0";

        private DropNetClient client; 

        public DropboxStorage()
        {
            client = new DropNetClient(App_key, App_secret, OauthAccessTokenValue, OauthAccessTokenSecret);
        }

        public byte[] DownloadFile(string path)
        {
            var fileBytes = client.GetFile(path);

            return fileBytes;
        }

        public string UploadFile(byte[] content, string filename, string target)
        {
            var uploaded = client.UploadFile(target, filename, content);

            return uploaded.Path;
        }
    }
}
