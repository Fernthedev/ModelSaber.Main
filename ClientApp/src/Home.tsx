import React, { Component } from "react";
import { withRouter, RouteComponentProps } from "react-router-dom";
import Models from "./components/Models";
import { GetModelsQueryResult, GetModelsQueryVariables, ModelSaberQuery, ModelType } from "./graphqlTypes";
import { withGetModels } from "./graphql";
import { GQLReturn } from "./graphql";
import { Loader } from "./components/Loader";

class Home extends Component<RouteComponentProps & GetModelsQueryResult & { setHookState: React.Dispatch<React.SetStateAction<GetModelsQueryVariables>> }, { gql?: ModelSaberQuery, after?: string, models: ModelType[], loading: boolean }> {
    constructor(props: any) {
        super(props);
        this.loadMore = this.loadMore.bind(this);
        this.nextPath = this.nextPath.bind(this);
    }

    loadMore() {
        this.props.setHookState({ after: this.props.data.models.pageInfo.endCursor, first: 60 });
    }

    nextPath(path: string) {
        this.props.history.push(path);
    }

    render() {
        if (this.props.loading) return <Loader></Loader>;
        console.log(this.props.data.models.pageInfo);
        return (<div>
            <h1 className="align-middle">
                Welcome to ModelSaber
            </h1>
            <div className="d-flex flex-wrap justify-content-between" style={{ margin: "0 -30px" }}>
                {this.props.data.models.items.map(model => (<Models.ModelCard key={model.uuid} {...model} navigate={this.nextPath} />))}
            </div>
            <button className="btn btn-primary" onClick={() => this.loadMore()}>Load more</button>
        </div>);
    }
}

export default withRouter(withGetModels(Home));