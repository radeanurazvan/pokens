namespace Pokens.Pokedex.Domain
{
    public sealed class Image : PokedexEntity
    {
        public Image(string imageName, byte[] contectImage)
        {
            this.ImageName = imageName;
            this.ContentImage = contectImage;
        }

        public string ImageName { get; set; }

        public byte[] ContentImage { get; set; }
    }
}
