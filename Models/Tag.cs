using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelSaber.Database.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public Guid CursorId { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public virtual ICollection<ModelTag> ModelTags { get; set; }
    }

    public class ModelTag
    {
        public int Id { get; set; }
        public int ModelId { get; set; }
        public int TagId { get; set; }
        public virtual Tag Tag { get; set; }
        [JsonIgnore]
        public virtual Model Model { get; set; }
    }
}
