import { gql } from '@apollo/client';
import * as Apollo from '@apollo/client';
export type Maybe<T> = T | null;
export type InputMaybe<T> = Maybe<T>;
export type Exact<T extends { [key: string]: unknown }> = { [K in keyof T]: T[K] };
export type MakeOptional<T, K extends keyof T> = Omit<T, K> & { [SubKey in K]?: Maybe<T[SubKey]> };
export type MakeMaybe<T, K extends keyof T> = Omit<T, K> & { [SubKey in K]: Maybe<T[SubKey]> };
const defaultOptions = {} as const;
/** All built-in and custom scalars, mapped to their actual values */
export type Scalars = {
  ID: string;
  String: string;
  Boolean: boolean;
  Int: number;
  Float: number;
  /** The `DateTime` scalar type represents a date and time. `DateTime` expects timestamps to be formatted in accordance with the [ISO-8601](https://en.wikipedia.org/wiki/ISO_8601) standard. */
  DateTime: any;
  UInt64: any;
};

/** A connection from an object to a list of objects of type `Model`. */
export type ModelConnection = {
  __typename?: 'ModelConnection';
  /** A list of all of the edges returned in the connection. */
  edges?: Maybe<Array<Maybe<ModelEdge>>>;
  /** A list of all of the objects returned in the connection. This is a convenience field provided for quickly exploring the API; rather than querying for "{ edges { node } }" when no edge data is needed, this field can be used instead. Note that when clients like Relay need to fetch the "cursor" field on the edge to enable efficient pagination, this shortcut cannot be used, and the full "{ edges { node } } " version should be used instead. */
  items?: Maybe<Array<Maybe<ModelType>>>;
  /** Information to aid in pagination. */
  pageInfo: PageInfo;
  /** A count of the total number of objects in this connection, ignoring pagination. This allows a client to fetch the first five objects by passing "5" as the argument to `first`, then fetch the total count so it could display "5 of 83", for example. In cases where we employ infinite scrolling or don't have an exact count of entries, this field will return `null`. */
  totalCount?: Maybe<Scalars['Int']>;
};

/** An edge in a connection from an object to another object of type `Model`. */
export type ModelEdge = {
  __typename?: 'ModelEdge';
  /** A cursor for use in pagination */
  cursor: Scalars['String'];
  /** The item at the end of the edge */
  node?: Maybe<ModelType>;
};

export type ModelSaberQuery = {
  __typename?: 'ModelSaberQuery';
  /** Single model */
  model?: Maybe<ModelType>;
  /** Model list */
  models?: Maybe<ModelConnection>;
  /** You wanted yer tags? */
  tags?: Maybe<TagConnection>;
  /** The entire user list */
  users?: Maybe<Array<Maybe<UserType>>>;
};


export type ModelSaberQueryModelArgs = {
  id: Scalars['ID'];
};


export type ModelSaberQueryModelsArgs = {
  after?: InputMaybe<Scalars['String']>;
  before?: InputMaybe<Scalars['String']>;
  first?: InputMaybe<Scalars['Int']>;
  last?: InputMaybe<Scalars['Int']>;
  modelType?: InputMaybe<TypeEnum>;
};


export type ModelSaberQueryTagsArgs = {
  after?: InputMaybe<Scalars['String']>;
  before?: InputMaybe<Scalars['String']>;
  first?: InputMaybe<Scalars['Int']>;
  last?: InputMaybe<Scalars['Int']>;
};

export type ModelType = {
  __typename?: 'ModelType';
  date?: Maybe<Scalars['DateTime']>;
  description?: Maybe<Scalars['String']>;
  downloadPath: Scalars['String'];
  hash?: Maybe<Scalars['String']>;
  id: Scalars['Int'];
  mainUser?: Maybe<UserType>;
  name: Scalars['String'];
  platform?: Maybe<Platform>;
  status?: Maybe<Status>;
  tags?: Maybe<Array<Maybe<TagType>>>;
  thumbnail: Scalars['String'];
  type?: Maybe<TypeEnum>;
  userId?: Maybe<Scalars['UInt64']>;
  users?: Maybe<Array<Maybe<UserType>>>;
  uuid: Scalars['ID'];
};

/** Information about pagination in a connection. */
export type PageInfo = {
  __typename?: 'PageInfo';
  /** When paginating forwards, the cursor to continue. */
  endCursor?: Maybe<Scalars['String']>;
  /** When paginating forwards, are there more items? */
  hasNextPage: Scalars['Boolean'];
  /** When paginating backwards, are there more items? */
  hasPreviousPage: Scalars['Boolean'];
  /** When paginating backwards, the cursor to continue. */
  startCursor?: Maybe<Scalars['String']>;
};

/** Platform type */
export enum Platform {
  /** Platform PC */
  Pc = 'PC',
  /** Platform Quest */
  Quest = 'QUEST'
}

