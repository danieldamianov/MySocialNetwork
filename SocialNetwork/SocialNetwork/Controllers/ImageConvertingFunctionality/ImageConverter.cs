using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Controllers.ImageConvertingFunctionality
{
    public class ImageConverter
    {
        public string ConvertByteArratToString(byte[] image)
        {
            string imgeBase64Data = Convert.ToBase64String(image);
            return string.Format("data:image/jpg;base64,{0}", imgeBase64Data);
        }
    }
}
