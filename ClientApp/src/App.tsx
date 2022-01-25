import React, { Component, lazy, Suspense } from "react";
import { Route } from "react-router";
import Layout from "./components/Layout";
import XRegExp from "xregexp";
import EventEmitter from "events";
import { Loader } from "./components/Loader";
const Developer = lazy(() => import("./components/Developers"));
const Contributions = lazy(() => import("./components/Contributions"));
const Home = lazy(() => import("./Home"));
const Models = lazy(() => import("./components/Model"));
const Login = lazy(() => import("./components/Login"));
const Logout = lazy(() => import("./components/Logout"));

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
    render() {
        return (
            <Suspense fallback={Loader}>
                <Layout>
                    <Route exact path="/" component={Home} />
                    <Route path="/contributions" component={Contributions} />
                    <Route path="/dev" component={Developer} />
                    <Route exact path="/discordlogin" component={Login} />
                    <Route exact path="/logout" component={Logout} />
                    <Route exact path="/model/:id" component={Models} />
                </Layout>
            </Suspense>
        )
    }
}

