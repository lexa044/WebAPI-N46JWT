using DNFKit.Core.Models;

namespace DNFKit.Core.Repositories
{
    public interface ICustomerRepository
    {
        Customer FindBy(Customer filter);
        Customer UpdateBy(Customer filter);
    }
}
