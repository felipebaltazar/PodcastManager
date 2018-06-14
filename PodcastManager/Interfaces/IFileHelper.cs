using System.IO;
using System.Runtime.CompilerServices;

[assembly:InternalsVisibleTo("ManagersApi.Tests")]
namespace PodcastManager.Interfaces
{
    internal interface IFileHelper
    {
        bool DirectoryExists(string directory);
        void CreateDirectory(string directory);
        bool FileExists(string filepath);
        FileStream FileOpen(string filepath, FileMode fileMode);
        void WriteAllText(string filePath, string text);
    }
}