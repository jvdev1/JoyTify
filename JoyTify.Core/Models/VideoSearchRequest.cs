using System;
using System.Collections.Generic;
using System.Text;
using JoyTify.Core.Interfaces;

namespace JoyTify.Core.Models
{
    public class VideoSearchRequest : IVideoListingRequest
    {
        public string ByQuery { get; private set; }
        public string ByVideoId { get; private set; }

        public VideoSearchRequest(string byQuery, string byVideoId)
        {
            ByQuery = byQuery;
            ByVideoId = byVideoId;
        }
    }
}
