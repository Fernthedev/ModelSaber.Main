using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQL.Types;
using Newtonsoft.Json;

namespace ModelSaber.Database.Models
{
    public class Model
    {
        public int Id { get; set; }
        public ulong UserId { get; set; }
        public Guid Uuid { get; set; }
        public TypeEnum Type { get; set; }
        public Status Status { get; set; }
        public Platform Platform { get; set; }
        public ThumbnailEnum ThumbnailExt { get; set; }
        public string Name { get; set; }
        public string Hash { get; set; }
        public string Thumbnail => ThumbnailExt == ThumbnailEnum.None ? "images/generic.png" : $"images/{Uuid}.{ThumbnailExt.ToString()[5..].ToLower()}";
        public string DownloadPath => $"models/{Uuid}.{Type.ToString().ToLower()}";
        public DateTime Date { get; set; }
        public virtual ICollection<ModelTag> Tags { get; set; }
        public virtual ICollection<ModelVariation> ModelVariations { get; set; }
        public virtual ICollection<ModelUser> Users { get; set; }
        public virtual ModelVariation ModelVariation { get; set; }
        public virtual User User { get; set; }
    }

    public class ModelVariation
    {
        public int Id { get; set; }
        public int ModelId { get; set; }
        public int ParentModelId { get; set; }
        [JsonIgnore]
        public virtual Model Model { get; set; }
        [JsonIgnore]
        public virtual Model ParentModel { get; set; }
    }

    public class ModelUser
    {
        public int Id { get; set; }
        public int ModelId { get; set; }
        public int UserId { get; set; }
        [JsonIgnore]
        public virtual Model Model { get; set; }
        [JsonIgnore]
        public virtual User User { get; set; }
    }
}
