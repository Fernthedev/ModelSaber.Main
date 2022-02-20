import React from "react";
import { ApolloProvider, ApolloClient, InMemoryCache } from "@apollo/client";

export default function GQLClient(props: React.PropsWithChildren<{ uri: string }>) {
    const client = new ApolloClient({
        uri: props.uri + "/graphql",
        cache: new InMemoryCache()
    });

    return (<ApolloProvider client={client}>
        {props.children}
    </ApolloProvider>)
}