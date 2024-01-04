using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Teams;
using Microsoft.Bot.Schema;
using Microsoft.Bot.Schema.Teams;
using AdaptiveCards;
using Newtonsoft.Json.Linq;
using Microsoft.Bot.Connector;
using static System.Net.WebRequestMethods;

namespace MsgextActionSrchData.Action;

public class ActionApp : TeamsActivityHandler
{
    private readonly string _adaptiveCardFilePath = Path.Combine(".", "Resources", "helloWorldCard.json");
    // Action.
    protected override async Task<MessagingExtensionActionResponse> OnTeamsMessagingExtensionSubmitActionAsync(ITurnContext<IInvokeActivity> turnContext, MessagingExtensionAction action, CancellationToken cancellationToken)
    {
        //if (action.CommandId == "selectItem")
        //{
        //    CardAction openTaskModueleExtextesion = new CardAction();
        //    openTaskModueleExtextesion.Type = "openUrl";
        //    openTaskModueleExtextesion.Value = "http://localhost:5130/initialaction"; // ToDo
        //    return new MessagingExtensionActionResponse
        //    {
        //        ComposeExtension = new MessagingExtensionResult
        //        {
        //            Type = "config",                    
        //            SuggestedActions = new()
        //            {
        //                Actions = new()
        //                {
        //                    openTaskModueleExtextesion
        //                }
        //            }
                      
        //    ]
        //        }
        //    };
        //}
        //else
        //{
            // The user has chosen to create a card by choosing the 'Create Card' context menu command.
            var actionData = ((JObject)action.Data).ToObject<CardResponse>();
            var templateJson = await System.IO.File.ReadAllTextAsync(_adaptiveCardFilePath, cancellationToken);
            var template = new AdaptiveCards.Templating.AdaptiveCardTemplate(templateJson);
            var adaptiveCardJson = template.Expand(new { title = actionData.Title ?? "", subTitle = actionData.SubTitle ?? "", text = actionData.Text ?? "" });
            var adaptiveCard = AdaptiveCard.FromJson(adaptiveCardJson).Card;
            var attachments = new MessagingExtensionAttachment()
            {
                ContentType = AdaptiveCard.ContentType,
                Content = adaptiveCard
            };

            return new MessagingExtensionActionResponse
            {
                ComposeExtension = new MessagingExtensionResult
                {
                    Type = "result",
                    AttachmentLayout = "list",
                    Attachments = new[] { attachments }
                }
            };
        //}
        
    }
}

internal class CardResponse
{
    public string Title { get; set; }
    public string SubTitle { get; set; }
    public string Text { get; set; }
}