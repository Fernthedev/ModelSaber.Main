import React, { Component } from "react";
import { RouteComponentProps, withRouter } from "react-router";
import { getCookie } from "..";
import { events } from "../App";

const discordLink = "https://discord.com/api/oauth2/authorize?client_id=923764515240300554&redirect_uri=https%3A%2F%2F" + window.location.host + "%2Fdiscordlogin&response_type=token&scope=identify%20email";

class NavBar extends Component<RouteComponentProps, { loggedIn: boolean }> {
    constructor(props: RouteComponentProps) {
        super(props);
        this.state = { loggedIn: false };
        events.on("loginEvent", this.loggedIn.bind(this));
    }

    componentDidMount() {
        if (!!getCookie("login")) {
            events.emitLogin();
        }
    }

    loggedIn() {
        this.setState({ loggedIn: !!getCookie("login") });
    }

    navigate(route: string) {
        this.props.history.push(route);
    }

    render() {
        return <header className="navbar sticky-top bg-dark">
            <div className="container-fluid">
                <a className="btn btn-dark" onClick={() => this.navigate("/")}>
                    <img src="modelsaber-logo-web.svg" alt="ModelSaberImage" className="img-fluid" width="52" height="52" />
                </a>
                <div>
                    <a className="btn btn-primary me-3" href="https://github.com/legoandmars/modeldownloader" target="_blank">
                        ModelDownloader
                    </a>
                    <a className="btn btn-outline-primary me-3" href="https://github.com/ModelSaber/ModelSaber/" target="_blank">
                        GitHub
                    </a>
                    <a className="btn btn-outline-primary me-3" onClick={() => this.navigate("/contributions")}>
                        Contributions
                    </a>
                    <a className="btn btn-outline-primary me-3" onClick={() => this.navigate("/dev")}>
                        Developers
                    </a>
                    <a className="btn btn-primary" href={this.state.loggedIn ? undefined : discordLink} onClick={() => { if (this.state.loggedIn) this.navigate("/logout"); }}>
                        {this.state.loggedIn ? "Logout" : "Login"}
                    </a>
                </div>
            </div>
        </header>;
    }
}

export default withRouter(NavBar);