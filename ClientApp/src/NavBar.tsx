import React, { Component } from "react";

export default class NavBar extends Component {
    render() {
        return <header className="navbar sticky-top bg-dark">
            <div className="container-fluid">
                <a className="btn btn-dark" href="https://localhost:44385/">
                    <img src="modelsaber-logo-web.svg" alt="ModelSaberImage" className="img-fluid" width="52" height="52" />
                </a>
                <div>
                    <a className="btn btn-outline-primary me-3" href="https://github.com/ModelSaber/ModelSaber/">
                        GitHub
                    </a>
                    <a className="btn btn-outline-primary me-3" href="https://localhost:44385/contributions">
                        Contributions
                    </a>
                    <a className="btn btn-outline-primary me-3" href="https://localhost:44385/dev">
                        Developers
                    </a>
                </div>
            </div>
        </header>;
    }
}