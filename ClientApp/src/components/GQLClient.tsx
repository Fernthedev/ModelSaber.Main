import React from "react";
import { createClient, dedupExchange, fetchExchange, Provider } from "urql";
import { cacheExchange } from "@urql/exchange-graphcache";
import { events } from "../App";
import { checkCookie, getCookie } from "..";

export default function GQLClient(props: React.PropsWithChildren<{ uri: string }>) {
    const client = createClient({
        url: props.uri + "/graphql",
        requestPolicy: "cache-and-network",
        exchanges: [dedupExchange, cacheExchange({}), fetchExchange]
    });

    if (checkCookie("login")) {
        client.fetchOptions = { headers: { "Authorization": "JWT " + getCookie("login") } };
    }

    events.on("loginEvent", () => {
        client.fetchOptions = { headers: { "Authorization": "JWT " + getCookie("login") } };
    });

    return (<Provider value={client}>
        {props.children}
    </Provider>)
}