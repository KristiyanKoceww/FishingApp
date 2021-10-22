import React from "react";
//import "../Fish/Fish.css";
// import {
//   Card,
//   CardImg,
//   CardText,
//   CardBody,
//   CardTitle,
//   CardSubtitle,
//   Button,
// } from "react-bootstrap";
// import { CardGroup } from "react-bootstrap";

const Fish = (fish) => {

  return (
    <div className="col-sm-4 my-2">
      <div className="card shadow-sm w-100" style={{ minHeight: 225 }}>
        <div className="card-body">
          <h5 className="card-title text-center h2">Id : {fish.Id}</h5>
          <h6 className="card-subtitle mb-2 text-muted text-center">{fish.Name}</h6>
          <p className="card-text">{fish.Weight}</p>
        </div>
      </div>
    </div>
  );
};
//   return(
//     <CardGroup>
//   <Card>
//     {/* <Card.Img variant="top" src={props.ImageUrls[0].ImageUrl} width='100px' height="150px" alt="Fish Image"/> */}
//     <Card.Body>
//       <Card.Title>{props.Name}</Card.Title>
//       <Card.Text>
//         {props.Description.slice(0, 40)}
//       </Card.Text>
//     <Button variant="primary">Go somewhere</Button>
//     </Card.Body>
//   </Card>
//   <br />
//   <Card>
//     {/* <Card.Img variant="top" src={props.ImageUrls[0].ImageUrl} width='100px' height="150px" alt="Fish Image"/> */}
//     <Card.Body>
//       <Card.Title>{props.Name}</Card.Title>
//       <Card.Text>
//       {props.Description.slice(0, 40)}
//       </Card.Text>
//     <Button variant="primary">Go somewhere</Button>
//     </Card.Body>
//   </Card>
//   <br />
//   <Card>
//     {/* <Card.Img variant="top" src={props.ImageUrls[0].ImageUrl} width='100px' height="150px" alt="Fish Image"/> */}
//     <Card.Body>
//       <Card.Title>{props.Name}</Card.Title>
//       <Card.Text>
//       {props.Description.slice(0, 40)}
//       </Card.Text>
//     <Button variant="primary">Go somewhere</Button>
//     </Card.Body>
//   </Card>
//   <br />
//   <Card>
//     {/* <Card.Img variant="top" src={props.ImageUrls[0].ImageUrl} width='100px' height="150px" alt="Fish Image"/> */}
//     <Card.Body>
//       <Card.Title>{props.Name}</Card.Title>
//       <Card.Text>
//       {props.Description.slice(0, 40)}
//       </Card.Text>
//     <Button variant="primary">Go somewhere</Button>
//     </Card.Body>
//   </Card>
//   <br />
//   <Card>
//     {/* <Card.Img variant="top" src={props.ImageUrls[0].ImageUrl} width='100px' height="150px" alt="Fish Image"/> */}
//     <Card.Body>
//       <Card.Title>{props.Name}</Card.Title>
//       <Card.Text>
//       {props.Description.slice(0, 40)}
//       </Card.Text>
//     <Button variant="primary">Go somewhere</Button>
//     </Card.Body>
//   </Card>
// </CardGroup>
//   )

//         }

export default Fish;
