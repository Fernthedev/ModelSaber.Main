#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GraphQL.Builders;
using GraphQL.Types;
using GraphQL.Types.Relay.DataObjects;
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
            Field<ListGraphType<TagType>>("tags", resolve: context => context.Source?.Tags.Select(t => t.Tag));
            Field<ListGraphType<UserType>>("users", resolve: context => context.Source?.Users.Select(t => t.User));
            Field<UserType>("mainUser", resolve: context => context.Source?.User);
        }
    }

    public sealed class TagType : ObjectGraphType<Tag>
    {
        public TagType()
        {
            Field(o => o.Id);
            Field(o => o.Name);
            Connection<ModelType>()
                .Name("models")
                .Description("Model list")
                .Bidirectional()
                .PageSize(100)
                .ResolveAsync(ResolveModelConnectionAsync);
        }

        private async Task<object?> ResolveModelConnectionAsync(IResolveConnectionContext<Tag> context)
        {
            var first = context.First;
            var afterCursor = Cursor.FromCursorDateTime(context.After);
            var last = context.Last;
            var beforeCursor = Cursor.FromCursorDateTime(context.Before);
            var cancellationToken = context.CancellationToken;

            var getModelsTask = GetModelAsync(context, first, afterCursor, last, beforeCursor, cancellationToken);
            var getNextPageTask = GetModelsNextPageAsync(context, first, afterCursor, cancellationToken);
            var getPreviousPageTask = GetModelPreviousPageAsync(context, last, beforeCursor, cancellationToken);
            var totalCountTask = Task.FromResult(context.Source?.ModelTags.Count);

            await Task.WhenAll(getModelsTask, getNextPageTask, getPreviousPageTask, totalCountTask).ConfigureAwait(false);
            var models = await getModelsTask.ConfigureAwait(false);
            var nextPage = await getNextPageTask.ConfigureAwait(false);
            var previousPage = await getPreviousPageTask.ConfigureAwait(false);
            var totalCount = await totalCountTask.ConfigureAwait(false);
            var (firstCursor, lastCursor) = Cursor.GetFirstAndLastCursor(models, x => x?.Date);

            return new Connection<Model>
            {
                Edges = models?.Select(x => new Edge<Model>
                {
                    Cursor = Cursor.ToCursor(x.Date),
                    Node = x
                }).ToList(),
                PageInfo = new PageInfo
                {
                    HasNextPage = nextPage ?? false,
                    HasPreviousPage = previousPage ?? false,
                    StartCursor = firstCursor,
                    EndCursor = lastCursor
                },
                TotalCount = totalCount
            };
        }

        private Task<bool?> GetModelPreviousPageAsync(IResolveConnectionContext<Tag> context, int? last, DateTime? beforeCursor, CancellationToken cancellationToken)
        {
            return Task.FromResult(context.Source?.ModelTags.Select(t => t.Model)
                .If(beforeCursor.HasValue, x => x.Where(y => y.Date < beforeCursor!.Value))
                .If(last.HasValue, x => x.TakeLast(last!.Value)).Any());
        }

        private Task<bool?> GetModelsNextPageAsync(IResolveConnectionContext<Tag> context, int? first, DateTime? afterCursor, CancellationToken cancellationToken)
        {
            return Task.FromResult(context.Source?.ModelTags.Select(t => t.Model)
                .If(afterCursor.HasValue, x => x.Where(y => y.Date > afterCursor!.Value)).
                If(first.HasValue, x => x.Take(first!.Value)).Any());
        }

        private Task<List<Model>?> GetModelAsync(IResolveConnectionContext<Tag> context, int? first, DateTime? afterCursor, int? last, DateTime? beforeCursor, CancellationToken cancellationToken)
        {
            return Task.FromResult(last.HasValue ? 
                context.Source?.ModelTags.Select(t => t.Model)
                    .If(afterCursor.HasValue, x => x.Where(y => y.Date > afterCursor!.Value))
                    .TakeLast(last!.Value).ToList() : 
                context.Source?.ModelTags.Select(t => t.Model)
                    .If(beforeCursor.HasValue, x => x.Where(y => y.Date < beforeCursor!.Value))
                    .If(first.HasValue, x => x.TakeLast(first!.Value)).ToList());
        }
    }

    public class PlatformType : EnumerationGraphType<Platform>
    {
    }

    public class StatusType : EnumerationGraphType<Status>
    {
    }

    public class UserLevelType : EnumerationGraphType<UserLevel>
    {
    }

    public sealed class UserType : ObjectGraphType<User>
    {
        public UserType()
        {
            Field(o => o.BSaber);
            Field(o => o.DiscordId, type: typeof(ULongGraphType));
            Field(o => o.Level, type: typeof(UserLevelType));
            Field(o => o.Name);
            Connection<ModelType>()
                .Name("models")
                .Description("Model list")
                .Bidirectional()
                .PageSize(100)
                .ResolveAsync(ResolveModelConnectionAsync);
        }

        private async Task<object?> ResolveModelConnectionAsync(IResolveConnectionContext<User> context)
        {
            var first = context.First;
            var afterCursor = Cursor.FromCursorDateTime(context.After);
            var last = context.Last;
            var beforeCursor = Cursor.FromCursorDateTime(context.Before);
            var cancellationToken = context.CancellationToken;

            var getModelsTask = GetModelAsync(context, first, afterCursor, last, beforeCursor, cancellationToken);
            var getNextPageTask = GetModelsNextPageAsync(context, first, afterCursor, cancellationToken);
            var getPreviousPageTask = GetModelPreviousPageAsync(context, last, beforeCursor, cancellationToken);
            var totalCountTask = Task.FromResult(context.Source?.Models.Count);

            await Task.WhenAll(getModelsTask, getNextPageTask, getPreviousPageTask, totalCountTask).ConfigureAwait(false);
            var models = await getModelsTask.ConfigureAwait(false);
            var nextPage = await getNextPageTask.ConfigureAwait(false);
            var previousPage = await getPreviousPageTask.ConfigureAwait(false);
            var totalCount = await totalCountTask.ConfigureAwait(false);
            var (firstCursor, lastCursor) = Cursor.GetFirstAndLastCursor(models, x => x?.Date);

            return new Connection<Model>
            {
                Edges = models?.Select(x => new Edge<Model>
                {
                    Cursor = Cursor.ToCursor(x.Date),
                    Node = x
                }).ToList(),
                PageInfo = new PageInfo
                {
                    HasNextPage = nextPage ?? false,
                    HasPreviousPage = previousPage ?? false,
                    StartCursor = firstCursor,
                    EndCursor = lastCursor
                },
                TotalCount = totalCount
            };
        }

        private Task<bool?> GetModelPreviousPageAsync(IResolveConnectionContext<User> context, int? last, DateTime? beforeCursor, CancellationToken cancellationToken)
        {
            return Task.FromResult(context.Source?.Models.Select(t => t.Model)
                .If(beforeCursor.HasValue, x => x.Where(y => y.Date < beforeCursor!.Value))
                .If(last.HasValue, x => x.TakeLast(last!.Value)).Any());
        }

        private Task<bool?> GetModelsNextPageAsync(IResolveConnectionContext<User> context, int? first, DateTime? afterCursor, CancellationToken cancellationToken)
        {
            return Task.FromResult(context.Source?.Models.Select(t => t.Model)
                .If(afterCursor.HasValue, x => x.Where(y => y.Date > afterCursor!.Value)).
                If(first.HasValue, x => x.Take(first!.Value)).Any());
        }

        private Task<List<Model>?> GetModelAsync(IResolveConnectionContext<User> context, int? first, DateTime? afterCursor, int? last, DateTime? beforeCursor, CancellationToken cancellationToken)
        {
            return Task.FromResult(last.HasValue ? 
                context.Source?.Models.Select(t => t.Model)
                    .If(afterCursor.HasValue, x => x.Where(y => y.Date > afterCursor!.Value))
                    .TakeLast(last!.Value).ToList() : 
                context.Source?.Models.Select(t => t.Model)
                    .If(beforeCursor.HasValue, x => x.Where(y => y.Date < beforeCursor!.Value))
                    .If(first.HasValue, x => x.TakeLast(first!.Value)).ToList());
        }
    }
}