using Sharphook.Exceptions;
using System.Net;
using System.Net.Http.Headers;

namespace Sharphook.Utility.Helpers;

internal record RatelimitInformation
{
	public int Limit { get; private set; }
	public int Remaining { get; private set; }
	public int ResetAfterInMilliseconds { get; private set; }
	public int SafeResetAfterInMilliseconds { get; private set; }
	public DateTimeOffset Reset { get; private set; }
	public string Bucket { get; private set; }

	public RatelimitInformation(
		int limit,
		int remaining,
		int resetAfter,
		DateTimeOffset reset,
		string bucket)
	{
		Limit = limit;
		Remaining = remaining;
		ResetAfterInMilliseconds = resetAfter * 1_000;
		SafeResetAfterInMilliseconds = (resetAfter * 1_000) + 1_000;
		Reset = reset;
		Bucket = bucket;
	}
}

internal record RatelimitedInformation
{
	public bool Global { get; private set; }
	public string Scope { get; private set; }
	public int RetryAfterInMilliseconds { get; private set; }
	public int SafeRetryAfterInMilliseconds { get; private set; }

	public RatelimitedInformation(
		string scope,
		int retryAfter)
	{
		Scope = scope;
		RetryAfterInMilliseconds = retryAfter * 1_000;
		SafeRetryAfterInMilliseconds = (retryAfter * 1_000) + 1_000;
		Global = scope == "global";
	}
}

internal static class HttpHelper
{
	public static void ThrowFromStatusCode(HttpResponseMessage httpResponseMessage)
	{
		throw httpResponseMessage.StatusCode switch
		{
			HttpStatusCode.BadRequest => new BadRequest(httpResponseMessage),
			HttpStatusCode.Unauthorized => new Unauthorized(httpResponseMessage),
			HttpStatusCode.Forbidden => new Forbidden(httpResponseMessage),
			HttpStatusCode.NotFound => new NotFound(httpResponseMessage),
			HttpStatusCode.BadGateway => new BadGateway(httpResponseMessage),
			_ => new SharphookHttpException(httpResponseMessage)
		};
	}

	public static RatelimitInformation? ParseRatelimitInformation(HttpResponseHeaders headers)
	{
		bool limitExists = headers.TryGetValues("X-RateLimit-Limit", out IEnumerable<string>? rawLimit);
		bool remainingExists = headers.TryGetValues("X-RateLimit-Remaining", out IEnumerable<string>? rawRemaining);
		bool resetExists = headers.TryGetValues("X-Ratelimit-Reset", out IEnumerable<string>? rawReset);
		bool resetAfterExists = headers.TryGetValues("X-RateLimit-Reset-After", out IEnumerable<string>? rawResetAfter);
		bool bucketExists = headers.TryGetValues("X-RateLimit-Bucket", out IEnumerable<string>? rawBucket);

		if (!limitExists || !remainingExists || !resetExists || !resetAfterExists || !bucketExists)
			return null;

		int limit = Convert.ToInt32(rawLimit!.First());
		int remaining = Convert.ToInt32(rawRemaining!.First());
		DateTimeOffset reset = DateTimeOffset.FromUnixTimeSeconds(
			Convert.ToInt64(Convert.ToSingle(rawReset!.First())));
		int resetAfter = Convert.ToInt32(Convert.ToSingle(rawResetAfter!.First()));
		string bucket = rawBucket!.First();

		return new RatelimitInformation(limit, remaining, resetAfter, reset, bucket);
	}

	public static RatelimitedInformation? ParseRatelimitedInformation(HttpResponseHeaders headers)
	{
		bool scopeExists = headers.TryGetValues("X-RateLimit-Limit", out IEnumerable<string>? rawScope);
		bool retryAfterExists = headers.TryGetValues("X-RateLimit-Remaining", out IEnumerable<string>? rawRetryAfter);

		if (!scopeExists || !retryAfterExists)
			return null;

		string scope = rawScope!.First();
		int retryAfter = Convert.ToInt32(Convert.ToSingle(rawRetryAfter!.First()));

		return new RatelimitedInformation(scope, retryAfter);
	}
}
