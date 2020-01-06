using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Pomelo.Kernel.Common;

namespace Pomelo.Kernel.EventStore
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UsePomeloEventStoreEvents(this IApplicationBuilder app, Action<EventStoreEvents> act)
        {
            app.ApplicationServices.ActOnEventStoreEvents(act);
            return app;
        }

        public static IApplicationBuilder UsePomeloEventStoreEventsLogging(this IApplicationBuilder app)
        {
            app.ApplicationServices.AddLoggingToEventStoreEvents();
            return app;
        }

        public static IHost UsePomeloEventStoreEventsLogging(this IHost host)
        {
            host.Services.AddLoggingToEventStoreEvents();
            return host;
        }

        private static void ActOnEventStoreEvents(this IServiceProvider provider, Action<EventStoreEvents> act)
        {
            var events = provider.GetService<EventStoreEvents>();
            act(events);
        }

        private static void AddLoggingToEventStoreEvents(this IServiceProvider provider)
        {
            var logger = provider.GetService<ILogger<EventStoreEvents>>();
            provider.ActOnEventStoreEvents(events =>
            {
                events.OnConnectionFailure += (_, e) => logger.LogError($"Connecting to EventStore failed: {e.Message}");
                events.OnCreateSubscriptionFailure += (_, e) => logger.LogError($"Creating persistent subscription failed: {e.Message}");
                events.OnSubscriptionConnectionFailure += (_, e) => logger.LogError($"Connecting to persistent subscription failed: {e.Message}");
                events.OnSubscriptionDropped += (_, args) => logger.LogError($"Event store subscription with reason {args.Reason} and message {args.Exception.Message}");
                events.OnSubscriptionCreated += (_, aggregateType) => logger.LogInformation($"Subscription created for {aggregateType.Name}");
                events.OnSubscriptionUpdated += (_, args) => logger.LogWarning($"Subscription for {args.Aggregate.Name} updated to {args.Settings.ToJson()}");
                events.OnSubscriptionCreating += (_, aggregateType) => logger.LogInformation($"Creating subscription for {aggregateType.Name}");
                events.OnConnectedToSubscription += (_, subscription) => logger.LogInformation($"Connected to subscription {subscription}");
                events.OnSubscriptionRecreating += (_, aggregate) => logger.LogWarning($"Recreating subscription {aggregate.Name}");
                //events.OnNotificationHandled += (_, notification) => logger.LogInformation($"Handled notification {notification.GetType().GetFriendlyName()} with payload {notification.ToJson()}");
                //events.OnHandleNotificationFailed += (_, args) => logger.LogError($"Failed handling notification {args.Notification.GetType().GetFriendlyName()} due to exception: {args.Exception.Message} with payload {args.Notification.ToJson()}");
                //events.OnNotificationNacked += (_, args) => logger.LogError($"Notification nacked {args.Notification.GetType().GetFriendlyName()} after retries, with exception: {args.Exception.Message} with payload {args.Notification.ToJson()}");
            });
        }

        private static string ToJson<T>(this T subject) => JsonConvert.SerializeObject(subject);
    }
}