using DataAccess.Public.Directory;

namespace Business.DataAccess.Public.Directory.DirectoryItems
{
    public sealed class AnimalItem : IDirectoryItem
    {
        public string Icon { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
    }
}