import React, { Component } from "react";
import NavBar from "./NavBar";
import "./Layout.scss";

export default class Layout extends Component<any, { build: { buildVersion: string, buildTime: string } }> {
    constructor(props: any) {
        super(props);
        this.state = {
            build: { buildTime: "", buildVersion: "" }
        }
    }

    componentDidMount() {
        fetch("api").then(async t => {
            if (!t.ok)
                return;
            var build = await t.json();
            this.setState({ build: { buildTime: new Date(Date.parse(build.buildTime)).toLocaleString(), buildVersion: build.buildVersion } });
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
                    <label>Build: {this.state.build.buildVersion}, Build Time: {this.state.build.buildTime}</label>
                </div>
            </footer>
        </>);
    }
}