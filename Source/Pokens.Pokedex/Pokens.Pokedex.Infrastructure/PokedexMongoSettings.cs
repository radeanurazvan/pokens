namespace Pokens.Pokedex.Infrastructure
{
    internal sealed class PokedexMongoSettings
    {
        public string Server { get; private set; }

        public int Port { get; private set; }

        public string Username { get; private set; }

        public string Password { get; private set; }

        public string AuthDatabase { get; private set; }

        public string Database { get; private set; }

        public string ConnectionString => $"mongodb://{Username}:{Password}@{Server}:{Port}/{AuthDatabase}";
    }
}