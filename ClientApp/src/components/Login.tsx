import React, { Component } from "react";
import { RouteComponentProps, withRouter } from "react-router-dom";
import { unicodeWord } from "../App";
import { events } from "../App";

class DiscordRedirLogin extends Component<RouteComponentProps, { requireName: boolean, discordId: string, email: string, name: string }> {
    constructor(props: RouteComponentProps) {
        super(props);
        this.state = { requireName: false, discordId: "", email: "", name: "" };
        this.createUser = this.createUser.bind(this);
    }

    componentDidMount() {
        var search = window.location.hash.substr(1).split('&').map(t => t.split('=')).reduce((map: { [key: string]: string }, obj) => { map[obj[0]] = obj[1]; return map }, {});
        if (search["error"] !== undefined) {
            // window.close();
        }
        else {
            fetch("https://discord.com/api/v6/users/@me", {
                headers: {
                    "Authorization": "Bearer " + search["access_token"]
                }
            }).then(res => {
                if (res.ok)
                    return res.json();
                else
                    return Promise.reject();
            }).then(async res => {
                var body = { discordId: res.id, email: res.email, name: res.username, avatar: res.avatar };
                console.log(body);
                var check = await (await fetch("api/user?discordId=" + body.discordId)).text() === "true";
                if (unicodeWord.test(body.name) || check) {
                    if (check) {
                        await this.loginUser(body);
                    }
                    else {
                        await this.createUser(body);
                    }
                }
                else {
                    this.setState(Object.assign(body, { requireName: true }));
                }
            });
        }
    }

    async createUser(obj: { discordId: string, email: string, name: string, avatar: string }) {
        var body = Object.assign(this.state, obj);
        await fetch("api/user", { body: JSON.stringify(body), method: "POST", headers: { "Content-Type": "application/json" } });
        this.loginUser(body);
    }

    async loginUser(obj: { discordId: string, email: string, name: string, avatar: string }) {
        await fetch("api/login", { body: JSON.stringify(obj), method: "POST", headers: { "Content-Type": "application/json" } });
        events.emitLogin();
        this.props.history.push("/");
    }

    render() {
        return (
            <div>
            </div>
        );
    }
}

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

export default { Login: withRouter(DiscordRedirLogin), Logout: withRouter(Logout) };