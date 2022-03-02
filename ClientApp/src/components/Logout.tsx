import React from "react";
import { useNavigate } from "react-router-dom";
import { events } from "../App";

export default function Logout() {
    const navigate = useNavigate();
    document.cookie = "login=;path=/;expires=Thu, 01 Jan 1970 00:00:01 GMT";
    events.emitLogin();
    navigate("/");
    return (<></>);
}; 