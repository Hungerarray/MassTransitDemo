using MassTransit;
using Messages;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class MessageProducerController : ControllerBase
{
	private readonly ISendEndpointProvider _endpointProvider;

	public MessageProducerController(ISendEndpointProvider endpointProvider)
	{
		_endpointProvider = endpointProvider;
	}

	[HttpGet("probable")]
	public async Task<ActionResult> Probable(double chance)
	{
		var endpoint = await _endpointProvider.GetSendEndpoint(new Uri("queue:message-with-pass-rate"));
		await endpoint.Send<MessageWithPassRate>(new()
		{
			Value = chance
		});
		return Ok();
	}

	[HttpGet("Delayed")]
	public async Task<ActionResult> Delayed(int seconds)
	{
		var endpoint = await _endpointProvider.GetSendEndpoint(new Uri("queue:delayed-message"));
		await endpoint.Send<DelayedMessage>(new()
		{
			Seconds = TimeSpan.FromSeconds(seconds)
		});
		return Ok();
	}
}