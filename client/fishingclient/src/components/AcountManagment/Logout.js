import React from "react";
import { Redirect } from "react-router-dom";

const Logout = (e) => {
      localStorage.removeItem("jwt");

    return(
      <Redirect to="/" />
    )
  }
   
  
  //https://localhost:44366/api/AppUsers/logout
    // await fetch('https://localhost:44366/api/Account/logout',
    //   {
    //     method: 'POST',
    //      headers: {
    //        Authorization: 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6ImZhYTVkMDBkLWU1NmEtNDViNi05N2U0LTVhYjcyMWM0ZDc5MCIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJrb2Nld3dAZ21haWwuY29tIiwibmJmIjoxNjM1MzI1NDE2LCJleHAiOjE2MzUzMjU3MTYsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjQ0MzY2LyIsImF1ZCI6Imh0dHBzOi8vbG9jYWxob3N0OjMwMDAvIn0.ZqttPejWm3R6uSAK1ZNXbnzyZJDDAIYT992tkJdUvus'
    //      },
    //     credentials: 'include',
    //   });
    
export default Logout