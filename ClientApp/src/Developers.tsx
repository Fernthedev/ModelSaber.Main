import React, { Component } from "react";

export default class Developers extends Component {
    render(){
        var l = ["Saber", "Avatar", "Platform", "Blocks"]
        
        return(
            <div>
                <h1>
                    Developer Documentation
                </h1>
                <p>
                    Here you will find all necessary information about the API.
                </p>
                
                <h2>
                    REST API endpoints
                </h2>
               <iframe src="https://api-modelsaber.ayadev.xyz/" />
            </div>
        )
    }
}