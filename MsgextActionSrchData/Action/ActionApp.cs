using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Teams;
using Microsoft.Bot.Schema;
using Microsoft.Bot.Schema.Teams;
using AdaptiveCards;
using Newtonsoft.Json.Linq;
using MsgextActionSrchData.Model;

namespace MsgextActionSrchData.Action;

public class ActionApp : TeamsActivityHandler
{
    private readonly string _adaptiveBaseCardFilePath = Path.Combine(".", "Resources");
    // Action.
    protected override async Task<MessagingExtensionActionResponse> OnTeamsMessagingExtensionSubmitActionAsync(ITurnContext<IInvokeActivity> turnContext, MessagingExtensionAction action, CancellationToken cancellationToken)
    {
        if (action.CommandId == "selectItem")
        {
            string _adaptiveCardFilePath = Path.Combine(_adaptiveBaseCardFilePath, "ProposedOrder.json");
            var actionData = ((JObject)action.Data).ToObject<Product>();
            string prodId = actionData.Id ?? "";
            string prodName = actionData.Name ?? "";
            string prodOrders = actionData.Orders.ToString() ?? "";
            string prodOrderable = actionData.Orderable.ToString() ?? "false";

            var templateJson = await System.IO.File.ReadAllTextAsync(_adaptiveCardFilePath, cancellationToken);
            var template = new AdaptiveCards.Templating.AdaptiveCardTemplate(templateJson);
            var adaptiveCardJson = template.Expand(new { ID = prodId, Name = prodName, Orders = prodOrders });
            var adaptiveCard = AdaptiveCard.FromJson(adaptiveCardJson).Card;
            if (prodOrderable != "false")
            {
                adaptiveCard.Actions.Add(new AdaptiveExecuteAction()
                {
                    Title = "Order",
                    Verb = "order",
                     = "Action.Execute",
                     Data = actionData
                });
            }
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
        }
        else
        {
            // The user has chosen to create a card by choosing the 'Create Card' context menu command.
            string _adaptiveCardFilePath = Path.Combine(_adaptiveBaseCardFilePath, "helloWorldCard.json");
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
        }

    }

    protected override async Task<AdaptiveCardInvokeResponse> OnAdaptiveCardInvokeAsync(ITurnContext<IInvokeActivity> turnContext, AdaptiveCardInvokeValue invokeValue, CancellationToken cancellationToken)
    {
        string dataJson = invokeValue.Action.Data.ToString();
        string verb = invokeValue.Action.Verb;
        turnContext.SendActivityAsync("Response received");
        return new AdaptiveCardInvokeResponse
        {
            StatusCode = 200
        };
        //return base.OnAdaptiveCardInvokeAsync(turnContext, invokeValue, cancellationToken);
    }

    internal class CardResponse
{
    public string Title { get; set; }
    public string SubTitle { get; set; }
    public string Text { get; set; }
}
}
