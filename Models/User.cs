﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ModelSaber.Database.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BSaber { get; set; }
        // this needs to be parsed to a string since javascript will freak out if its about '2^53 - 1' see https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Number/MAX_SAFE_INTEGER where this runs all the way up to 2^64
        public ulong? DiscordId { get; set; }
        public UserLevel Level { get; set; }
        [JsonIgnore]
        public virtual ICollection<Model> Models { get; set; }
    }

    public enum UserLevel : byte
    {
        Normal,
        Verified,
        Admin
    }
}
