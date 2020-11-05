using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
//using Helpers;
namespace ImageResizer.Controllers
{
    public class ResizeOption
    {
        /// <summary>
        /// base64 image
        /// </summary>
        public string ImageToResize { get; set; }
        public float Value { get; set; }
        /// <summary>
        /// 1 for ratio conversion and 2 for fixed pixel conversion
        /// </summary>
        public short ConvertionType { get; set; }
    }

    [RoutePrefix("api/ImageResizer")]
    public class ImageResizerController : ApiController
    {

        [HttpPost]
        [Route("Resize")]
        public string Resize(ResizeOption option)
        {
            if(option.ConvertionType == 2)
            {
                return Helpers.ImageHelper.ResizeImageByPixel(option.ImageToResize, Convert.ToInt32(option.Value));
            }
            else
            {
                return Helpers.ImageHelper.ResizeImageByRatio(option.ImageToResize, option.Value);
            }
        }
    }
}