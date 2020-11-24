using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PicturesCommon;
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
        private readonly string _imagesPath;
        public PicturesService(IConfiguration configuration)
        {
            _imagesPath = Path.Combine(Directory.GetCurrentDirectory(), configuration["ImagesPath"]);
        }

        public async Task<IList<Picture>> GetPictures()
        {
            var images = Directory.GetFiles(_imagesPath);

            IList<Picture> imageList = new List<Picture>();
            foreach (var imgUrl in images)
            {
                imageList.Add(new Picture
                {
                    Filename = Path.GetFileName(imgUrl),
                    Data = await File.ReadAllBytesAsync(imgUrl)
                });
            }

            return imageList;
        }
    }
}
