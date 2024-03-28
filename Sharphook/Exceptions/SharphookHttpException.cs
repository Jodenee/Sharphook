namespace Sharphook.Exceptions;

public class SharphookHttpException : SharphookException
{
    public HttpResponseMessage Response { get; private set; }

    public SharphookHttpException(HttpResponseMessage response) : base()
    {
        Response = response;
    }

    public void Dispose()
    {
        Response.Dispose();
    }
}
