import React, { Component } from "react";

export default class Layout extends Component {
    render() {
        return (<div>
            <header></header>
            <div className="container">
                {this.props.children}
            </div>
        </div>);
    }
}