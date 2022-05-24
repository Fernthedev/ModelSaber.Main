import React from "react";

export default function ModelUpload() {
    return (<>
        <div className="col d-flex">
            <div className="me-2 text-center">
                <text className="">
                    By uploading models to ModelSaber, you are
                </text>
            </div>
            <div className="mt-2">
                <form className="column">
                    <label>
                        <input type="text" />
                    </label>
                    <input className="btn btn-primary" type="submit" />
                </form>
            </div>
        </div>
    </>)
}
