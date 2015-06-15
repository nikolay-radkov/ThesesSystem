namespace ThesesSystem.Web.Infrastructure.Constants
{
    public static class MimeTypeCreator
    {
        private const string RAR = "rar";
        private const string ZIP = "zip";
        private const string SEVENZ = "7z";
        private const string BZIP = "bz";
        private const string BZIP2 = "bz2";
        private const string TAR = "tar";

        private const string RAR_MIME_TYPE = "application/x-rar-compressed";
        private const string ZIP_MIME_TYPE = "application/zip";
        private const string SEVENZ_MIME_TYPE = "application/x-7z-compressed";
        private const string BZIP_MIME_TYPE = "application/x-bzip";
        private const string BZIP2_MIME_TYPE = "application/x-bzip2";
        private const string TAR_MIME_TYPE = "application/x-tar";

        public static string GetFileMimeType(string fileExtension)
        {
            switch (fileExtension)
            {
                case RAR:
                    return RAR_MIME_TYPE;
                case SEVENZ:
                    return SEVENZ_MIME_TYPE;
                case ZIP:
                    return ZIP_MIME_TYPE;
                case BZIP:
                    return BZIP_MIME_TYPE;
                case BZIP2:
                    return BZIP2_MIME_TYPE;
                case TAR:
                    return TAR_MIME_TYPE;
                default:
                    return System.Net.Mime.MediaTypeNames.Application.Octet;
            }
        }
    }
}