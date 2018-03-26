using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace JoyTify.Core.DBModels
{
    public class DBLastSearch
    {
        [Key]
        public string Term { get; set; }

        public override string ToString()
        {
            return Term;
        }
    }
}