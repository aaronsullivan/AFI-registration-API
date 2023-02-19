using Registration.Domain.Customers;
using Registration.Helpers.Repository;

namespace Registration.Application.Customers;

public class CustomerService : ICustomerService
{
    private readonly IRepository<Customer> _customerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CustomerService(
        IRepository<Customer> customerRepository,
        IUnitOfWork unitOfWork)
    {
        _customerRepository = customerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CustomerDTO> Register(RegisterCustomerRequest request)
    {
        var customer = await Customer.Register(
            request.FirstName,
            request.LastName,
            request.PolicyReferenceNumber,
            request.DateOfBirth,
            request.Email);

        _customerRepository.Add(customer);
        await _unitOfWork.SaveChangesAsync();

        return new CustomerDTO(customer);
    }
}