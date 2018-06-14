using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PodcastManager.Interfaces;

namespace ManagersApi.Tests.Helpers
{
    public class FakeFileHelper : IFileHelper
    {
        public void CreateDirectory(string directory)
        {
        }

        public bool DirectoryExists(string directory)
        {
            return true;
        }

        public bool FileExists(string filepath)
        {
            return false;
        }

        public FileStream FileOpen(string filepath, FileMode fileMode)
        {
            return (FileStream)Stream.Null;
        }

        public void WriteAllText(string filePath, string text)
        {
        }
    }
}