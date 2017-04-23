using DataAccess.Public.Directory;

namespace Business.DataAccess.Public.Directory.DirectoryItems
{
    public sealed class CityItem : IDirectoryItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Sort { get; set; }
    }
}