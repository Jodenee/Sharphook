namespace Sharphook.Exceptions;

public sealed class BadGateway : SharphookHttpException
{
	public BadGateway(HttpResponseMessage response) : base(response) { }
}
