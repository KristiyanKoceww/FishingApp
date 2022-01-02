import React from "react";
import {Link } from "react-router-dom";

const Fish = (fish) => {
  return (
    <div className="col-sm-4 my-2">
      <div className="card shadow-sm w-100" style={{ minHeight: 225 , minWidth: 420 }}>
        <div className="card-body">
          <h5 className="card-title text-center h3">{fish.Name}</h5>
          <img src={fish.imageUrls[0].imageUrl} alt='fish' height='150px' width='370px'/>
          <hr/>
          <div style={{ display: 'flex', justifyContent: 'center' }}>
          <Link className='btn btn-primary' to={"/FishInfoPage/" + fish.name }> Learn more about {fish.name}</Link>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Fish;
