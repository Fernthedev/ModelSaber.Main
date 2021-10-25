using GraphQL.Types;

namespace ModelSaber.Database.Models
{
    public class ModelType : ObjectGraphType<Model>
    {
        public ModelType()
        {
            Field(o => o.Date, type: typeof(DateGraphType));
            Field(o => o.Hash);
            Field(o => o.Id);
            Field(o => o.Name);
            Field(o => o.Platform, type: typeof(ByteGraphType));
            Field(o => o.Status, type: typeof(ByteGraphType));
            Field(o => o.Thumbnail);
            Field(o => o.Uuid);
            Field(o => o.DownloadPath);
            Field(o => o.UserId);
        }
    }
}