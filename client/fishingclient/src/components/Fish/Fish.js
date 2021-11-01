import React from "react";
import {Link } from "react-router-dom";

const Fish = (fish) => {
  return (
    <div className="col-sm-4 my-2">
      <div className="card shadow-sm w-100" style={{ minHeight: 225 , minWidth: 420 }}>
        <div className="card-body">
          <h5 className="card-title text-center h3">{fish.Name}</h5>
          <img src={fish.ImageUrls[0].ImageUrl} alt='fish' height='150px' width='370px'/>
          <hr/>
          <div style={{ display: 'flex', justifyContent: 'center' }}>
          <Link className='btn btn-primary' to={"/FishInfoPage/" + fish.Name }> Learn more about {fish.Name}</Link>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Fish;
