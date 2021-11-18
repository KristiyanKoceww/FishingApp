import React from "react";
import { Redirect } from "react-router-dom";

const Logout = (e) => {
  localStorage.removeItem("jwt");
  localStorage.removeItem("userId");

  return (
    <Redirect to="/" />
  )
}
export default Logout