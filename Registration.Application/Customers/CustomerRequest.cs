namespace Registration.Application.Customers;

public class RegisterCustomerRequest
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PolicyReferenceNumber { get; set; }
    public DateTimeOffset? DateOfBirth { get; set; }
    public string? Email { get; set; }
}
