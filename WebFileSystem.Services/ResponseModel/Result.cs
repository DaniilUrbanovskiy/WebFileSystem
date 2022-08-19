namespace WebFileSystem.Services
{
    public static class Result
    {
        public static string Created { get; } = "New folder created";
        public static string NoName { get; } = "Enter folder name";
        public static string Exists { get; } = "This folder already exists";
        public static string Removed { get; } = "Folder removed";
        public static string WrongName { get; } = "Enter correct name";
    }
}
