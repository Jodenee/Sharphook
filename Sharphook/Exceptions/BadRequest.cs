namespace Sharphook.Exceptions;

public class BadRequest : SharphookHttpException
{
    public BadRequest(HttpResponseMessage response) : base(response) { }
}
