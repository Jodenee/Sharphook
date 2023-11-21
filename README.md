# Sharphook

## Overview

Sharphook is an asynchronous object oriented API wrapper for Discord's webhook API.

## Examples

### Sending a message 

```c#
WebhookClient webhookClient = new WebhookClient();
PartialWebhook webhook = webhookClient.GetPartialWebhook(123, "Token");

await webhook.SendMessage("Hello, world!");
```

### Sending a message to a thread

```c#
WebhookClient webhookClient = new WebhookClient();
PartialWebhook webhook = webhookClient.GetPartialWebhook(123, "Token");
ulong threadId = 123;

await webhook.SendMessageInThread(threadId, "Hello, world!");
```

### Getting a Message responce

```c#
WebhookClient webhookClient = new WebhookClient();
PartialWebhook webhook = webhookClient.GetPartialWebhook(123, "Token");

AbstractedResponce<Message> responce =  await webhook.SendMessage("Hello, world!", null, true);
Message message = responce.ResponceObject!;

Console.WriteLine(message.Id);
```

### Editing a webhook
```c# 
WebhookClient webhookClient = new WebhookClient();
PartialWebhook webhook = webhookClient.GetPartialWebhook(123, "Token");

// Edit webhook name
await webhook.Edit("New name");

// Edit webhook avatar
await webhook.Edit(@"newAvatar.png", FileContentType.PNG);
```

### Error handling 

```c#
WebhookClient webhookClient = new WebhookClient();
PartialWebhook webhook = webhookClient.GetPartialWebhook(123, "Token");

try
{
	await webhook.SendMessage("");
}
catch (SharphookHttpException exception)
{
	Console.WriteLine($"{(int)exception.Response.StatusCode} {exception.Response.ReasonPhrase}"); // 400 Bad Request
}
```

## Libraries used

*  [Json.NET](http://james.newtonking.com/json)

## License

Sharphook is  under the [MIT license](LICENSE.txt).

## Disclaimers

* Sharphook is just maintained for fun.
* Sharphook is not mature enough to be used in production yet.