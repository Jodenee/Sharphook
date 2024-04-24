namespace Sharphook.Exceptions;

public class NotFound : SharphookHttpException
{
	public NotFound(HttpResponseMessage response) : base(response) { }
}
