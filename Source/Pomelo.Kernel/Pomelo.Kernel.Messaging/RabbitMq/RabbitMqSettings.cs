namespace Pomelo.Kernel.Messaging
{
    internal sealed class RabbitMqSettings
    {
        public string Server { get; private set; }

        public int Port { get; private set; }

        public string Username { get; private set; }

        public string Password { get; private set; }
    }
}