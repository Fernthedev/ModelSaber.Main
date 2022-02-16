import React from "react";
import { useGetModelCursorsQuery } from "../../graphqlTypes";

type ModelFilterProps = ModelFilterState & { pageMove: (page: number, cursor: string) => void, setSize: (size: number) => void, setFilter: (filter: string) => void };

export interface ModelFilterState {
    size: number;
    page: number;
    filter: string;
}

function ModelFilterFunc(props: ModelFilterProps) {
    const page = props.page;
    const size = props.size;
    const filter = props.filter;
    const { data, loading } = useGetModelCursorsQuery({ variables: { size: size } });

    if (loading) return (<></>);

    const cursors = ["", ...data.modelCursors];

    function updateSize(s: number) {
        props.setSize(s);
    }

    function updatePage(p: number) {
        if (p > cursors.length) p = 1;
        else if (p < 1) p = cursors.length;

        props.pageMove(p - 1, cursors[p - 1]);
    }

    function updateFilter(f: string) {
        props.setFilter(f);
    }

    function getPages(length: number) {
        var userPagesFirst = Array(length).fill(null).map((_, i) => i + 1);
        var userPagesStart: number[] = [];
        var userPagesMiddle: number[] = [];
        var userPagesEnd: number[] = [];
        if (userPagesFirst.length > 8) {
            if (page > 5) {
                userPagesStart = [userPagesFirst[0]];
                if (userPagesFirst.length < page + 5) {
                    userPagesEnd = userPagesFirst.slice(userPagesFirst.length - 7, userPagesFirst.length);
                }
                else {
                    userPagesMiddle = userPagesFirst.slice(page - 3, 4 + (page - 2));
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
            {userPagesStart.length > 0 ? userPagesStart.map(f => (<li key={f} className={"page-item" + (page === f ? " active" : "")}>
                <a className="page-link" href="#" key={f} onClick={() => updatePage(f)}>{f}</a>
            </li>)) : null}
            {userPagesStart.length > 0 ? (<li className="page-item disabled">
                <span className="page-link">...</span>
            </li>) : null}
            {userPagesMiddle.length > 0 ? userPagesMiddle.map(f => (<li key={f} className={"page-item" + (page === f ? " active" : "")}>
                <a className="page-link" href="#" key={f} onClick={() => updatePage(f)}>{f}</a>
            </li>)) : null}
            {userPagesMiddle.length > 0 ? (<li className="page-item disabled">
                <span className="page-link">...</span>
            </li>) : null}
            {userPagesEnd.length > 0 ? userPagesEnd.map(f => (<li key={f} className={"page-item" + (page === f ? " active" : "")}>
                <a className="page-link" href="#" key={f} onClick={() => updatePage(f)}>{f}</a>
            </li>)) : null}
        </>);
    }

    return (
        <nav>
            <ul className="pagination me-2">
                <li className="page-item"><a className="page-link" href="#" onClick={() => updatePage(page - 1)}>Previous</a></li>
                {getPages(cursors.length)}
                <li className="page-item"><a className="page-link" href="#" onClick={() => updatePage(page + 1)}>Next</a></li>
                <li className="page-item">
                    <div className="dropdown">
                        <button className="btn btn-secondary dropdown-toggle" type="button" id="pageSizeDropdown" data-bs-toggle="dropdown" aria-expanded="false">Page Size</button>
                        <ul className="dropdown-menu dropdown-menu-dark" aria-labelledby="pageSizeDropdown">
                            <li><a className={"dropdown-item" + (size === 10 ? " active" : "")} href="#" onClick={() => updateSize(10)}>10</a></li>
                            <li><a className={"dropdown-item" + (size === 20 ? " active" : "")} href="#" onClick={() => updateSize(20)}>20</a></li>
                            <li><a className={"dropdown-item" + (size === 40 ? " active" : "")} href="#" onClick={() => updateSize(40)}>40</a></li>
                            <li><a className={"dropdown-item" + (size === 60 ? " active" : "")} href="#" onClick={() => updateSize(60)}>60</a></li>
                            <li><a className={"dropdown-item" + (size === 80 ? " active" : "")} href="#" onClick={() => updateSize(80)}>80</a></li>
                            <li><a className={"dropdown-item" + (size === 100 ? " active" : "")} href="#" onClick={() => updateSize(100)}>100</a></li>
                        </ul>
                    </div>
                </li>
            </ul>
            <input value={filter} onInput={(event) => updateFilter(event.currentTarget.value)} />
        </nav>
    );
}

export default ModelFilterFunc;