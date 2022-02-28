import React from "react";
import { RouteComponentProps } from "react-router-dom";
import { useGetModelFullQuery, useGetModelVotesQuery } from "../../graphqlTypes";
import { GetTumbnail } from "./GetTumbnail";
import { Loader } from "../Loader";


export default function Model(props: RouteComponentProps<{ id: string }>) {
    const [{ data, fetching }] = useGetModelFullQuery({ variables: { modelId: props.match.params.id } });
    const votes = useGetModelVotesQuery({ variables: { modelId: props.match.params.id }, pause: fetching });

    if (fetching) return (<Loader></Loader>);
    if (!data) {
        props.history.push("/");
        return (<></>);
    }


    let model = data.model;
    return !!model ?
        (<>
            <div className="row mt-2">
                <div className="col-4 border-end pb-2">
                    <GetTumbnail {...model} css={{ width: "100%", borderRadius: "0.5rem" }} />
                </div>
                <div className="col-8">
                    <h1>{model.name}</h1>
                    <div className="row border-top pt-1">
                        <h3>Users</h3>
                        <div className="row" style={{ marginTop: -12 }}>
                            {model.users.map((t: any) => (<a key={t.discordId} href="#" className="fs-5 text-decoration-none" style={{ cursor: "pointer" }}>{t.name}</a>))}
                        </div>
                    </div>
                    <div className="row border-top pt-2 pb-2">
                        <div className="col-6 text-center"><a href={`modelsaber:${model.type}:${model.uuid}`} className="h-100 w-100 btn btn-dark">One Click Install</a></div>
                        <div className="col-6 text-center"><a href={model.downloadPath} target="_blank" className="h-100 w-100 btn btn-dark">Download</a></div>
                    </div>
                    <div className="row border-top pt-1">
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
                    <div className="row">
                        <div className="d-flex">
                            {votes[0].fetching ? (<h3>Loading...</h3>) : (<>
                                <h3 className="col-1">Votes:</h3>
                                <div className="col-2"><i className="bi bi-hand-thumbs-up-fill btn btn-secondary"></i><label className="ps-2">{votes[0].data.modelVotes.find(t => !t.down)?.count || 0}</label></div>
                                <div className="col-2"><i className="bi bi-hand-thumbs-down-fill btn btn-secondary"></i><label className="ps-2">{votes[0].data.modelVotes.find(t => t.down)?.count || 0}</label></div>
                                <div className="col-7"></div>
                            </>)}
                        </div>
                    </div>
                </div>
                <pre>
                    {JSON.stringify(model, null, 4)}
                </pre>
            </div>
        </>)
        :
        (<></>);
}