using System;
using System.Collections.Generic;
using System.Linq;
using Business.DataAccess.Public.Directory;
using DataAccess.Public.Directory;

namespace Business.DataAccess.Private.Directory
{
    public abstract class Directory<T> : IDirectory<T> where T: IDirectoryItem
    {
        private readonly Lazy<Data> _data;

        protected Directory()
        {
            _data = new Lazy<Data>(() => new Data(GetData()));
        }

        protected abstract IEnumerable<T> GetData();

        public int Count => _data.Value.ById.Count;

        public T this[string code] => _data.Value.ByCode[code];

        public T this[int id] => _data.Value.ById[id];

        public IReadOnlyCollection<T> All()
        {
            return _data.Value.GetAll;
        }

        private sealed class Data
        {
            public readonly IReadOnlyDictionary<string, T> ByCode;

            public readonly IReadOnlyDictionary<int, T> ById;

            public readonly IReadOnlyCollection<T> GetAll;

            public Data(IEnumerable<T> items)
            {
                var list = items.ToList();
                var byCode = new Dictionary<string, T>(StringComparer.InvariantCultureIgnoreCase);
                var byId = new Dictionary<int, T>();
                GetAll = list;
                foreach (var item in list)
                {
                    byCode.Add(item.Name, item);
                    byId.Add(item.Id, item);
                }

                ById = byId;
                ByCode = byCode;
            }
        }
    }
}
