namespace Sharphook.Exceptions;

public sealed class Forbidden : SharphookHttpException
{
	public Forbidden(HttpResponseMessage response) : base(response) { }
}