/** Model status */
export enum Status {
  /** ApprovalRequired */
  ApprovalRequired = 'APPROVAL_REQUIRED',
  /** Approved */
  Approved = 'APPROVED',
  /** Featured */
  Featured = 'FEATURED',
  /** Published */
  Published = 'PUBLISHED',
  /** Unpublished */
  Unpublished = 'UNPUBLISHED'
}

/** A connection from an object to a list of objects of type `Tag`. */
export type TagConnection = {
  __typename?: 'TagConnection';
  /** A list of all of the edges returned in the connection. */
  edges?: Maybe<Array<Maybe<TagEdge>>>;
  /** A list of all of the objects returned in the connection. This is a convenience field provided for quickly exploring the API; rather than querying for "{ edges { node } }" when no edge data is needed, this field can be used instead. Note that when clients like Relay need to fetch the "cursor" field on the edge to enable efficient pagination, this shortcut cannot be used, and the full "{ edges { node } } " version should be used instead. */
  items?: Maybe<Array<Maybe<TagType>>>;
  /** Information to aid in pagination. */
  pageInfo: PageInfo;
  /** A count of the total number of objects in this connection, ignoring pagination. This allows a client to fetch the first five objects by passing "5" as the argument to `first`, then fetch the total count so it could display "5 of 83", for example. In cases where we employ infinite scrolling or don't have an exact count of entries, this field will return `null`. */
  totalCount?: Maybe<Scalars['Int']>;
};

/** An edge in a connection from an object to another object of type `Tag`. */
export type TagEdge = {
  __typename?: 'TagEdge';
  /** A cursor for use in pagination */
  cursor: Scalars['String'];
  /** The item at the end of the edge */
  node?: Maybe<TagType>;
};

export type TagType = {
  __typename?: 'TagType';
  id: Scalars['Int'];
  /** Model list */
  models?: Maybe<ModelConnection>;
  name: Scalars['String'];
};


export type TagTypeModelsArgs = {
  after?: InputMaybe<Scalars['String']>;
  before?: InputMaybe<Scalars['String']>;
  first?: InputMaybe<Scalars['Int']>;
  last?: InputMaybe<Scalars['Int']>;
};

export enum TypeEnum {
  Avatar = 'AVATAR',
  Effect = 'EFFECT',
  HealthBar = 'HEALTH_BAR',
  Note = 'NOTE',
  Platform = 'PLATFORM',
  Saber = 'SABER',
  Wall = 'WALL'
}

/** UserLevels for what your user is */
export enum UserLevel {
  /** Praise them */
  Admin = 'ADMIN',
  /** Be carefull what you do */
  Moderator = 'MODERATOR',
  /** Just your average Joe */
  Normal = 'NORMAL',
  /** Oh you just got fancy */
  Verified = 'VERIFIED'
}

export type UserTagType = {
  __typename?: 'UserTagType';
  name?: Maybe<Scalars['String']>;
};

export type UserType = {
  __typename?: 'UserType';
  avatar?: Maybe<Scalars['String']>;
  bSaber?: Maybe<Scalars['String']>;
  discordId?: Maybe<Scalars['UInt64']>;
  level?: Maybe<UserLevel>;
  /** Model list */
  models?: Maybe<ModelConnection>;
  name?: Maybe<Scalars['String']>;
  userTags?: Maybe<Array<Maybe<UserTagType>>>;
};


export type UserTypeModelsArgs = {
  after?: InputMaybe<Scalars['String']>;
  before?: InputMaybe<Scalars['String']>;
  first?: InputMaybe<Scalars['Int']>;
  last?: InputMaybe<Scalars['Int']>;
};

export type GetModelFullQueryVariables = Exact<{
  modelId: Scalars['ID'];
}>;


export type GetModelFullQuery = { __typename?: 'ModelSaberQuery', model?: { __typename?: 'ModelType', uuid: string, name: string, status?: Status | null | undefined, platform?: Platform | null | undefined, type?: TypeEnum | null | undefined, description?: string | null | undefined, thumbnail: string, downloadPath: string, users?: Array<{ __typename?: 'UserType', name?: string | null | undefined, discordId?: any | null | undefined } | null | undefined> | null | undefined, tags?: Array<{ __typename?: 'TagType', name: string, id: number } | null | undefined> | null | undefined } | null | undefined };

export type GetModelsQueryVariables = Exact<{
  first?: InputMaybe<Scalars['Int']>;
  after?: InputMaybe<Scalars['String']>;
}>;


