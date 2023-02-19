using Registration.Domain.Exceptions;
using Registration.Domain.Specifications;
using Registration.Helpers.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace Registration.Domain.Customers;

public class Customer : IAggregateRoot
{
    public virtual int? Id { get; protected set; }
    public virtual string? FirstName { get; protected set; }
    public virtual string? LastName { get; protected set; }
    public virtual string? PolicyReferenceNumber { get; protected set; }

    [Column("DateOfBirth", TypeName = "datetime")]
    public virtual DateTimeOffset? DateOfBirth { get; protected set; }
    public virtual string? Email { get; protected set; }

    public static Task<Customer> Register(
        string? firstName, 
        string? lastName, 
        string? policyReferenceNumber, 
        DateTimeOffset? dateOfBirth, 
        string? email)
    {
        if (string.IsNullOrEmpty(firstName))
        {
            throw new ValidationException(nameof(firstName));
        }

        if (firstName.Length < 3 || firstName.Length > 50)
        {
            throw new ValidationException(nameof(firstName));
        }

        if (string.IsNullOrEmpty(lastName))
        {
            throw new ValidationException(nameof(lastName));
        }

        if (lastName.Length < 3 || lastName.Length > 50)
        {
            throw new ValidationException(nameof(lastName));
        }

        if (string.IsNullOrEmpty(policyReferenceNumber))
        {
            throw new ValidationException(nameof(policyReferenceNumber));
        }

        var policyReferenceNumberSpec = new ValidPolicyReferenceNumberSpecification();
        if (!policyReferenceNumberSpec.IsSatisfied(policyReferenceNumber))
        {
            throw new ValidationException("Invalid policy reference number. It should match the following format: 2 capitalised alpha characters followed by a hyphen and 6 numbers.");
        }

        if (!dateOfBirth.HasValue && string.IsNullOrEmpty(email))
        {
            throw new ValidationException("Either a date of birth or email are required");
        }

        if (dateOfBirth.HasValue)
        {
            const int requiredAge = 18;
            var olderThanDateOfBirthSpecification = new OlderThanDateOfBirthSpecification(requiredAge);
            if (!olderThanDateOfBirthSpecification.IsSatisfied(dateOfBirth.Value))
            {
                throw new ValidationException($"You must be atleast {requiredAge} years old to register.");
            }
        }

        if (!string.IsNullOrWhiteSpace(email))
        {
            var emailSpec = new ValidEmailSpecification();
            if (!emailSpec.IsSatisfied(email))
            {
                throw new ValidationException("Invalid email. It should match the following format: a string of at least 4 alpha numeric chars followed by an ‘@’ sign and then another string of at least 2 alpha numeric chars. It should also end in either ‘.com’ or ‘.co.uk’.");
            }
        }

        return Task.FromResult(new Customer
        {
            FirstName = firstName,
            LastName = lastName,
            PolicyReferenceNumber = policyReferenceNumber,
            DateOfBirth = dateOfBirth,
            Email = email
        });
    }
}
