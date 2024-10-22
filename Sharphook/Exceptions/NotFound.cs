namespace Sharphook.Exceptions;

public sealed class NotFound : SharphookHttpException
{
	public NotFound(HttpResponseMessage response) : base(response) { }
}
