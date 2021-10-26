using System.Linq;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;

namespace ModelSaber.Database.Models
{
    public sealed class ModelType : ObjectGraphType<Model>
    {
        public ModelType()
        {
            Field(o => o.Date, type: typeof(DateTimeGraphType));
            Field(o => o.Hash, type: typeof(StringGraphType));
            Field(o => o.Id);
            Field(o => o.Name);
            Field(o => o.Platform, type: typeof(PlatformType));
            Field(o => o.Status, type: typeof(StatusType));
            Field(o => o.Thumbnail);
            Field(o => o.Uuid);
            Field(o => o.DownloadPath);
            Field(o => o.UserId);
            Field<ListGraphType<TagType>>("tags", resolve: context => context.Source.Tags.Select(t => t.Tag));
        }
    }

    public class TagType : ObjectGraphType<Tag>
    {
        public TagType()
        {
            Field(o => o.Id);
            Field(o => o.Name);
            Field<ListGraphType<ModelType>>("models", resolve: context => context.Source.ModelTags.Select(t => t.Model));
        }
    }

    public class PlatformType : EnumerationGraphType<Platform>
    {
    }

    public class StatusType : EnumerationGraphType<Status>
    {
    }
}