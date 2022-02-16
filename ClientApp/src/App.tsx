import React, { Component, lazy } from "react";
import { Route } from "react-router";
import Layout from "./components/Layout";
import XRegExp from "xregexp";
import EventEmitter from "events";
const Developer = lazy(() => import("./components/Developers"));
const Contributions = lazy(() => import("./components/Contributions"));
const Home = lazy(() => import("./Home"));
const Model = lazy(() => import("./components/model/Model"));
const Models = lazy(() => import("./components/model/Models"));
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
            <Layout>
                <Route exact path="/" component={Home} />
                <Route exact path="/models" component={Models} />
                <Route path="/contributions" component={Contributions} />
                <Route path="/dev" component={Developer} />
                <Route exact path="/discordlogin" component={Login} />
                <Route exact path="/logout" component={Logout} />
                <Route exact path="/model/:id" component={Model} />
            </Layout>
        )
    }
}

