import React from "react";
import { createClient, dedupExchange, fetchExchange, Provider } from "urql";
import { cacheExchange } from "@urql/exchange-graphcache";

export default function GQLClient(props: React.PropsWithChildren<{ uri: string }>) {
    const client = createClient({
        url: props.uri + "/graphql",
        requestPolicy: "cache-and-network",
        exchanges: [dedupExchange, cacheExchange({}), fetchExchange]
    });

    return (<Provider value={client}>
        {props.children}
    </Provider>)
}