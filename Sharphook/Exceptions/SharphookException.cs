using System.Net.Http.Headers;

namespace Sharphook.Exceptions
{
	public class SharphookException : Exception
	{
		public SharphookException() : base() {}
	}

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

	public class BadRequest : SharphookHttpException
	{
		public BadRequest(HttpResponseMessage response) : base(response) {}
	}

	public class Unauthorized : SharphookHttpException
	{
		public Unauthorized(HttpResponseMessage response) : base(response) { }
	}

	public class Forbidden : SharphookHttpException
	{
		public Forbidden(HttpResponseMessage response) : base(response) { }
	}

	public class NotFound : SharphookHttpException
	{
		public NotFound(HttpResponseMessage response) : base(response) { }
	}

	public class BadGateway : SharphookHttpException
	{
		public BadGateway(HttpResponseMessage response) : base(response) { }
	}
}
