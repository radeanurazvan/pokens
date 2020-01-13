using Pokens.Pokedex.Domain;

namespace Pokens.Pokedex.Business
{
    public sealed class ImageModel
    {
        public ImageModel(Image image)
        {
            this.Id = image.Id;
            this.ImageName = image.ImageName;
            this.ContentImage = image.ContentImage;
        }

        public string Id { get; set; }

        public string ImageName { get; set; }

        public byte[] ContentImage { get; set; }
    }
}
