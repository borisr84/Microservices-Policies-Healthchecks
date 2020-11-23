using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PicturesLib.Infra
{
    public interface IPicturesManager
    {
        Task<IList<byte[]>> GetPictures();
    }
}
