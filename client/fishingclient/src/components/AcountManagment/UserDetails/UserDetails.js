import React from 'react';
import { Link } from "react-router-dom";
import './UserDetails.css'
const UserDetails = (props) => {

  return (
    <div className="user__details">
      <div className="card">
        <div className="text-center">
          <img className="profileimage" src={props.user.mainImageUrl ? props.user.mainImageUrl : null} />
          <h3 className="mt-3">{props.user.firstName} {props.user.lastName}</h3>
          <div >
            <h5>Posts: {props.user.postsCount}</h5>
          </div>
          <div>
            <h5>Username: {props.user.userName}</h5>
          </div>
          <div>
            <h5>Email: {props.user.email}</h5>
          </div>
          <div className="social-buttons mt-5">
            <button className="neo-button"><i className="fa fa-facebook fa-1x"></i> </button>
            <button className="neo-button"><i className="fa fa-google fa-1x"></i> </button>
          </div>
          <div className="profile mt-5">
            <Link className="profile_button px-5" to={"/UserProfile"}> View profile</Link>
            <hr />
          </div>
        </div>
      </div>
    </div>

  );
};

export default UserDetails
