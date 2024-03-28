namespace Sharphook.Exceptions;

public class Forbidden : SharphookHttpException
{
    public Forbidden(HttpResponseMessage response) : base(response) { }
}
