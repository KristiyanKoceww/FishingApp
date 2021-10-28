import React from "react";
import Button from 'react-bootstrap/Button'
import { Redirect, Link ,useHistory } from "react-router-dom";

const Fish = (fish) => {
  const history = useHistory();

  
  const FishInfoToPage = () =>{
    history.push('/FishInfoPage', {
      myProp: fish.Id,
      foo: 'bar'
    })
    //return <Redirect to="/FishInfoPage" props={fish.Id} />
  }
  
  return (
    <div className="col-sm-4 my-2">
      <div className="card shadow-sm w-100" style={{ minHeight: 225 , minWidth: 420 }}>
        <div className="card-body">
          <h5 className="card-title text-center h2">{fish.Name}</h5>
          <img src={fish.ImageUrls[0].ImageUrl} alt='image' height='150px' width='370px'/>
          <hr/>
          <div style={{ display: 'flex', justifyContent: 'center' }}>
          <Button onClick={FishInfoToPage} variant="primary">Learn more about {fish.Name}</Button>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Fish;
