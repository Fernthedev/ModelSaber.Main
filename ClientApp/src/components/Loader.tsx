import React, { Component } from "react";
import "./Loader.scss";

export class Loader extends Component {
    render() {
        return (<div className="lds-roller-main" id="tsx-loader">
            <div className="background-blured">
                <div className="background-darken"></div>
            </div>
            <svg width="200" height="200" viewBox="0 0 100 100" className="loader">
                <polyline className="line-cornered stroke-still" points="0,0 100,0 100,100" stroke-width="10" fill="none">
                </polyline>
                <polyline className="line-cornered stroke-still" points="0,0 0,100 100,100" stroke-width="10" fill="none">
                </polyline>
                <polyline className="line-cornered stroke-animation" points="0,0 100,0 100,100" stroke-width="10" fill="none">
                </polyline>
                <polyline className="line-cornered stroke-animation" points="0,0 0,100 100,100" stroke-width="10" fill="none">
                </polyline>
            </svg>
        </div>)
    }
}