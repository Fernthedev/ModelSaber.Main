import React, { Component, lazy } from "react";
import "./index.scss"
import "bootstrap-icons/font/bootstrap-icons.scss";
import "@popperjs/core";
import "bootstrap";
import ReactDOM from "react-dom";
import { BrowserRouter } from "react-router-dom";
import App from "./App";
import registerServiceWorker from "./registerServiceWorker";
import { ApolloProvider, ApolloClient, InMemoryCache } from "@apollo/client";

const baseUrl = document.getElementsByTagName("base")[0].getAttribute("href");
const rootElement = document.getElementById("root");
const loaderbackground = document.getElementById("lds-roller");
const uri = process.env.NODE_ENV == "development" ? process.env.REACT_APP_API_URL : "https://apimodelsaber.rainemods.io";
const client = new ApolloClient({
    uri: uri + "/graphql",
    cache: new InMemoryCache()
});
class Index extends Component {
    componentDidMount() {
        if (window.location.pathname !== "/login") {
            rootElement.classList.remove("loading-hidden");
            loaderbackground.classList.add("loading-hidden");
            setTimeout(() => {
                loaderbackground.classList.add("loading-inactive");
            }, 1000);
        }
    }

    render() {
        return (
            <BrowserRouter basename={baseUrl}>
                <ApolloProvider client={client}>
                    <App />
                </ApolloProvider>
            </BrowserRouter>
        );
    }
}

ReactDOM.render(<Index />, rootElement);

registerServiceWorker();

export function getCookie(cname: string) {
    var name = cname + "=";
    var decodedCookie = decodeURIComponent(document.cookie);
    var ca = decodedCookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) === ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) === 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}

export function b64DecodeUnicode(str: string) {
    return decodeURIComponent(atob(str).split('').map(function (c) {
        return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
    }).join(''));
}
