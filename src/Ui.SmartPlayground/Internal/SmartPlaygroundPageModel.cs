using System.Text;
using System.Text.Json;

namespace GraphQL.Server.Ui.SmartPlayground.Internal;

// https://docs.microsoft.com/en-us/aspnet/core/mvc/razor-pages/?tabs=netcore-cli
internal sealed class PlaygroundPageModel
{
    private readonly SmartPlaygroundOptions _options;

    public PlaygroundPageModel(SmartPlaygroundOptions options)
    {
        _options = options;
    }

    public string Render()
    {
        using var manifestResourceStream = _options.IndexStream(_options);
        using var streamReader = new StreamReader(manifestResourceStream);

        var headers = new Dictionary<string, object>
        {
            ["Accept"] = "application/json",
            // TODO: investigate, fails in Chrome
            // {
            //   "error": "Response not successful: Received status code 400"
            // }
            //
            // MediaTypeHeaderValue.TryParse(httpRequest.ContentType, out var mediaTypeHeader) from GraphQLHttpMiddleware
            // returns false because of
            // content-type: application/json, application/json

            //["Content-Type"] = "application/json",
        };

        if (_options.Headers?.Count > 0)
        {
            foreach (var item in _options.Headers)
                headers[item.Key] = item.Value;
        }

        var builder = new StringBuilder(streamReader.ReadToEnd())

            .Replace("@Model.GraphQLEndPoint", _options.GraphQLEndPoint.ToString())
            .Replace("@Model.SubscriptionsEndPoint", _options.SubscriptionsEndPoint.ToString())
            .Replace("@Model.GraphQLConfig", JsonSerializer.Serialize<object>(_options.GraphQLConfig!))
            .Replace("@Model.Headers", JsonSerializer.Serialize<object>(headers))
            .Replace("@Model.PlaygroundSettings", JsonSerializer.Serialize<object>(_options.PlaygroundSettings));

        return _options.PostConfigure(_options, builder.ToString());
    }
}