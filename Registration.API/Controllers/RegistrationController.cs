using Microsoft.AspNetCore.Mvc;
using Registration.Application.Customers;

namespace Registration.API.Controllers;

[ApiController]
[Route("registration")]
public class RegistrationController : ControllerBase
{
    private readonly ICustomerService _customerService;
    private readonly ILogger<RegistrationController> _logger;

    public RegistrationController(
        ICustomerService customerService,
        ILogger<RegistrationController> logger)
    {
        _customerService = customerService;
        _logger = logger;
    }

    [HttpPost(Name = "Register")]
    public async Task<IActionResult> Register([FromBody] RegisterCustomerRequest request)
    {
        CustomerDTO customer;

        try
        {
           customer = await _customerService.Register(request);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

        return Ok(customer.Id);
    }
}
