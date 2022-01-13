import React, { Component } from "react";
import { RouteComponentProps } from "react-router-dom";
import { GQLReturn } from "../graphql";
import { ModelType } from "../graphqlTypes";

export class Model extends Component<RouteComponentProps<{ id: string }>, { model?: ModelType }> {
    constructor(props: any) {
        super(props);
        this.state = {};
    }

    componentDidMount() {
        fetch(process.env.REACT_APP_API_URL + "/graphql", {
            method: "POST", body: JSON.stringify({
                query: `query ($modelId: ID!) {
                            model(id: $modelId) {
                                uuid
                                name
                                status
                                platform
                                type
                                description
                                users {
                                    name
                                    discordId
                                }
                                tags {
                                    name
                                }
                                thumbnail
                                downloadPath
                            }
                        }`,
                variables: { modelId: this.props.match.params.id, }
            }),
            headers: {
                'Content-Type': 'application/json',
                'Accept': 'application/json',
            }
        }).then(res => res.ok ? res.json() as Promise<GQLReturn> : undefined).then(res => {
            this.setState({ model: res.data.model });
        });
    }

    render() {
        return !!this.state.model ?
            (<>
                <div className="row mt-2">
                    <div className="col-4 border-end pb-2">
                        <img className="rounded" style={{ width: "100%" }} src={this.state.model.thumbnail} />
                    </div>
                    <div className="col-8">
                        <h1>{this.state.model.name}</h1>
                        <div className="row border-top pt-1">
                            <h3>Users</h3>
                            <div className="row" style={{ marginTop: -12 }}>
                                {this.state.model.users.map(t => (<a key={t.discordId} href="#" className="fs-5 text-decoration-none" style={{ cursor: "pointer" }}>{t.name}</a>))}
                            </div>
                        </div>
                        <div className="row border-top pt-2">
                            <div className="col-6 text-center"><a href={`modelsaber:${this.state.model.type}:${this.state.model.uuid}`} className="h-100 w-100 btn btn-dark">One Click Install</a></div>
                            <div className="col-6 text-center"><a href={this.state.model.downloadPath} target="_blank" className="h-100 w-100 btn btn-dark">Download</a></div>
                        </div>
                        <div className="row mt-2 border-top pt-1">
                            <h5 className="mb-0">Tags</h5>
                            <div className="d-flex flex-wrap">
                                {this.state.model.tags.map(t => (<div key={t.id} className="d-inline p-1 ps-2 pe-2 me-1 mt-2 bg-dark rounded-pill text-nowrap">{t.name}</div>))}
                            </div>
                        </div>
                        {!!this.state.model.description ?
                            (<div className="row mt-2 border-top pt-1">
                                <h5>Description</h5>
                                <div>
                                    {this.state.model.description}
                                </div>
                            </div>)
                            :
                            (<></>)}
                    </div>
                    <div className="row border-top pt-1">

                    </div>
                    {JSON.stringify(this.state)}
                </div>
            </>)
            :
            (<></>);
    }
}

export class ModelCard extends Component<ModelType & { navigate: (path: string) => void }> {
    vidRef: React.RefObject<HTMLVideoElement>;
    imgRef: React.RefObject<HTMLImageElement>;
    constructor(props: any) {
        super(props);
        this.imgRef = React.createRef();
        this.vidRef = React.createRef();
        this.fixWoopsieDaisy = this.fixWoopsieDaisy.bind(this);
    }

    getCheckColor() {
        switch (this.props.status) {
            case "APPROVED":
                return "bg-success";
            default:
                return "bg-warning";
        }
    }

    getStatusIconType() {
        switch (this.props.status) {
            case "APPROVED":
                return (<i className="bi bi-check2" />);
            default:
                return (<i className="bi bi-question" />);
        }
    }

    getPlatformIcon() {
        switch (this.props.platform) {
            case "PC":
                return (<i className="bi bi-display" />);
            default:
                return (<i className="bi bi-phone" />);
        }
    }

    fixWoopsieDaisy() {
        this.imgRef.current.style.display = "none";
        this.vidRef.current.style.display = "inline";
    }

    getTumbnail() {
        let thumb = this.props.thumbnail;
        if (thumb.endsWith(".webm")) {
            return (<video ref={this.vidRef} className="card-img-top" style={{ width: 259, height: 259, margin: "-0.5rem -1rem" }} autoPlay loop muted playsInline >
                <source src={this.props.thumbnail} type="video/webm"></source>
                <source src="isfmoment.webm" type="video/webm"></source>
            </video>)
        }
        else {
            return (<>
                <img ref={this.imgRef} className="card-img-top" src={this.props.thumbnail} alt="you're not supposed to see this" style={{ width: 259, height: 259, objectFit: "cover", margin: "-0.5rem -1rem" }} onError={this.fixWoopsieDaisy} />
                <video ref={this.vidRef} className="card-img-top" style={{ width: 259, height: 259, margin: "-0.5rem -1rem", display: "none" }} autoPlay loop muted playsInline>
                    <source src="isfmoment.webm" type="video/webm"></source>
                </video>
            </>)
        }
    }

    render() {
        return (<div className="card bg-dark mb-5" style={{ width: 259 }}>
            <div className="card-header" style={{ position: "relative" }}>
                <div style={{ width: 259, height: 259 }}>
                    {this.getTumbnail()}

                </div>
                <h4 className="mt-3">
                    {this.props.name}
                </h4>
                <div className={this.getCheckColor() + " rounded-pill d-inline-flex justify-content-center text-dark me-4"} style={{ width: 20, height: 20 }}>{this.getStatusIconType()}</div>
                {this.getPlatformIcon()}
            </div>
            <div className="card-body">
                <div className="mb-2">
                    Tags
                    <br />
                    <div className="d-flex flex-wrap">
                        {this.props.tags.map(t => (<div key={t.id} className="rounded-pill outline outline-light d-inline text-nowrap me-1 ps-2 pe-2 mt-1" style={{ fontSize: ".75rem" }}>{t.name}</div>))}
                    </div>
                </div>
                <div style={{ width: "100%", borderBottom: "1px solid rgba(0, 0, 0, 0.125)" }} />
                <div>
                    Users:
                    <br />
                    {this.props.users.map((t, i, a) => {
                        return (<>
                            <a href="#" className="link-primary" style={{ textDecoration: "none" }}>{t.name}</a>
                            {(i + 1) < a.length ? " & " : ""}
                        </>)
                    })}
                </div>
            </div>
            <div className="card-footer">
                <div className="row" style={{ margin: "-0.5rem -1rem" }}>
                    <a href="#" className="col-4 btn btn-sm btn-outline-light" style={{ borderTopRightRadius: 0, borderBottomRightRadius: 0, borderTopLeftRadius: 0 }}>Install</a>
                    <a href="#" className="col-4 btn btn-sm btn-outline-light" style={{ borderRadius: 0, borderLeft: 0, borderRight: 0 }}>Download</a>
                    <button className="col-4 btn btn-sm btn-outline-light" style={{ borderTopLeftRadius: 0, borderBottomLeftRadius: 0, borderTopRightRadius: 0 }} onClick={() => this.props.navigate(`/model/${this.props.uuid}`)}>Show</button>
                </div>
            </div>
        </div>);
    }
}
