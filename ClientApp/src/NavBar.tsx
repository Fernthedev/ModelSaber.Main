import React, { Component } from "react";

export default class NavBar extends Component{
    render() {
        return <header className="navbar">
            <a className="btn btn-primary">
                PC
            </a>
            <a className="btn btn-primary">
                Quest
            </a>
        </header>;
    }
}