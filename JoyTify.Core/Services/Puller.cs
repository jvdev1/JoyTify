using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeExplode;
using YoutubeExplode.Models;
using YoutubeExplode.Models.ClosedCaptions;
using YoutubeExplode.Models.MediaStreams;

namespace JoyTify.Core.Services
{
    public class Puller : IPuller
    {
        public static async Task<string> New_Pull(string vidId)
        {
            YoutubeClient client = new YoutubeClient();
            MediaStreamInfoSet videoInfo = await client.GetVideoMediaStreamInfosAsync(vidId);


            string RS = null;

            try { RS = videoInfo.Audio.OrderBy(p => p.Bitrate).First().Url; } catch (Exception ex) { }

            return RS;
        }
    }
}

