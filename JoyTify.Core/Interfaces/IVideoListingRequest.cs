using System;
using System.Collections.Generic;
using System.Text;

namespace JoyTify.Core.Interfaces
{
    public interface IVideoListingRequest
    {
        string ByQuery { get; }
        string ByVideoId { get; }
    }
}
