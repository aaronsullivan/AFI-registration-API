using Registration.Domain.Customers;

namespace Registration.Application.Customers;

public class CustomerDTO
{
    public int Id { get; set; }

    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PolicyReferenceNumber { get; set; }
    public DateTimeOffset? DateOfBirth { get; set; }
    public string? Email { get; set; }

    public CustomerDTO(Customer customer)
    {
        Id = customer.Id.Value;
        FirstName = customer.FirstName;
        LastName = customer.LastName;
        PolicyReferenceNumber = customer.PolicyReferenceNumber;
        DateOfBirth = customer.DateOfBirth;
        Email = customer.Email;
    }
}
