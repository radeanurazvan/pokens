namespace Pokens.Trainers.Api
{
    public sealed class RegisterTrainerModel
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}