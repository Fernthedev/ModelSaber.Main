import React, { Component } from "react";
import { Route } from "react-router";
import { EventEmitter } from "events";
import Layout from "./Layout";
import Developer from "./Developers";
import Contributions from "./Contributions";
import Home from "./Home";
import { Model } from "./Models";

export default class App extends Component {
    render() {
        return (
            <Layout>
                <Route exact path="/" component={Home} />
                <Route path="/contributions" component={Contributions} />
                <Route path="/dev" component={Developer} />
                <Route exact path="/model/:id" component={Model} />
            </Layout>
        )
    }
}