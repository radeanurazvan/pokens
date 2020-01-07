using Pomelo.Kernel.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pokens.Training.Domain
{
    public sealed class Image : DocumentEntity
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
