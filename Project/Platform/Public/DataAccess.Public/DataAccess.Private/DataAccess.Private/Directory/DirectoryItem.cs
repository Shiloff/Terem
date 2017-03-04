using DataAccess.Public.Directory;

namespace DataAccess.Private.Directory
{
    public class DirectoryItem : IDirectoryItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}