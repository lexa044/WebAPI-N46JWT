using DNFKit.Core;
using DNFKit.Core.Dtos;
using DNFKit.Core.Models;
using DNFKit.Core.Repositories;
using DNFKit.Core.Services;

namespace DNFKit.Services
{
    public class CustomerService: ICustomerService
    {
        private readonly IDalSession _session;
        private readonly ICustomerRepository _repository;

        public CustomerService(IDalSession session, ICustomerRepository repository)
        {
            _session = session;
            _repository = repository;
        }

        public CustomerResponse FetchCustomer(int idType, string idNumber)
        {
            CustomerResponse response = null;
            Customer filter = new Customer();
            filter.IdType = idType;
            filter.IdNumber = idNumber;

            Customer model = _repository.FindBy(filter);
            if(null != model)
            {
                response = new CustomerResponse();
                response.Id = model.Id;
                response.IdNumber = model.IdNumber;
                response.IdType = model.IdType;
                response.IsPositive = model.IsPositive;
            }

            return response;
        }
    }
}
