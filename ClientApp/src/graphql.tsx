import { GetModelCursorsQueryResult, GetModelCursorsQueryVariables, GetModelFullQueryResult, GetModelsQueryResult, GetModelsQueryVariables, useGetModelCursorsQuery, useGetModelFullQuery, useGetModelsQuery } from "./graphqlTypes";
import React, { ComponentType, useState } from "react";
import { RouteComponentProps } from "react-router";

export function withGetModelFull(WrappedComponent: ComponentType<GetModelFullQueryResult & RouteComponentProps<{ id: string }>>) {
  return function C(props: RouteComponentProps<{ id: string }>) {
    console.log(props);
    const query = useGetModelFullQuery({ variables: { modelId: props.match.params.id } });
    return (<WrappedComponent {...query} {...props} />);
  }
}

export type WithGetModelsProps = GetModelsQueryResult & { setHookState: React.Dispatch<React.SetStateAction<GetModelsQueryVariables>> }

export function withGetModels<P>(WrappedComponent: ComponentType<WithGetModelsProps & P>) {
  return function C(props: P) {
    const [state, setState] = useState<GetModelsQueryVariables>({ first: 10 });
    const query = useGetModelsQuery({ variables: state });
    return (<WrappedComponent {...query} {...props} setHookState={setState} />);
  }
}

export type WithGetModelCursorsProps = GetModelCursorsQueryResult & { setCursorSize: React.Dispatch<React.SetStateAction<GetModelCursorsQueryVariables>> }

export function withGetModelCursors<P>(WrappedComponent: ComponentType<WithGetModelCursorsProps & { size: number } & P>) {
  return function C(props: P & { size: number }) {
    const [state, setState] = useState<GetModelCursorsQueryVariables>({ size: props.size });
    const query = useGetModelCursorsQuery({ variables: state });
    return (<WrappedComponent {...query} {...props} setCursorSize={setState} />);
  }
}