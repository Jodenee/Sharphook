namespace Sharphook.Exceptions;

public class Unauthorized : SharphookHttpException
{
    public Unauthorized(HttpResponseMessage response) : base(response) { }
}
