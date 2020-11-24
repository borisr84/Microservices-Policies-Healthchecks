using PicturesCommon;
using PicturesLib.Infra;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RemotePicturesService.Services
{
    public class PicturesService : IPicturesManager
    {
        private HttpClient _httpClient;
        public PicturesService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IList<Picture>> GetPictures()
        {
            var imgUrls = new List<string>
            {
                "https://cdn.apartmenttherapy.info/image/upload/f_jpg,q_auto:eco,c_fill,g_auto,w_1500,ar_16:9/k%2Farchive%2F2816f86937ebc7019a513d858cec8e0c55d38890",
                "https://www.thespruceeats.com/thmb/hl4lkmdLO7tj1eDCsGbakfk97Co=/3088x2055/filters:fill(auto,1)/marinated-top-round-steak-3060302-hero-02-ed071d5d7e584bea82857112aa734a94.jpg"
            };

            IList<Picture> imgs = new List<Picture>();
            foreach (var imgUrl in imgUrls)
            {
                imgs.Add(new Picture
                {
                    Filename = Path.GetFileName(imgUrl),
                    Data = await _httpClient.GetByteArrayAsync(imgUrl)
                });
            }

            return imgs;
        }
    }
}
