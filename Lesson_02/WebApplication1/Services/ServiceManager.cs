using Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IPeopleService> _peopleService;

        public ServiceManager(IUnitOfWork unitOfWork)
        {
            _peopleService = new Lazy<IPeopleService>(() => new PeopleService(unitOfWork));
        }

        public IPeopleService PeopleService => _peopleService.Value;
    }
}
