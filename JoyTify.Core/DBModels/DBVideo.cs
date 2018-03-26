using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JoyTify.Core.DBModels
{
    public class DBVideo
    {
        [Key]
        public string Title { get; set; }
        public string Thumb { get; set; }
        public string VideoId { get; set; }

        public string ChannelId { get; set; }
        public string ChannelTitle { get; set; }
        public string ChannelThumb { get; set; }

        public DateTime Date { get; set; }
        public string Description { get; set; }
        public ulong ViewCount { get; set; }
        public string Duration { get; set; }
        public double DurationM { get; set; }

        public override string ToString()
        {
            return Title;
        }
    }
}