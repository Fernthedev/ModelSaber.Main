import React, { Component } from "react";
import NavBar from "./NavBar";
import "./Layout.scss";

export default class Layout extends Component<any, { build: string }> {
    constructor(props: any) {
        super(props);
        this.state = {
            build: ""
        }
    }

    componentDidMount() {
        fetch("api").then(async t => {
            if (!t.ok)
                return;
            var build = await t.text();
            this.setState({ build: build });
        }).catch(console.error);
    }

    render() {
        return (<>
            <NavBar />
            <div className="container">
                {this.props.children}
            </div>
            <div className="spacer" />
            <footer className="footer bg-dark">
                <div className="container">
                    <label>{this.state.build}</label>
                </div>
            </footer>
        </>);
    }
}