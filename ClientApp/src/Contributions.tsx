import React, { Component } from "react";

var wild = ["Backend", "Database", "GraphQL", "Pagination"]

export default class Contributions extends Component {
    render(){
        return(
            <div>
                <h1 className="ms-3 me-3">
                    Current Contributors
                </h1>
                <div className="d-inline-flex">
                    <div className="bg-dark m-1">
                        <div className="card-header mt-2 mb-2" style={{width:"100%",height:300}}>
                            <div className="flex-wrap m-2 ms-1">
                                <img src="raine.png" alt="Image of Raine" style={{height:128,width:128}} />
                                <h2>Raine</h2>
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
                                <a href="https://github.com/sponsors/dawnvt" className="btn btn-outline-primary">
                                    GitHub Sponsors
                                </a>
                            </div>
                        </div>
                    </div>
                    <div className="bg-dark m-1">
                        <div className="card-header mt-2 mb-2">
                            <div className="flex-wrap m-2 m-1">
                                <img src="wildwolf.png" alt="Image of WildWolf" style={{height:128,width:128}} />
                                <h2>WildWolf</h2>
                                <p>
                                    Lorem ipsum dolor sit amet,
                                    consectetur adipiscing elit,
                                    sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.
                                    Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.
                                    Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.
                                    
                                </p>
                            </div>
                            
                            <h3>
                                Support them!
                            </h3>
                            <a href="https://github.com/sponsors/wolfcomp" className="btn btn-outline-primary">
                                GitHub Sponsors
                            </a>
                        </div>
                    </div>
                    <div className="bg-dark m-1">
                        <div className="card-header mt-2 mb-2">
                            <div className="flex-wrap m-2 m-1">
                                <img src="omurky.png" alt="Image of WildWolf" style={{height:128,width:128}} />
                                <h2>Omurky</h2>
                                <p>
                                    Lorem ipsum dolor sit amet,
                                    consectetur adipiscing elit,
                                    sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.
                                    Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.
                                    Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.

                                </p>
                            </div>

                            <h3>
                                Support them!
                            </h3>
                            <a href="https://github.com/sponsors/wolfcomp" className="btn btn-outline-primary">
                                GitHub Sponsors
                            </a>
                        </div>
                    </div>
                </div>
            </div>
        )
    }
}