namespace WebFileSystem.Services
{
    public static class WebServerError
    {
        public static string Created { get; set; } = "New folder created";
        public static string NoName { get; set; } = "Enter folder name";
        public static string Exists { get; set; } = "This folder already exists";
        public static string Removed { get; set; } = "Folder removed";
        public static string WrongName { get; set; } = "Enter correct name";
    }
}
