import React, { Component, lazy } from "react";
import { Route, Routes } from "react-router";
import { Tooltip } from "bootstrap/dist/js/bootstrap.bundle.min";
import Layout from "./components/Layout";
import XRegExp from "xregexp";
import EventEmitter from "events";
const Developer = lazy(() => import("./pages/Developers"));
const Contributions = lazy(() => import("./pages/Contributions"));
const Home = lazy(() => import("./pages/Home"));
const Model = lazy(() => import("./pages/Model"));
const Models = lazy(() => import("./pages/Models"));
const Login = lazy(() => import("./pages/Login"));
const Logout = lazy(() => import("./pages/Logout"));

class ModelSaberEventEmitter extends EventEmitter {
    emitLogin() {
        this.emit("loginEvent");
    }
}

interface ModelSaberEventEmitter {
    on(event: "loginEvent", listener: () => void): this;
    on(event: string, listener: Function): this;
}

export const unicodeWord = XRegExp.tag()`^\p{Letter}[\p{Letter}\p{Mark}]*$`;
export const events = new ModelSaberEventEmitter();

export default class App extends Component {
    componentDidMount() {
        setTimeout(() => {
            document.querySelectorAll('[data-bs-toggle="tooltip"]').forEach(function (tooltipTriggerEl) {
                new Tooltip(tooltipTriggerEl);
            })
        }, 500);
    }

    render() {
        return (
            <Layout>
                <Routes>
                    <Route path="/" element={<Home />} />
                    <Route path="/models" element={<Models />} />
                    <Route path="/contributions" element={<Contributions />} />
                    <Route path="/dev" element={<Developer />} />
                    <Route path="/discordlogin" element={<Login />} />
                    <Route path="/logout" element={<Logout />} />
                    <Route path="/model/:id" element={<Model />} />
                </Routes>
            </Layout>
        )
    }
}

