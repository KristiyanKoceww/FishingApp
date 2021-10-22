import React from 'react';
import {useHistory} from 'react-router-dom';

const UserDetails = (props) => {

const history = useHistory();
const data = history.location.state.data;
console.log(data);

  return (
    <div className="container mt-4 mb-4 p-3 d-flex justify-content-center">
      <div className="card p-4">
        <div className=" image d-flex flex-column justify-content-center align-items-center">
          {" "}
          <button className="btn btn-secondary">
            {" "}
            <img
              src="https://i.imgur.com/wvxPV9S.png"
              height="100"
              width="100"
            />
          </button>{" "}
          <span className="name mt-3">{data.FirstName}</span>{" "}
          <span className="idd">{props.LastName}</span>
          <div className="d-flex flex-row justify-content-center align-items-center gap-2">
            {" "}
            <span className="idd1">Age:{props.Age}</span>{" "}
          </div>
          <div className="d-flex flex-row justify-content-center align-items-center mt-3">
            {" "}
            <span className="number">
            PhoneNumber <span className="follow">{props.PhoneNumber}</span>
            </span>{" "}
          </div>
          <div className=" d-flex mt-2">
            {" "}
            <button className="btn1 btn-dark">Edit Profile</button>{" "}
          </div>
          <div className="text mt-3">
            <span>
             Email : {props.Email}
              <br />
              <br />Username: {props.propsname}{" "}
            </span>
          </div>
          <div className="gap-3 mt-3 icons d-flex flex-row justify-content-center align-items-center">
            {" "}
            <span>
              <i className="fa fa-twitter"></i>
            </span>{" "}
            <span>
              <i className="fa fa-facebook-f"></i>
            </span>{" "}
            <span>
              <i className="fa fa-instagram"></i>
            </span>{" "}
            <span>
              <i className="fa fa-linkedin"></i>
            </span>{" "}
          </div>
          <div className=" px-2 rounded mt-4 date ">
            {" "}
            <span className="join">Joined : {props.CreatedOn}</span>{" "}
          </div>
        </div>
      </div>
    </div>
  );
};

export default UserDetails
