using System;
using EventStore.ClientAPI;
using Pomelo.Kernel.Domain;

namespace Pomelo.Kernel.EventStore
{
    public sealed class EventStoreEvents
    {
        public event EventHandler<Exception> OnConnectionFailure;
     
        public event EventHandler<Exception> OnCreateSubscriptionFailure;

        public event EventHandler<Exception> OnSubscriptionConnectionFailure;

        public event EventHandler<EventStoreSubscriptionDroppedEventArgs> OnSubscriptionDropped;
        
        public event EventHandler<Type> OnSubscriptionCreating;

        public event EventHandler<Type> OnSubscriptionCreated;

        public event EventHandler<Type> OnSubscriptionRecreating;
        
        public event EventHandler<EventStoreSubscriptionUpdatedEventArgs> OnSubscriptionUpdated;

        public event EventHandler<string> OnConnectedToSubscription;

        public event EventHandler<EventStoreNotificationFailureEventArgs> OnHandleNotificationFailed;

        public event EventHandler<EventStoreNotificationFailureEventArgs> OnNotificationNacked;

        public event EventHandler<object> OnNotificationHandled;

        internal void RaiseConnectionFailure(Exception e)
        {
            OnConnectionFailure?.Invoke(this, e);
        }
        
        internal void RaiseSubscriptionConnectionFailure(Exception e)
        {
            OnSubscriptionConnectionFailure?.Invoke(this, e);
        }

        internal void RaiseCreateSubscriptionFailure(Exception e)
        {
            OnCreateSubscriptionFailure?.Invoke(this, e);
        }

        internal void RaiseSubscriptionDropped(SubscriptionDropReason reason, Exception e)
        {
            OnSubscriptionDropped?.Invoke(this, new EventStoreSubscriptionDroppedEventArgs(reason, e));
        }

        internal void RaiseSubscriptionCreating<T>()
        {
            OnSubscriptionCreating?.Invoke(this, typeof(T));
        }

        internal void RaiseSubscriptionCreated<T>()
        {
            OnSubscriptionCreated?.Invoke(this, typeof(T));
        }

        internal void RaiseSubscriptionUpdated<T>(PersistentSubscriptionSettings settings)
        {
            OnSubscriptionUpdated?.Invoke(this, new EventStoreSubscriptionUpdatedEventArgs(typeof(T), settings));
        }

        internal void RaiseSubscriptionRecreating<T>()
        {
            OnSubscriptionRecreating?.Invoke(this, typeof(T));
        }

        internal void RaiseConnectedToSubscription(string subscription)
        {
            OnConnectedToSubscription?.Invoke(this, subscription);
        }

        internal void RaiseHandleNotificationFailed(Exception exception, object notification)
        {
            OnHandleNotificationFailed?.Invoke(this, new EventStoreNotificationFailureEventArgs(exception, notification));
        }

        internal void RaiseNotificationNacked(Exception exception, object notification)
        {
            OnNotificationNacked?.Invoke(this, new EventStoreNotificationFailureEventArgs(exception, notification));
        }

        internal void RaiseNotificationHandled(object notification)
        {
            OnNotificationHandled?.Invoke(this, notification);
        }
    }

    public sealed class EventStoreSubscriptionDroppedEventArgs
    {
        private EventStoreSubscriptionDroppedEventArgs()
        {
        }

        internal EventStoreSubscriptionDroppedEventArgs(SubscriptionDropReason reason, Exception exception)
            : this()
        {
            Reason = reason;
            Exception = exception;
        }

        public SubscriptionDropReason Reason { get; private set; }

        public Exception Exception { get; private set; }
    }

    public sealed class EventStoreSubscriptionUpdatedEventArgs
    {
        public EventStoreSubscriptionUpdatedEventArgs(Type aggregate, PersistentSubscriptionSettings settings)
        {
            Aggregate = aggregate;
            Settings = settings;
        }

        public Type Aggregate { get; private set; }

        public PersistentSubscriptionSettings Settings { get; private set; }
    }

    public sealed class EventStoreNotificationFailureEventArgs
    {
        public EventStoreNotificationFailureEventArgs(Exception exception, object notification)
        {
            Exception = exception;
            Notification = notification;
        }

        public Exception Exception { get; private set; }

        public object Notification { get; private set; }
    }
}