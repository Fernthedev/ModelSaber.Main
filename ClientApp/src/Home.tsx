import React, { Component } from "react";
import Models from "./components/model/Models";

export default function Home() {
    return (
        <div>
            <h1 className="align-middle">
                Welcome to ModelSaber <label style={{ display: "inline", fontSize: 1, textDecorationLine: "line-through", opacity: 0.1 }}><i>(Destroyer of old links)</i></label>
            </h1>
            <Models />
        </div>
    );
};