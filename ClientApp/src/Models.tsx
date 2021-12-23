import React, { Component } from "react";
import { RouteComponentProps } from "react-router-dom";

export class Model extends Component<RouteComponentProps<{ id: string }>, { model?: ModelData }> {
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
                                users {
                                    name
                                    discordId
                                }
                                tags {
                                    name
                                }
                                thumbnail
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
        return this.state.model !== undefined ?
            (<>
                <div className="row mt-2">
                    <div className="col-4 border-end">
                        <img className="rounded" style={{ height: "100%", width: "100%" }} src={this.state.model.thumbnail} />
                    </div>
                    <div className="col-8">
                        <h1>{this.state.model.name}</h1>
                        <div className="row border-top pt-1">
                            <h3>Users</h3>
                            <div className="row" style={{ marginTop: -12 }}>
                                {this.state.model.users.map(t => (<a className="fs-5 text-decoration-none" style={{ cursor: "pointer" }}>{t.name}</a>))}
                            </div>
                        </div>
                        <div className="row border-top pt-2">
                            <div className="col-6 text-center"><a className="h-100 w-100 btn btn-dark">One Click Install</a></div>
                            <div className="col-6 text-center"><a className="h-100 w-100 btn btn-dark">Download</a></div>
                        </div>
                        <div className="row mt-2 border-top pt-1">
                            <h5>Tags</h5>
                            <div className="d-flex">
                                {this.state.model.tags.map(t => (<div className="d-inline p-1 ps-2 pe-2 me-1 bg-dark rounded-pill">{t.name}</div>))}
                            </div>
                        </div>
                        <div>

                        </div>
                        {JSON.stringify(this.state)}
                    </div>
                </div>
            </>)
            :
            (<></>);
    }
}

export class ModelCard extends Component<ModelData & { navigate: (path: string) => void }> {
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

    render() {
        return (<div className="card bg-dark mb-5" style={{ width: 259 }}>
            <div className="card-header" style={{ position: "relative" }}>
                <img ref={this.imgRef} className="card-img-top" src={this.props.thumbnail} alt="you're not supposed to see this" style={{ width: 259, height: 259, objectFit: "cover", margin: "-0.5rem -1rem" }} onError={this.fixWoopsieDaisy} />
                <video ref={this.vidRef} className="card-img-top" style={{ width: 259, height: 259, margin: "-0.5rem -1rem", display:"none" }} autoPlay loop muted playsInline>
                    <source src="isfmoment.webm" type="video/webm"></source>
                </video>
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
                        {this.props.tags.map(t => (<div className="rounded-pill outline outline-light d-inline text-nowrap me-1 ps-2 pe-2 mt-1" style={{ fontSize: ".75rem" }}>{t.name}</div>))} 
                    </div>
                </div>
                <div style={{ width: "100%", borderBottom: "1px solid rgba(0, 0, 0, 0.125)" }} />
                <div>
                    Users:
                    <br />
                    {this.props.users.map((t, i, a) => {
                        return (<>
                            <a className="link-primary" style={{ textDecoration: "none" }}>{t.name}</a>
                            {(i + 1) < a.length ? " & " : ""}
                        </>)
                    })}
                </div>
            </div>
            <div className="card-footer">
                <div className="row" style={{ margin: "-0.5rem -1rem" }}>
                    <a className="col-4 btn btn-sm btn-outline-light" style={{ borderTopRightRadius: 0, borderBottomRightRadius: 0, borderTopLeftRadius: 0 }}>Install</a>
                    <a className="col-4 btn btn-sm btn-outline-light" style={{ borderRadius: 0, borderLeft: 0, borderRight: 0 }}>Download</a>
                    <a className="col-4 btn btn-sm btn-outline-light" style={{ borderTopLeftRadius: 0, borderBottomLeftRadius: 0, borderTopRightRadius: 0 }} onClick={() => this.props.navigate(`/model/${this.props.uuid}`)}>Show</a>
                </div>
            </div>
        </div>);
    }
}

export interface User {
    name: string;
    bSaber: string;
    discordId: string;
    level: string;
    models: Pagination<ModelData>;
}

export interface Tag {
    name: string;
    id: number;
    models: Pagination<ModelData>;
}

export interface ModelData {
    uuid: string;
    thumbnail: string;
    name: string;
    status: string;
    platform: string;
    date: Date;
    hash: string;
    id: number;
    type: string;
    downloadPath: string;
    userId: string;
    mainUser: User[];
    users: User[];
    tags: Tag[];
}

export interface Edge<T> {
    cursor: string;
    node: T;
}

export interface Pagination<T> {
    items: T[];
    pageInfo: PageInfo;
    edges: Edge<T>;
    totalCount: number;
}

export interface PageInfo {
    hasNextPage: boolean;
    hasPreviousPage: boolean;
    startCursor: string;
    endCursor: string;
}

export interface GQLData {
    model: ModelData;
    models: Pagination<ModelData>;
    tags: Pagination<Tag>;
}

type StringNumber = string | number;

export interface GQLError {
    message: string;
    locations: Location[];
    path: StringNumber[];
    extensions: Extensions;
}

export interface Location {
    line: number;
    column: number;
}

export interface Extensions {
    code: string;
    codes: string[];
}

export interface GQLReturn {
    data: GQLData;
    errors: GQLError[];
}