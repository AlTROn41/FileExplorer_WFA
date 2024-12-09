using System;

namespace FileExplorerLibrary
{
    public class EventsManager
    {
        public delegate void DirectoryChangedHandler(string path);
        public event DirectoryChangedHandler OnDirectoryChanged;

        public void RaiseDirectoryChanged(string path)
        {
            OnDirectoryChanged?.Invoke(path);
        }
    }
}
