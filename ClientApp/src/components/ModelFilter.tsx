import React, { Component } from "react";
import { withGetModelCursors, WithGetModelCursorsProps } from "../graphql";

type ModelFilterProps = WithGetModelCursorsProps & ModelFilterState & { pageMove: (page: number, cursor: string) => void, setSize: (size: number) => void };

export interface ModelFilterState {
    size: number;
    page: number;
}

class ModelFilter extends Component<ModelFilterProps, ModelFilterState>{
    constructor(props: ModelFilterProps) {
        super(props);
        this.state = { page: props.page, size: props.size };
    }

    componentWillReceiveProps(nextProps: Readonly<ModelFilterProps>, nextContext: any): void {
        this.setState({ size: nextProps.size, page: nextProps.page })
    }

    updateSize(num: number) {
        this.setState({ size: num }, this.setNextSize);
    }

    setNextSize() {
        this.props.setCursorSize({ size: this.state.size });
        this.props.setSize(this.state.size);
    }

    updatePage(num: number) {
        if (num > this.props.data.modelCursors.length) this.setState({ page: 1 }, this.setNextPage);
        else if (num < 1) this.setState({ page: this.props.data.modelCursors.length }, this.setNextPage);
        else this.setState({ page: num }, this.setNextPage);
    }

    setNextPage() {
        this.props.pageMove(this.state.page, this.props.data.modelCursors[this.state.page - 1]);
    }

    getPages() {
        var userPagesFirst = Array(this.props.data.modelCursors.length).fill(null).map((_, i) => i + 1);
        var userPagesStart: number[] = [];
        var userPagesMiddle: number[] = [];
        var userPagesEnd: number[] = [];
        if (userPagesFirst.length > 8) {
            if (this.state.page > 5) {
                userPagesStart = [userPagesFirst[0]];
                if (userPagesFirst.length < this.state.page + 5) {
                    userPagesEnd = userPagesFirst.slice(userPagesFirst.length - 7, userPagesFirst.length);
                }
                else {
                    userPagesMiddle = userPagesFirst.slice(this.state.page - 3, 4 + (this.state.page - 2));
                    userPagesEnd = [userPagesFirst.pop()];
                }
            }
            else {
                userPagesMiddle = userPagesFirst.slice(0, 7);
                userPagesEnd = [userPagesFirst.pop()];
            }
        }
        else {
            userPagesMiddle = userPagesFirst;
        }
        return (<>
            {userPagesStart.length > 0 ? userPagesStart.map(f => (<li className={"page-item" + (this.state.page === f ? " active" : "")}>
                <a className="page-link" href="#" key={f} onClick={() => this.updatePage(f)}>{f}</a>
            </li>)) : null}
            {userPagesStart.length > 0 ? (<li className="page-item disabled">
                <span className="page-link">...</span>
            </li>) : null}
            {userPagesMiddle.length > 0 ? userPagesMiddle.map(f => (<li className={"page-item" + (this.state.page === f ? " active" : "")}>
                <a className="page-link" href="#" key={f} onClick={() => this.updatePage(f)}>{f}</a>
            </li>)) : null}
            {userPagesMiddle.length > 0 ? (<li className="page-item disabled">
                <span className="page-link">...</span>
            </li>) : null}
            {userPagesEnd.length > 0 ? userPagesEnd.map(f => (<li className={"page-item" + (this.state.page === f ? " active" : "")}>
                <a className="page-link" href="#" key={f} onClick={() => this.updatePage(f)}>{f}</a>
            </li>)) : null}
        </>);
    }

    render() {
        if (this.props.loading) return (<></>);
        return (<nav aria-label="Page navigation example">
            <ul className="pagination">
                <li className="page-item"><a className="page-link" href="#" onClick={() => this.updatePage(this.state.page - 1)}>Previous</a></li>
                {this.getPages()}
                <li className="page-item"><a className="page-link" href="#" onClick={() => this.updatePage(this.state.page + 1)}>Next</a></li>
                <li className="page-item">
                    <div className="dropdown">
                        <button className="btn btn-secondary dropdown-toggle" type="button" id="pageSizeDropdown" data-bs-toggle="dropdown" aria-expanded="false">Page Size</button>
                        <ul className="dropdown-menu dropdown-menu-dark" aria-labelledby="pageSizeDropdown">
                            <li><a className={"dropdown-item" + (this.state.size === 10 ? " active" : "")} href="#" onClick={() => this.updateSize(10)}>10</a></li>
                            <li><a className={"dropdown-item" + (this.state.size === 20 ? " active" : "")} href="#" onClick={() => this.updateSize(20)}>20</a></li>
                            <li><a className={"dropdown-item" + (this.state.size === 40 ? " active" : "")} href="#" onClick={() => this.updateSize(40)}>40</a></li>
                            <li><a className={"dropdown-item" + (this.state.size === 60 ? " active" : "")} href="#" onClick={() => this.updateSize(60)}>60</a></li>
                            <li><a className={"dropdown-item" + (this.state.size === 80 ? " active" : "")} href="#" onClick={() => this.updateSize(80)}>80</a></li>
                            <li><a className={"dropdown-item" + (this.state.size === 100 ? " active" : "")} href="#" onClick={() => this.updateSize(100)}>100</a></li>
                        </ul>
                    </div>
                </li>
            </ul>
        </nav>);
    }
}

export default withGetModelCursors(ModelFilter);