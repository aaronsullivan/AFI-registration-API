namespace Registration.Application.Customers
{
    public interface ICustomerService
    {
        Task<CustomerDTO> Register(RegisterCustomerRequest request);
    }
}
