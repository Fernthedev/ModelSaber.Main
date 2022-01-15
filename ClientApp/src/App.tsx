import React, { Component } from "react";
import { Route } from "react-router";
import Layout from "./components/Layout";
import Developer from "./components/Developers";
import Contributions from "./components/Contributions";
import Home from "./Home";
import Models from "./components/Models";
import Login from "./components/Login";
import XRegExp from "xregexp";
import EventEmitter from "events";

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
                <Route path="/contributions" component={Contributions} />
                <Route path="/dev" component={Developer} />
                <Route exact path="/discordlogin" component={Login.Login} />
                <Route exact path="/logout" component={Login.Logout}></Route>
                <Route exact path="/model/:id" component={Models.Model} />
            </Layout>
        )
    }
}

