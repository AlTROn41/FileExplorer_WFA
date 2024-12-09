using System.IO;

namespace FileExplorerLibrary
{
    public class NavigationManager
    {
        private DirectoryInfo _previousDirectory;

        public void SetPreviousDirectory(DirectoryInfo current)
        {
            _previousDirectory = current;
        }

        public DirectoryInfo GetPreviousDirectory()
        {
            return _previousDirectory;
        }
    }
}
