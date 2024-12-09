using System;
using System.IO;
using System.Collections.Generic;

namespace FileExplorerLibrary
{
    public class DirectoryManager
    {
        public DirectoryInfo CurrentDirectory { get; private set; }

        public DirectoryManager(string initialPath)
        {
            if (Directory.Exists(initialPath))
            {
                CurrentDirectory = new DirectoryInfo(initialPath);
            }
            else
            {
                throw new DirectoryNotFoundException($"Path not found: {initialPath}");
            }
        }

        public IEnumerable<DirectoryInfo> GetDirectories()
        {
            return CurrentDirectory.GetDirectories();
        }

        public IEnumerable<FileInfo> GetFiles()
        {
            return CurrentDirectory.GetFiles();
        }

        public void MoveToParentDirectory()
        {
            if (CurrentDirectory?.Parent != null)
            {
                CurrentDirectory = CurrentDirectory.Parent;
            }
            else
            {
                throw new InvalidOperationException("No parent directory available.");
            }
        }

        public void MoveToDirectory(DirectoryInfo directory)
        {
            if (directory.Exists)
            {
                CurrentDirectory = directory;
            }
            else
            {
                throw new DirectoryNotFoundException($"Directory not found: {directory.FullName}");
            }
        }
    }
}
