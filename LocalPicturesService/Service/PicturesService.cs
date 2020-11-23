using Microsoft.AspNetCore.Mvc;
using PicturesLib.Infra;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LocalPicturesService.Service
{
    public class PicturesService : IPicturesManager
    {
        public async Task<IList<byte[]>> GetPictures()
        {
            var images = Directory.GetFiles(Path.Combine(Directory.GetCurrentDirectory(), "Pictures")).ToList();

            IList<byte[]> imageList = new List<byte[]>();
            foreach (var img in images)
            {
                imageList.Add(ImageToByteArray(Image.FromFile(img)));
            }

            return imageList;
        }


        private byte[] ImageToByteArray(System.Drawing.Image img)
        {
            ImageConverter imgCon = new ImageConverter();
            return (byte[])imgCon.ConvertTo(img, typeof(byte[]));
        }
    }
}
