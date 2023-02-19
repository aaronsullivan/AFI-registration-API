using Registration.Helpers.Specification;
using System.Text.RegularExpressions;

namespace Registration.Domain.Specifications;

public class ValidPolicyReferenceNumberSpecification : ISpecification<string>
{
    public bool IsSatisfied(string policyReferenceNumber)
    {
        var matches = Regex.Match(policyReferenceNumber, @"^[A-Z]{2}[-][\d]{6}")?.Success ?? false;

        return matches;
    }
}
