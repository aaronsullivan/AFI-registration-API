using Registration.Helpers.Specification;

namespace Registration.Domain.Specifications;

public class OlderThanDateOfBirthSpecification : ISpecification<DateTimeOffset>
{
    private readonly int _requiredAge;

    public OlderThanDateOfBirthSpecification(int requiredAge)
    {
        _requiredAge = requiredAge;
    }

    public bool IsSatisfied(DateTimeOffset dateOfBirth)
    {
        var age = DetermineAge(dateOfBirth);

        return age >= _requiredAge;
    }

    private int DetermineAge(DateTimeOffset dateOfBirth)
    {
        DateTime today = DateTime.Today;
        int age = today.Year - dateOfBirth.Year;
        if (dateOfBirth > today.AddYears(-age))
        {
            age--;
        }

        return age;
    }
}
