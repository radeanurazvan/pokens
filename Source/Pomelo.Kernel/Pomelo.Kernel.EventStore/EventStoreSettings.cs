﻿using EventStore.ClientAPI.SystemData;

namespace Pomelo.Kernel.EventStore
{
    public sealed class EventStoreSettings
    {
        public string User { get; private set; }

        public string Password { get; private set; }

        public string Server { get; private set; }

        public int Port { get; private set; }

        public string ConnectionString => $"ConnectTo=tcp://{User}:{Password}@{Server}:{Port}; HeartBeatTimeout=500";

        public UserCredentials Credentials => new UserCredentials(User, Password);
    }
}