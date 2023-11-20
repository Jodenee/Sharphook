using System.Net.Http.Headers;

namespace Sharphook.Models
{
	public class RatelimitInfo
	{
		public byte Limit { get; private set; }
		public byte Remaining { get; private set; }
        public DateTime Reset { get; private set; }
        public float ResetAfter { get; private set; }
        public int ResetAfterInMs { get; private set; }
		public int SafeResetAfterInMs { get; private set; }
        public string Bucket { get; private set; }

        public RatelimitInfo(HttpResponseHeaders responceHeaders) 
		{
			string limit = responceHeaders.GetValues("X-RateLimit-Limit").First();
			string remaining = responceHeaders.GetValues("X-RateLimit-Remaining").First();
			string reset = responceHeaders.GetValues("X-RateLimit-Reset").First();
			string resetAfter = responceHeaders.GetValues("X-RateLimit-Reset-After").First();
			string bucket = responceHeaders.GetValues("X-RateLimit-Bucket").First();

			DateTimeOffset resetOffset = DateTimeOffset.FromUnixTimeSeconds(Convert.ToInt64(reset));

			Limit = Convert.ToByte(limit);
			Remaining = Convert.ToByte(remaining);
			Reset = resetOffset.DateTime;
            ResetAfter = Convert.ToSingle(resetAfter);
            ResetAfterInMs = Convert.ToInt32(ResetAfter * 1000);
            SafeResetAfterInMs = ResetAfterInMs + 1000;
            Bucket = bucket;
		}
	}
}
