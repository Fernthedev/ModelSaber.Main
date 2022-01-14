import { GetModelFullQueryResult, GetModelsQueryResult, GetModelsQueryVariables, ModelSaberQuery, useGetModelFullQuery, useGetModelsQuery } from "./graphqlTypes";
import React, { ComponentType, useState } from "react";
import { RouteComponentProps } from "react-router";
type StringNumber = string | number;

export interface GQLError {
  message: string;
  locations: Location[];
  path: StringNumber[];
  extensions: Extensions;
}

export interface Location {
  line: number;
  column: number;
}

export interface Extensions {
  code: string;
  codes: string[];
}

export interface GQLReturn {
  data: ModelSaberQuery;
  errors: GQLError[];
}

export function withGetModelFull(WrappedComponent: ComponentType<GetModelFullQueryResult & RouteComponentProps<{ id: string }>>) {
  return function C(props: RouteComponentProps<{ id: string }>) {
    console.log(props);
    const query = useGetModelFullQuery({ variables: { modelId: props.match.params.id } });
    return (<WrappedComponent {...query} {...props} />);
  }
}

export function withGetModels<P>(WrappedComponent: ComponentType<GetModelsQueryResult & { setHookState: React.Dispatch<React.SetStateAction<GetModelsQueryVariables>> } & P>) {
  return function C(props: any) {
    const [state, setState] = useState<GetModelsQueryVariables>({ first: 60 });
    const query = useGetModelsQuery({ variables: state });
    return (<WrappedComponent {...query} {...props} setHookState={setState} />);
  }
}