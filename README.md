# Sharphook

## Overview

Sharphook is an asynchronous object oriented API wrapper for Discord's webhook API.

## .NET Compatibility


| .NET Version   | Compatible? |
|:--------------:|:-----------:|
| .NET 8         | **YES**	   |
| .NET 7         | **YES**     |
| .NET 6         | **YES**     |


## Examples

### Sending a message 

```c#
WebhookClient webhookClient = new WebhookClient();
PartialWebhook webhook = webhookClient.GetPartialWebhook(123, "Token");

await webhook.SendMessageAsync("Hello, world!");
```

### Sending a message to a thread

```c#
WebhookClient webhookClient = new WebhookClient();
PartialWebhook webhook = webhookClient.GetPartialWebhook(123, "Token");
ulong threadId = 123;

await webhook.SendMessageInThreadAsync(threadId, "Hello, world!");
```

### Obtaining a message object

```c#
WebhookClient webhookClient = new WebhookClient();
PartialWebhook webhook = webhookClient.GetPartialWebhook(123, "Token");

Message message = await webhook.SendMessageAsync("Hello, world!", null, true);

Console.WriteLine(message.Id);
```

### Editing a webhook
```c# 
WebhookClient webhookClient = new WebhookClient();
PartialWebhook webhook = webhookClient.GetPartialWebhook(123, "Token");

// Edit webhook name
await webhook.EditWebhookNameAsync("New name");

// Edit webhook avatar
await webhook.EditWebhookAvatarAsync(@"newAvatar.png", FileContentType.PNG);
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

## Installation

* Go to [releases](https://github.com/Jodenee/Sharphook/releases)
* Find the latest version
* Download the folder with your .NET version
* Unzip the folder
* Add Sharphook.dll as a reference to your project

(Nuget support coming in v1.0.0)

## Dependencies

*  [Json.NET](http://james.newtonking.com/json)

## License

Sharphook is  under the [MIT license](LICENSE.txt).

## Disclaimers

* Sharphook is just maintained for fun.
* Sharphook is in alpha.