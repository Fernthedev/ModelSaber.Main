import React, { Component } from "react";
import { withRouter } from "react-router-dom";
import { ModelCard } from "./components/Models";
import { ModelSaberQuery, ModelType } from "./graphqlTypes";
import { GQLReturn } from "./graphql";
import { Loader } from "./components/Loader";

class Home extends Component<any, { gql?: ModelSaberQuery, after?: string, models: ModelType[], loading: boolean }> {
    constructor(props: any) {
        super(props);
        this.state = { models: [], loading: true };
        this.loadMore = this.loadMore.bind(this);
        this.nextPath = this.nextPath.bind(this);
    }

    loadMore() {
        this.setState({ after: this.state.gql.models.pageInfo.endCursor, loading: true }, this.componentDidMount);
    }

    nextPath(path: string) {
        this.props.history.push(path);
    }

    componentDidMount() {
        fetch(process.env.REACT_APP_API_URL + "/graphql", {
            method: "POST", body: JSON.stringify({
                query: `query GetModel($first: Int, $after: String) {
                            models(first: $first, after: $after) {
                                items {
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
                                pageInfo {
                                    endCursor
                                }
                            }
                        }`,
                variables: { first: 60, after: this.state.after }
            }),
            headers: {
                'Content-Type': 'application/json',
                'Accept': 'application/json',
            }
        }).then(res => res.ok ? res.json() as Promise<GQLReturn> : undefined).then(res => this.setState({ gql: res.data, models: this.state.models.concat(res.data.models.items), loading: false }));
    }

    render() {
        return (<div>
            <h1 className="align-middle">
                Welcome to ModelSaber
            </h1>
            <div className="d-flex flex-wrap justify-content-between" style={{ margin: "0 -30px" }}>
                {this.state.models.map(model => (<ModelCard key={model.uuid} {...model} navigate={this.nextPath} />))}
            </div>
            <button className="btn btn-primary" onClick={() => this.loadMore()}>Load more</button>
            {this.state.loading ? <Loader></Loader> : <></>}
        </div>);
    }
}

export default withRouter(Home);