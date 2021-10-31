import React, { Component } from "react";

export default class Contributions extends Component {
    render(){
        return(
            <div>
                <h1 className="ms-3 me-3">
                    Current Contributors
                </h1>
                <div className="card bg-dark m-1">
                    <div className="card-header">
                        <img src="cover.jpg" alt="Contributor 1 Image" className="card-img-top" style={{height:128,width:128}} />
                        <h2>Contributor 1</h2>
                        <p className="card-text">
                            Lorem ipsum dolor sit amet,
                            consectetur adipiscing elit,
                            sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.
                            Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.
                            Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.
                        </p>            
                        <h3>
                            Support them!
                        </h3>
                        <a href="https://github.com/sponsors/legoandmars" className="btn btn-outline-primary">
                            GitHub Sponsors
                        </a>
                    </div>
                </div>
                <div className="card bg-dark m-1">
                    <div className="card-header">
                        <img src="alice.png" alt="Contributor 2 Image" className="card-img-top" style={{height:128,width:128}} />
                        <h2>Contributor 2</h2>
                        <p className="card-text">
                            Lorem ipsum dolor sit amet,
                            consectetur adipiscing elit,
                            sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.
                            Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.
                            Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.
                        </p>
                        <h3>
                            Support them!
                        </h3>
                        <a href="https://github.com/sponsors/legoandmars" className="btn btn-outline-primary">
                            GitHub Sponsors
                        </a>
                    </div>
                </div>
            </div>
        )
    }
}