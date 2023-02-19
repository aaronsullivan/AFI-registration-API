using NUnit.Framework;
using Registration.Domain.Customers;
using Registration.Domain.Exceptions;

namespace Registration.Tests;

public class Tests
{
    [TestCase(null, false)]
    [TestCase("a", false)]
    [TestCase("aa", false)]
    [TestCase("aaronaaronaaronaaronaaronaaronaaronaaronaaronaaronn", false)]
    [TestCase("aar", true)]
    [TestCase("aaon", true)]
    [TestCase("aaronaaronaaronaaronaaronaaronaaronaaronaaronaaron", true)]
    public void GivenDifferentFirstNames_Then_ValidityShouldMatchExpected(string firstName, bool expectedValidity)
    {
        var registerCustomer = async () =>
        {
            var customer = await Customer.Register(
                firstName,
                "Smith",
                "AA-123456",
                null,
                "abcd123@hotmail.co.uk");

            return customer;
        };

        if (expectedValidity)
        {
            Assert.DoesNotThrowAsync(async () =>
            {
                await registerCustomer();
            });
        }
        else
        {
            Assert.ThrowsAsync<ValidationException>(async () =>
            {
                await registerCustomer();
            });
        }
    }

    [TestCase(null, false)]
    [TestCase("s", false)]
    [TestCase("sm", false)]
    [TestCase("smithsmithsmithsmithsmithsmithsmithsmithsmithsmiths", false)]
    [TestCase("smi", true)]
    [TestCase("smith", true)]
    [TestCase("smithsmithsmithsmithsmithsmithsmithsmithsmithsmith", true)]
    public void GivenDifferentLastNames_Then_ValidityShouldMatchExpected(string lastName, bool expectedValidity)
    {
        var registerCustomer = async () =>
        {
            var customer = await Customer.Register(
                "Bob",
                lastName,
                "AA-123456",
                null,
                "abcd123@hotmail.co.uk");

            return customer;
        };

        if (expectedValidity)
        {
            Assert.DoesNotThrowAsync(async () =>
            {
                await registerCustomer();
            });
        }
        else
        {
            Assert.ThrowsAsync<ValidationException>(async () =>
            {
                await registerCustomer();
            });
        }
    }

    [TestCase(null, false)]
    [TestCase("ABC-123", false)]
    [TestCase("AA", false)]
    [TestCase("12-999999", false)]
    [TestCase("ab-123456", false)]
    [TestCase("aB-123456", false)]
    [TestCase("AB.123456", false)]
    [TestCase("AB-12345", false)]
    [TestCase("AB-1234567", false)]
    [TestCase("AB-123456", true)]
    [TestCase("JK-987654", true)]
    [TestCase("XX-999999", true)]
    public void GivenDifferentPolicyNumbers_Then_ValidityShouldMatchExpected(string policyReferenceNumber, bool expectedValidity)
    {
        var registerCustomer = async () =>
        {
            var customer = await Customer.Register(
                "Bob",
                "Smith",
                policyReferenceNumber,
                null,
                "abcd123@hotmail.co.uk");

            return customer;
        };

        if (expectedValidity)
        {
            Assert.DoesNotThrowAsync(async () =>
            {
                await registerCustomer();
            });
        }
        else
        {
            Assert.ThrowsAsync<ValidationException>(async () =>
            {
                await registerCustomer();
            });
        }
    }

    [Test]
    public void GivenNoDateOfBirthAndNoEmail_Then_ShouldFailValidation()
    {
        Assert.ThrowsAsync<ValidationException>(async () =>
        {
            var customer = await Customer.Register(
                "Bob",
                "Smith",
                "AA-123456",
                dateOfBirth: null,
                null);
        });
    }

    [Test]
    public void GivenNoDateOfBirthButValidEmail_Then_ShouldPassValidation()
    {
        Assert.DoesNotThrowAsync(async () =>
        {
            var customer = await Customer.Register(
                "Bob",
                "Smith",
                "AA-123456",
                dateOfBirth: null,
                "abcd123@hotmail.co.uk");
        });
    }

    [Test]
    public void Given17YearOld_Then_ShouldFailValidation()
    {
        Assert.ThrowsAsync<ValidationException>(async () =>
        {
            var dateOfBirth = DateTime.UtcNow.Date.AddYears(-18).AddDays(1);

            var customer = await Customer.Register(
                "Bob",
                "Smith",
                "AA-123456",
                dateOfBirth: dateOfBirth,
                "abcd123@hotmail.co.uk");
        });
    }

    [Test]
    public void GivenTodayAsDateOfBirth_Then_ShouldFailValidation()
    {
        Assert.ThrowsAsync<ValidationException>(async () =>
        {
            var customer = await Customer.Register(
                "Bob",
                "Smith",
                "AA-123456",
                dateOfBirth: DateTime.UtcNow.Date,
                "abcd123@hotmail.co.uk");
        });
    }

    [Test]
    public void GivenValidDateOfBirth_Then_ShouldPassValidation()
    {
        Assert.DoesNotThrowAsync(async () =>
        {
            var customer = await Customer.Register(
                "Bob",
                "Smith",
                "AA-123456",
                dateOfBirth: new DateTime(1990, 07, 12),
                "abcd123@hotmail.co.uk");
        });
    }

    [Test]
    public void GivenValid18YearOld_Then_ShouldPassValidation()
    {
        Assert.DoesNotThrowAsync(async () =>
        {
            var dateOfBirth = DateTime.UtcNow.Date.AddYears(-18);

            var customer = await Customer.Register(
                "Bob",
                "Smith",
                "AA-123456",
                dateOfBirth: dateOfBirth,
                "abcd123@hotmail.co.uk");
        });
    }


    [TestCase(null, false)]
    [TestCase("AB1@hotmail.com", false)]
    [TestCase("AB1234@h.co.uk", false)]
    [TestCase("AB1234@hotmail.net", false)]
    [TestCase("asullivan@hotmail.co.uk", true)]
    [TestCase("asullivan1990@hotmail.com", true)]
    public void GivenDifferentEmails_Then_ValidityShouldMatchExpected(string email, bool expectedValidity)
    {
        var registerCustomer = async () =>
        {
            var customer = await Customer.Register(
                "Bob",
                "Smith",
                "AB-123456789",
                null,
                email);

            return customer;
        };

        if (expectedValidity)
        {
            Assert.DoesNotThrowAsync(async () =>
            {
                await registerCustomer();
            });
        }
        else
        {
            Assert.ThrowsAsync<ValidationException>(async () =>
            {
                await registerCustomer();
            });
        }
    }
}