export type GetModelsQuery = { __typename?: 'ModelSaberQuery', models?: { __typename?: 'ModelConnection', items?: Array<{ __typename?: 'ModelType', uuid: string, name: string, status?: Status | null | undefined, platform?: Platform | null | undefined, thumbnail: string, users?: Array<{ __typename?: 'UserType', name?: string | null | undefined, discordId?: any | null | undefined } | null | undefined> | null | undefined, tags?: Array<{ __typename?: 'TagType', name: string, id: number } | null | undefined> | null | undefined } | null | undefined> | null | undefined, pageInfo: { __typename?: 'PageInfo', endCursor?: string | null | undefined, hasNextPage: boolean, hasPreviousPage: boolean } } | null | undefined };

export type ModelFragment = { __typename?: 'ModelType', uuid: string, name: string, status?: Status | null | undefined, platform?: Platform | null | undefined, thumbnail: string, users?: Array<{ __typename?: 'UserType', name?: string | null | undefined, discordId?: any | null | undefined } | null | undefined> | null | undefined, tags?: Array<{ __typename?: 'TagType', name: string, id: number } | null | undefined> | null | undefined };

export const ModelFragmentDoc = gql`
    fragment Model on ModelType {
  uuid
  name
  status
  platform
  users {
    name
    discordId
  }
  tags {
    name
    id
  }
  thumbnail
}
    `;
export const GetModelFullDocument = gql`
    query GetModelFull($modelId: ID!) {
  model(id: $modelId) {
    uuid
    name
    status
    platform
    type
    description
    users {
      name
      discordId
    }
    tags {
      name
      id
    }
    thumbnail
    downloadPath
  }
}
    `;

/**
 * __useGetModelFullQuery__
 *
 * To run a query within a React component, call `useGetModelFullQuery` and pass it any options that fit your needs.
 * When your component renders, `useGetModelFullQuery` returns an object from Apollo Client that contains loading, error, and data properties
 * you can use to render your UI.
 *
 * @param baseOptions options that will be passed into the query, supported options are listed on: https://www.apollographql.com/docs/react/api/react-hooks/#options;
 *
 * @example
 * const { data, loading, error } = useGetModelFullQuery({
 *   variables: {
 *      modelId: // value for 'modelId'
 *   },
 * });
 */
export function useGetModelFullQuery(baseOptions: Apollo.QueryHookOptions<GetModelFullQuery, GetModelFullQueryVariables>) {
        const options = {...defaultOptions, ...baseOptions}
        return Apollo.useQuery<GetModelFullQuery, GetModelFullQueryVariables>(GetModelFullDocument, options);
      }
export function useGetModelFullLazyQuery(baseOptions?: Apollo.LazyQueryHookOptions<GetModelFullQuery, GetModelFullQueryVariables>) {
          const options = {...defaultOptions, ...baseOptions}
          return Apollo.useLazyQuery<GetModelFullQuery, GetModelFullQueryVariables>(GetModelFullDocument, options);
        }
export type GetModelFullQueryHookResult = ReturnType<typeof useGetModelFullQuery>;
export type GetModelFullLazyQueryHookResult = ReturnType<typeof useGetModelFullLazyQuery>;
export type GetModelFullQueryResult = Apollo.QueryResult<GetModelFullQuery, GetModelFullQueryVariables>;
export const GetModelsDocument = gql`
    query GetModels($first: Int, $after: String) {
  models(first: $first, after: $after) {
    items {
      ...Model
    }
    pageInfo {
      endCursor
      hasNextPage
      hasPreviousPage
    }
  }
}
    ${ModelFragmentDoc}`;

/**
 * __useGetModelsQuery__
 *
 * To run a query within a React component, call `useGetModelsQuery` and pass it any options that fit your needs.
 * When your component renders, `useGetModelsQuery` returns an object from Apollo Client that contains loading, error, and data properties
 * you can use to render your UI.
 *
 * @param baseOptions options that will be passed into the query, supported options are listed on: https://www.apollographql.com/docs/react/api/react-hooks/#options;
 *
 * @example
 * const { data, loading, error } = useGetModelsQuery({
 *   variables: {
 *      first: // value for 'first'
 *      after: // value for 'after'
 *   },
 * });
 */
export function useGetModelsQuery(baseOptions?: Apollo.QueryHookOptions<GetModelsQuery, GetModelsQueryVariables>) {
        const options = {...defaultOptions, ...baseOptions}
        return Apollo.useQuery<GetModelsQuery, GetModelsQueryVariables>(GetModelsDocument, options);
      }
export function useGetModelsLazyQuery(baseOptions?: Apollo.LazyQueryHookOptions<GetModelsQuery, GetModelsQueryVariables>) {
          const options = {...defaultOptions, ...baseOptions}
          return Apollo.useLazyQuery<GetModelsQuery, GetModelsQueryVariables>(GetModelsDocument, options);
        }
export type GetModelsQueryHookResult = ReturnType<typeof useGetModelsQuery>;
export type GetModelsLazyQueryHookResult = ReturnType<typeof useGetModelsLazyQuery>;
export type GetModelsQueryResult = Apollo.QueryResult<GetModelsQuery, GetModelsQueryVariables>;