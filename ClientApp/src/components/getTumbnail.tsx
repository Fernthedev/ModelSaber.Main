import React from "react";

export function getTumbnail(props: { thumbnail: string; }, vidRef: React.RefObject<HTMLVideoElement>, imgRef: React.RefObject<HTMLImageElement>, onError: React.ReactEventHandler<HTMLImageElement>, css: React.CSSProperties) {
    let thumb = props.thumbnail;
    if (thumb.endsWith(".webm")) {
        return (<video ref={vidRef} className="card-img-top" style={css} autoPlay loop muted playsInline>
            <source src={props.thumbnail} type="video/webm"></source>
            <source src="isfmoment.webm" type="video/webm"></source>
        </video>);
    }
    else {
        return (<>
            <img ref={imgRef} className="card-img-top" src={props.thumbnail} alt="you're not supposed to see this" style={Object.assign(css, { objectFit: "cover" })} onError={onError} />
            <video ref={vidRef} className="card-img-top" style={{ width: 259, height: 259, margin: "-0.5rem -1rem", display: "none" }} autoPlay loop muted playsInline>
                <source src="isfmoment.webm" type="video/webm"></source>
            </video>
        </>);
    }
}
