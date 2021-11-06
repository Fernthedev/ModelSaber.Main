import "./index.scss"
import "bootstrap-icons/font/bootstrap-icons.scss";
import React, { Component } from "react";
import ReactDOM from "react-dom";
import { BrowserRouter } from "react-router-dom";
import App from "./App";
import registerServiceWorker from "./registerServiceWorker";

const baseUrl = document.getElementsByTagName("base")[0].getAttribute("href");
const rootElement = document.getElementById("root");
const loaderbackground = document.getElementById("lds-roller");

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
                <App />
            </BrowserRouter>
        );
    }
}

ReactDOM.render(<Index />, rootElement);

registerServiceWorker();