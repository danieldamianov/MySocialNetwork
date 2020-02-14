using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Controllers.ImageConvertingFunctionality
{
    public class ImageConverter
    {
        public string ConvertByteArrayToString(byte[] image)
        {
            string imgeBase64Data = Convert.ToBase64String(image);
            return string.Format("data:image/jpg;base64,{0}", imgeBase64Data);
        }

        internal object ConvertByteArrayToString(Func<byte[], string, FileContentResult> file)
        {
            throw new NotImplementedException();
        }
    }
}
