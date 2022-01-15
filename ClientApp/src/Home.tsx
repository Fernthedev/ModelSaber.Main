import React, { Component } from "react";
import { withRouter, RouteComponentProps } from "react-router-dom";
import Models from "./components/Models";
import { ModelSaberQuery, ModelType } from "./graphqlTypes";
import { WithGetModelsProps, withGetModels } from "./graphql";
import ModelFilter, { ModelFilterState } from "./components/ModelFilter";
import { Loader } from "./components/Loader";

class Home extends Component<RouteComponentProps & WithGetModelsProps, ModelFilterState & { cursor: string | null }> {
    constructor(props: any) {
        super(props);
        this.loadMore = this.loadMore.bind(this);
        this.nextPath = this.nextPath.bind(this);
        this.state = { page: 1, size: 60, cursor: null };
    }

    loadMore() {
        this.props.setHookState({ after: this.state.cursor, first: this.state.size });
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
            <ModelFilter page={this.state.page} size={this.state.size} pageMove={(page, cursor) => this.setState({ page: page, cursor: cursor }, this.loadMore)} setSize={(size) => this.setState({ size: size }, this.loadMore)} />
            <div className="d-flex flex-wrap justify-content-between" style={{ margin: "0 -30px" }}>
                {this.props.data.models.items.map(model => (<Models.ModelCard key={model.uuid} {...model} navigate={this.nextPath} />))}
            </div>
            <ModelFilter page={this.state.page} size={this.state.size} pageMove={(page, cursor) => this.setState({ page: page, cursor: cursor }, this.loadMore)} setSize={(size) => this.setState({ size: size }, this.loadMore)} />
        </div>);
    }
}

export default withRouter(withGetModels(Home));