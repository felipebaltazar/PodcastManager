using PodcastManager.Interfaces;
using System.IO;

namespace PodcastManager.Helpers
{
    internal class FileHelper : IFileHelper
    {
        public void CreateDirectory(string directory) =>
            Directory.CreateDirectory(directory);

        public bool DirectoryExists(string directory) =>
            Directory.Exists(directory);

        public bool FileExists(string filepath) =>
            File.Exists(filepath);

        public FileStream FileOpen(string filepath, FileMode fileMode) =>
            File.Open(filepath, fileMode);

        public void WriteAllText(string filePath, string text) =>
            File.WriteAllText(filePath, text);
    }
}