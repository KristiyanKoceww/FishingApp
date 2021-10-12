import react from "react";
import Carousel from 'react-bootstrap/Carousel'

const post = (props) =>{

   return(
    <div class="container">
    <div class="box image">
      <div class="box-header">
        <h3><a><img src="https://goo.gl/oOD0V2" alt="" />{props.User.FirstName}</a>
          <span>Date: {props.CreatedOn} <i class="fa fa-globe"></i></span>
        </h3>
        <span><i class="fa fa-angle-down"></i></span>
        <div class="window"><span></span></div>
      </div>
      <div class="box-content">
        <div class="content">
          <img src={props.ImageUrls[0].ImageUrl} width="250px" alt="image" />
        </div>
        <div class="bottom">
          <p >{props.Content}</p>
          <span><span class="fa fa-search-plus"></span></span>
        </div>
      </div>
      <div class="box-likes">
        <div class="row">
          <span><a ><img src="https://goo.gl/oM0Y8G" alt="" /></a></span>
          <span><a ><img src="https://goo.gl/vswgSn" alt="" /></a></span>
          <span><a ><img src="https://goo.gl/4W27eB" alt="" /></a></span>
          <span><a >{props.Vote}</a></span>
        </div>
        <div class="row">
          <span>{props.Comments}</span>
        </div>
      </div>
      </div>
      </div>
     
   )
}
     {/* return (
        <div>
        <h1>{props.Title}</h1>
        <p>{props.Content}</p>
        <div>
            <p>
                <li>User:{props.UserId}</li>
                <li>User Name:{props.User.FirstName}</li>
                <li>User Age:{props.User.Age}</li>
                <li>Email:{props.User.Email}</li>
              </p>
        </div>
              
              <div>
              <img src={props.ImageUrls[0].ImageUrl} alt="image post" width='250'></img> 
              </div>
       
    </div>
        );  */}

export default post;