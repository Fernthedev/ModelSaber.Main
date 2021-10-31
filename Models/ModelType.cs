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
            Field(o => o.Type, type: typeof(TypeType));
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
                .ResolveAsync(context => context.ResolveConnectionAsync(connectionContext => connectionContext.Source?.ModelTags.Select(t => t.Model), 
                    (set, i, u, c) => Task.FromResult(set.If(u.HasValue, x => x.Where(y => y.Date > u!.Value)).TakeLast(i!.Value).ToList()),
                    (set, i, u, c) => Task.FromResult(set.If(u.HasValue, x => x.Where(y => y.Date > u!.Value)).TakeLast(i!.Value).ToList()),
                    set => set?.Date,
                    (set, i, u, c) => Task.FromResult(set.If(u.HasValue, x => x.Where(y => y.Date > u!.Value)).If(i.HasValue, x => x.Take(i!.Value)).Any()),
                    (set, i, u, c) => Task.FromResult(set.If(u.HasValue, x => x.Where(y => y.Date < u!.Value)).If(i.HasValue, x => x.TakeLast(i!.Value)).Any())));
        }
    }

    public class PlatformType : EnumerationGraphType<Platform>
    {
    }

    public class StatusType : EnumerationGraphType<Status>
    {
    }

    public class TypeType : EnumerationGraphType<TypeEnum>
    {
    }

    public class UserLevelType : EnumerationGraphType<UserLevel>
    {
    }

    public sealed class UserType : ObjectGraphType<User>
    {
        public UserType()
        {
            Field(o => o.BSaber, type: typeof(StringGraphType));
            Field(o => o.DiscordId, type: typeof(ULongGraphType));
            Field(o => o.Level, type: typeof(UserLevelType));
            Field(o => o.Name, type: typeof(StringGraphType));
            Connection<ModelType>()
                .Name("models")
                .Description("Model list")
                .Bidirectional()
                .PageSize(100)
                .ResolveAsync(context => context.ResolveConnectionAsync(connectionContext => connectionContext.Source?.Models.Select(t => t.Model), 
                    (set, i, u, c) => Task.FromResult(set.If(u.HasValue, x => x.Where(y => y.Date > u!.Value)).TakeLast(i!.Value).ToList()),
                    (set, i, u, c) => Task.FromResult(set.If(u.HasValue, x => x.Where(y => y.Date > u!.Value)).TakeLast(i!.Value).ToList()),
                    set => set?.Date,
                    (set, i, u, c) => Task.FromResult(set.If(u.HasValue, x => x.Where(y => y.Date > u!.Value)).If(i.HasValue, x => x.Take(i!.Value)).Any()),
                    (set, i, u, c) => Task.FromResult(set.If(u.HasValue, x => x.Where(y => y.Date < u!.Value)).If(i.HasValue, x => x.TakeLast(i!.Value)).Any())));
        }
    }

    public static class GQLPaginationExtension
    {
        public static async Task<object?> ResolveConnectionAsync<T, U, K>(this IResolveConnectionContext<K> context, 
            Func<IResolveConnectionContext<K>, IEnumerable<T>> func, 
            Func<IEnumerable<T>, int?, U?, CancellationToken, Task<List<T>>> beforeFunc, 
            Func<IEnumerable<T>, int?, U?, CancellationToken, Task<List<T>>> afterFunc, 
            Func<T, U?> cursorFunc, 
            Func<IEnumerable<T>, int?, U?, CancellationToken, Task<bool>> afterCheckFunc,
            Func<IEnumerable<T>, int?, U?, CancellationToken, Task<bool>> beforeCheckFunc) where K : class
        {
            var first = context.First;
            var afterCursor = Cursor.FromCursor<U>(context.After);
            var last = context.Last;
            var beforeCursor = Cursor.FromCursor<U>(context.Before);
            var cancellationToken = context.CancellationToken;

            var getModelsTask = GetListAsync(context, first, afterCursor, last, beforeCursor, cancellationToken, func, beforeFunc, afterFunc);
            var getNextPageTask = GetNextPageAsync(context, first, afterCursor, cancellationToken, func, afterCheckFunc);
            var getPreviousPageTask = GetPreviousPageAsync(context, last, beforeCursor, cancellationToken, func, beforeCheckFunc);
            var totalCountTask = Task.FromResult(func(context).Count());

            var (models, nextPage, previousPage, totalCount) = await WaitAll(getModelsTask, getNextPageTask, getPreviousPageTask, totalCountTask).ConfigureAwait(false);
            var (firstCursor, lastCursor) = Cursor.GetFirstAndLastCursor(models, cursorFunc);

            return new Connection<T>
            {
                Edges = models.Select(x => new Edge<T>
                {
                    Cursor = Cursor.ToCursor(cursorFunc(x)),
                    Node = x
                }).ToList(),
                PageInfo = new PageInfo
                {
                    HasNextPage = nextPage,
                    HasPreviousPage = previousPage,
                    StartCursor = firstCursor,
                    EndCursor = lastCursor
                },
                TotalCount = totalCount
            };
        }
        private static Task<bool> GetPreviousPageAsync<T, U, K>(IResolveConnectionContext<K> dbContext, 
            int? last, 
            U? beforeCursor, 
            CancellationToken cancellationToken, 
            Func<IResolveConnectionContext<K>, IEnumerable<T>> modelFunc,
            Func<IEnumerable<T>, int?, U?, CancellationToken, Task<bool>> func) where K : class
            => func(modelFunc(dbContext), last, beforeCursor, cancellationToken);

        private static Task<bool> GetNextPageAsync<T, U, K>(IResolveConnectionContext<K> dbContext, 
            int? first, 
            U? afterCursor, 
            CancellationToken cancellationToken,
            Func<IResolveConnectionContext<K>, IEnumerable<T>> modelFunc,
            Func<IEnumerable<T>, int?, U?, CancellationToken, Task<bool>> func) where K : class
            => func(modelFunc(dbContext), first, afterCursor, cancellationToken);

        private static Task<List<T>> GetListAsync<T, U, K>(IResolveConnectionContext<K> context, 
            int? first, 
            U? afterCursor, 
            int? last, 
            U? beforeCursor, 
            CancellationToken cancellationToken, 
            Func<IResolveConnectionContext<K>, IEnumerable<T>> dbFunc, 
            Func<IEnumerable<T>, int?, U?, CancellationToken, Task<List<T>>> beforeFunc, 
            Func<IEnumerable<T>, int?, U?, CancellationToken, Task<List<T>>> afterFunc) where K : class
            => first.HasValue ?
                beforeFunc(dbFunc(context), first, afterCursor, cancellationToken) :
                afterFunc(dbFunc(context), last, beforeCursor, cancellationToken);

        private static async Task<(T1, T2, T3, T4)> WaitAll<T1, T2, T3, T4>(Task<T1> task1, Task<T2> task2, Task<T3> task3, Task<T4> task4)
        {
            await Task.WhenAll(task1, task2, task3, task4).ConfigureAwait(false);
            return (await task1.ConfigureAwait(false), await task2.ConfigureAwait(false), await task3.ConfigureAwait(false), await task4.ConfigureAwait(false));
        }
    }
}