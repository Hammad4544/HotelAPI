using HotelServices.Implementation;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class PaymentController : ControllerBase
{
    private readonly PaymentService _paymentService;

    public PaymentController(PaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpPost("create-checkout-session")]
    public IActionResult CreateCheckoutSession([FromBody] PaymentRequest request)
    {
        var session = _paymentService.CreateCheckoutSession(
            request.HotelName,
            request.Price,
            request.SuccessUrl,
            request.CancelUrl
        );

        return Ok(new { url = session.Url });
    }
}

public class PaymentRequest
{
    public string HotelName { get; set; }
    public decimal Price { get; set; }
    public string SuccessUrl { get; set; }
    public string CancelUrl { get; set; }
}
