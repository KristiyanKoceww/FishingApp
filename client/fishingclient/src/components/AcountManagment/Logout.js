import React  from "react";
import { useContext } from "react";
import { Redirect } from "react-router-dom";
import { UserContext } from './UserContext';

const Logout = (e) => {
  localStorage.removeItem("jwt");
  localStorage.removeItem("refresh");
  localStorage.removeItem("userId");
  const {appUser,setAppUser} = useContext(UserContext);
  setAppUser(null);
  
  return (
    <Redirect to="/Login" />
  )
}
export default Logout