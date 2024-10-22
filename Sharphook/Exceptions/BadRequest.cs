namespace Sharphook.Exceptions;

public sealed class BadRequest : SharphookHttpException
{
	public BadRequest(HttpResponseMessage response) : base(response) { }
}
