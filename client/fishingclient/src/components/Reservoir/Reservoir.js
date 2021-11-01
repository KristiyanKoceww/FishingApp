import React from "react";
import {Link } from "react-router-dom";

const Reservoir = (reservoir) => {
  return (
    <div className="col-sm-4 my-2">
      <div className="card shadow-sm w-100" style={{ minHeight: 225 , minWidth: 420 }}>
        <div className="card-body">
          <h5 className="card-title text-center h3">{reservoir.Name}</h5>
          <img src={reservoir.ImageUrls[0].ImageUrl} alt='reservoir' height='150px' width='370px'/>
          <hr/>
          <div style={{ display: 'flex', justifyContent: 'center' }}>
          <Link className='btn btn-primary' to={"/reservoirInfoPage/" + reservoir.Name }> Learn more about {reservoir.Name}</Link>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Reservoir;