using DNFKit.Core.Dtos;

namespace DNFKit.Core.Services
{
    public interface ICustomerService
    {
        CustomerResponse FetchCustomer(int idType, string idNumber);
        CustomerResponse UpdateCustomer(int idType, string idNumber);
    }
}
