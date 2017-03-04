using System.Collections.Generic;

namespace Business.DataAccess.Public.Directory
{
    public interface IDirectory<out T>
    {
        T this[int id] { get; }

        T this[string name] { get; }

        IReadOnlyCollection<T> All();
    }
}