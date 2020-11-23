using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PicturesLib.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RemotePicturesService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PicturesController : ControllerBase
    {
        private readonly IPicturesManager _picManager;
        public PicturesController(IPicturesManager picManager)
        {
            _picManager = picManager;
        }

        [HttpGet]
        public async Task<IList<byte[]>> GetPictures()
        {
            return await _picManager.GetPictures();
        }
    }
}
