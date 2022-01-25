import React, { Component } from "react";

export default class Developers extends Component {
    render(){        
        return(
            <div>
                <h1 className="ms-3 me-3">
                    Developer Documentation
                </h1>
                <p className="ms-3 me-3">
                    Here you will find all necessary information about the API.
                </p>
                
                <h2 className="ms-3 me-3">
                    REST API Endpoints and Schema
                </h2>
                <iframe src="https://apimodelsaber.rainemods.io/" style={{height:1024,width:"100%"}} />
                
                <div className="ms-3 me-3 navbar">
                    <h2>
                        GraphQL Endpoint
                    </h2>
                    <a className="btn btn-outline-primary bg-dark" 
                       href="http://studio.apollographql.com/sandbox/explorer?endpoint=https://apimodelsaber.rainemods.io/graphql">
                        Try it out!
                    </a>
                </div>
                <iframe className="bg-dark" src="https://apimodelsaber.rainemods.io/voyager" style={{height:1024,width:"100%"}} />
            </div>
        )
    }
}
