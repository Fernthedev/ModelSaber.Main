import React, { useState } from "react";
import { withRouter, RouteComponentProps } from "react-router-dom";
import { useGetModelsQuery } from "../../graphqlTypes";
import { Loader } from "../Loader";
import { ModelCard } from "./ModelCard";
import ModelFilter from "./ModelFilter";

export default withRouter(function Models(props: RouteComponentProps) {
    const [size, setSize] = useState(10);
    const [cursor, setCursor] = useState("");
    const [filter, setFilter] = useState("");
    const [page, setPage] = useState(0);
    const { data, loading, error } = useGetModelsQuery({ variables: { first: size, after: cursor } });

    props.location.hash = `p${page}s${size}`;

    function navigate(path: string) {
        props.history.push(path);
    }

    if (loading) return (<Loader></Loader>);

    return (<>
        <ModelFilter
            filter={filter}
            page={page + 1}
            size={size}
            pageMove={(page, cursor) => { setPage(page); setCursor(cursor); }}
            setSize={setSize}
            setFilter={setFilter} />
        <div className="d-flex flex-wrap justify-content-between" style={{ margin: "0 -30px" }}>
            {data.models.items.map(model => (<ModelCard key={model.uuid} {...model} navigate={navigate} />))}
        </div>
        <ModelFilter
            filter={filter}
            page={page + 1}
            size={size}
            pageMove={(page, cursor) => { setPage(page); setCursor(cursor); }}
            setSize={setSize}
            setFilter={setFilter} />
    </>);
})