namespace Sharphook.Exceptions;

public sealed class Unauthorized : SharphookHttpException
{
	public Unauthorized(HttpResponseMessage response) : base(response) { }
}
