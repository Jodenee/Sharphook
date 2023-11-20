using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http.Headers;
using static System.Formats.Asn1.AsnWriter;

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

	public class TooManyRequests : SharphookHttpException
	{
		public byte? Limit { get; private set; }
		public byte? Remaining { get; private set; }
		public DateTime? Reset { get; private set; }
		public float ResetAfter { get; private set; }
        public int ResetAfterInMs { get; private set; }
		public int SafeResetAfterInMs { get; private set; }
        public string? Bucket { get; private set; }
		public bool? Global { get; private set; }
		public string Scope { get; private set; }

		public TooManyRequests(HttpResponseMessage response) : base(response) 
		{
			HttpResponseHeaders responceHeaders = response.Headers;

			string scope = responceHeaders.GetValues("X-RateLimit-Scope").First();
            string resetAfter = responceHeaders.GetValues("X-RateLimit-Reset-After").First();

            Scope = scope;
            ResetAfter = Convert.ToSingle(resetAfter);
            ResetAfterInMs = Convert.ToInt32(ResetAfter * 1000);
            SafeResetAfterInMs = ResetAfterInMs + 1000;

            if (scope == "user")
			{
				string limit = responceHeaders.GetValues("X-RateLimit-Limit").First();
				string remaining = responceHeaders.GetValues("X-RateLimit-Remaining").First();
				string reset = responceHeaders.GetValues("X-RateLimit-Reset").First();
				string bucket = responceHeaders.GetValues("X-RateLimit-Bucket").First();

				DateTimeOffset resetOffset = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(reset));

				Limit = Convert.ToByte(limit);
				Remaining = Convert.ToByte(remaining);
				Reset = resetOffset.DateTime;
                Bucket = Convert.ToString(bucket);
            } 
			else if (scope == "shared")
			{
				string limit = responceHeaders.GetValues("X-RateLimit-Limit").First();
				string remaining = responceHeaders.GetValues("X-RateLimit-Remaining").First();
				string reset = responceHeaders.GetValues("X-RateLimit-Reset").First();
				string bucket = responceHeaders.GetValues("X-RateLimit-Bucket").First();

				DateTimeOffset resetOffset = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(reset));

				Limit = Convert.ToByte(limit);
				Remaining = Convert.ToByte(remaining);
				Reset = resetOffset.DateTime;
                Bucket = Convert.ToString(bucket);
			}
			else
			{
				string global = responceHeaders.GetValues("X-RateLimit-Global").First();

                Global = Convert.ToBoolean(global);
            }
		}
	}

	public class BadGateway : SharphookHttpException
	{
		public BadGateway(HttpResponseMessage response) : base(response) { }
	}
}
