using PicturesLib.Infra;
using System;
using System.Collections.Generic;
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

        public async Task<IList<byte[]>> GetPictures()
        {
            var img1 = await _httpClient.GetAsync("https://images.unsplash.com/photo-1602524210257-90ba1ca81ea3?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=1350&q=80");
            var img2 = await _httpClient.GetAsync("https://images.unsplash.com/photo-1606114175460-31ba3462a098?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=crop&w=1352&q=80");

            var img1ByteArr = await img1.Content.ReadAsByteArrayAsync();
            var img2ByteArr = await img2.Content.ReadAsByteArrayAsync();

            IList<byte[]> imgs = new List<byte[]>();
            imgs.Add(img1ByteArr);
            imgs.Add(img2ByteArr);

            return imgs;
        }
    }
}
