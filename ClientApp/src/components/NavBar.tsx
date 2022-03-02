﻿import React, { Component, useState } from "react";
import { useNavigate } from "react-router-dom";
import { checkCookie } from "..";
import { events } from "../App";

const discordLink = "https://discord.com/api/oauth2/authorize?client_id=923764515240300554&redirect_uri=https%3A%2F%2F" + window.location.host + "%2Fdiscordlogin&response_type=token&scope=identify%20email";

export default function NavBar() {
    const navigate = useNavigate();
    const [loggedIn, setLoggedIn] = useState(checkCookie("login"));
    events.on("loginEvent", () => setLoggedIn(checkCookie("login")));

    return (
        <header className="navbar sticky-top bg-dark">
            <div className="container-fluid">
                <a className="btn btn-dark" onClick={() => navigate("/")}>
                    <img src="modelsaber-logo.svg" alt="ModelSaberImage" className="img-fluid" width="52" height="52" />
                </a>
                <div>
                    <a className="btn btn-outline-primary me-3" href="https://github.com/ModelSaber/ModelSaber/" target="_blank">GitHub</a>
                    <a className="btn btn-outline-primary me-3" onClick={() => navigate("/contributions")}>Contributions</a>
                    <a className="btn btn-outline-primary me-3" onClick={() => navigate("/dev")}>Developers</a>
                    <a className="btn btn-outline-primary me-3" href="https://github.com/legoandmars/modeldownloader" target="_blank">ModelDownloader</a>
                    <a className="btn btn-primary" href={loggedIn ? undefined : discordLink} onClick={() => { if (loggedIn) navigate("/logout"); }}>
                        {loggedIn ? "Logout" : "Login"}
                    </a>
                </div>
            </div>
        </header>
    )
}