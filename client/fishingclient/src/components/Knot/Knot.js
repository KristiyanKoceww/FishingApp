import React from "react";
import {Link } from "react-router-dom";

const Knot = (knot) => {
  return (
    <div className="col-sm-4 my-2">
      <div className="card shadow-sm w-100" style={{ minHeight: 225 , minWidth: 420 }}>
        <div className="card-body">
          <h5 className="card-title text-center h3">{knot.Name}</h5>
          <img src={knot.ImageUrls[0].ImageUrl} alt='knot' height='150px' width='370px'/>
          <hr/>
          <div style={{ display: 'flex', justifyContent: 'center' }}>
          <Link className='btn btn-primary' to={"/knotInfoPage/" + knot.Name }> Learn more about {knot.Name}</Link>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Knot;