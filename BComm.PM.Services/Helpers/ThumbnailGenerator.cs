using Microsoft.AspNetCore.Hosting;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;

namespace BComm.PM.Services.Helpers
{
    public class ThumbnailGenerator
    {
        public string FileName
        {
            get
            {
                return _imageInfo.Name + "_thumb" + _imageInfo.Extention;
            }
        }

        private readonly ImageInfo _imageInfo;
        private readonly int boxHeight = 600;

        public ThumbnailGenerator(ImageInfo imageInfo)
        {
            _imageInfo = imageInfo;
        }

        public void Generate()
        {
            using(var ms = new MemoryStream(_imageInfo.FileBytes))
            {
                var image = Image.FromStream(ms);

                double ratio = (double)boxHeight / image.Height;
                int newWidth = (int)(image.Width * ratio);
                int newHeight = (int)(image.Height * ratio);
                Bitmap newImage = new Bitmap(newWidth, newHeight);
                using (Graphics g = Graphics.FromImage(newImage))
                {
                    g.CompositingQuality = CompositingQuality.HighQuality;
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.DrawImage(image, 0, 0, newWidth, newHeight);
                }

                image.Dispose();
                newImage.Save(Path.Combine(_imageInfo.Directory, FileName));
            }
        }
    }
}
