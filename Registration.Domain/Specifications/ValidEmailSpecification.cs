using Registration.Helpers.Specification;
using System.Text.RegularExpressions;

namespace Registration.Domain.Specifications;

public class ValidEmailSpecification : ISpecification<string>
{
    public bool IsSatisfied(string email)
    {
        var matches = Regex.Match(email, @"^[a-zA-Z\d]{4,}[@][a-zA-Z\d]{2,}(.co.uk|.com)")?.Success ?? false;

        return matches;
    }
}
