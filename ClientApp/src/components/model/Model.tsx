import React, { Component } from "react";
import { RouteComponentProps } from "react-router-dom";
import { withGetModelFull } from "../../graphql";
import { GetModelFullQueryResult } from "../../graphqlTypes";
import { getTumbnail } from "../getTumbnail";

export class Model extends Component<GetModelFullQueryResult & RouteComponentProps<{ id: string }>> {
    vidRef: React.RefObject<HTMLVideoElement>;
    imgRef: React.RefObject<HTMLImageElement>;
    constructor(props: any) {
        super(props);
        this.imgRef = React.createRef();
        this.vidRef = React.createRef();
    }

    fixWoopsieDaisy() {
        this.imgRef.current.style.display = "none";
        this.vidRef.current.style.display = "inline";
    }

    render() {
        if (this.props.loading) return (<></>);
        if (!this.props.data) {
            this.props.history.push("/");
            return (<></>);
        }
        let model = this.props.data.model;
        return !!model ?
            (<>
                <div className="row mt-2">
                    <div className="col-4 border-end pb-2">
                        {getTumbnail(model, this.vidRef, this.imgRef, this.fixWoopsieDaisy, { width: "100%", borderRadius: "0.5rem" })}
                    </div>
                    <div className="col-8">
                        <h1>{model.name}</h1>
                        <div className="row border-top pt-1">
                            <h3>Users</h3>
                            <div className="row" style={{ marginTop: -12 }}>
                                {model.users.map((t: any) => (<a key={t.discordId} href="#" className="fs-5 text-decoration-none" style={{ cursor: "pointer" }}>{t.name}</a>))}
                            </div>
                        </div>
                        <div className="row border-top pt-2">
                            <div className="col-6 text-center"><a href={`modelsaber:${model.type}:${model.uuid}`} className="h-100 w-100 btn btn-dark">One Click Install</a></div>
                            <div className="col-6 text-center"><a href={model.downloadPath} target="_blank" className="h-100 w-100 btn btn-dark">Download</a></div>
                        </div>
                        <div className="row mt-2 border-top pt-1">
                            <h5 className="mb-0">Tags</h5>
                            <div className="d-flex flex-wrap">
                                {model.tags.map((t: any) => (<div key={t.id} className="d-inline p-1 ps-2 pe-2 me-1 mt-2 bg-dark rounded-pill text-nowrap">{t.name}</div>))}
                            </div>
                        </div>
                        {!!model.description ?
                            (<div className="row mt-2 border-top pt-1">
                                <h5>Description</h5>
                                <div>
                                    {model.description}
                                </div>
                            </div>)
                            :
                            (<></>)}
                    </div>
                    <div className="row border-top pt-1">

                    </div>
                    <pre>
                        {JSON.stringify(model, null, 4)}
                    </pre>
                </div>
            </>)
            :
            (<></>);
    }
}

export default withGetModelFull(Model);