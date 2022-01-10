import React, { Component } from "react";
import NavBar from "./NavBar";

export default class Layout extends Component {
    render() {
        return (<div>
            <NavBar />
            <div className="container">
                {this.props.children}
            </div>
        </div>);
    }
}