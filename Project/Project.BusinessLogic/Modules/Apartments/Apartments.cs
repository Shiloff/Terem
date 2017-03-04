

using Project.Domain.Abstract;

namespace Project.BusinessLogic.Modules.Apartments
{
    class Apartments : IApartments
    {
        public Apartments(IApartmentRepository repo)
        {
            repo = repo;
        }
    }
}
