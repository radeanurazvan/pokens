using Pomelo.Kernel.Domain;

namespace Pokens.Training.Domain
{
    public sealed class Pokemon : DocumentEntity
    {
        private Pokemon()
        {
        }

        public string Name { get; private set; }
    }
}