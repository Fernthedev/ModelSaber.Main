import React, { Component } from "react";
import { RouteComponentProps, withRouter } from "react-router-dom";
import { events } from "../App";

class Logout extends Component<RouteComponentProps> {
    componentDidMount() {
        document.cookie = "login=;path=/;expires=Thu, 01 Jan 1970 00:00:01 GMT";
        events.emitLogin();
        this.props.history.push("/");
    }

    render() {
        return (<></>);
    }
}
export default withRouter(Logout); 