import React, { Component } from "react";
import { withRouter, RouteComponentProps } from "react-router-dom";
import Models from "./components/model/Models";

class Home extends Component<RouteComponentProps> {
    constructor(props: any) {
        super(props);
        this.nextPath = this.nextPath.bind(this);
    }

    nextPath(path: string) {
        this.props.history.push(path);
    }

    render() {
        return (<div>
            <h1 className="align-middle">
                Welcome to ModelSaber <label style={{ display: "inline", fontSize: 10, textDecorationLine: "line-through", opacity: 0.1 }}><i>(Destroyer of old links)</i></label>
            </h1>
            <Models />
        </div>);
    }
}

export default withRouter(Home);