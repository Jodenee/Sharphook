namespace Sharphook.Exceptions;

public class BadGateway : SharphookHttpException
{
    public BadGateway(HttpResponseMessage response) : base(response) { }
}
