import React from 'react';
import '../Fish/Fish.css'
import {
  Card, CardImg, CardText, CardBody,
  CardTitle, CardSubtitle, Button
} from 'react-bootstrap';
import { CardGroup } from 'react-bootstrap';


const Fish = (props) => {
  console.log(props);
  
  return(
    <CardGroup>
  <Card>
    <Card.Img variant="top" src={props.ImageUrls[0].ImageUrl} width='100px' height="150px" alt="Fish Image"/>
    <Card.Body>
      <Card.Title>{props.Name}</Card.Title>
      <Card.Text>
        {props.Description.slice(0, 40)}
      </Card.Text>
    <Button variant="primary">Go somewhere</Button>
    </Card.Body>
  </Card>
  <br />
  <Card>
    <Card.Img variant="top" src={props.ImageUrls[0].ImageUrl} width='100px' height="150px" alt="Fish Image"/>
    <Card.Body>
      <Card.Title>{props.Name}</Card.Title>
      <Card.Text>
      {props.Description.slice(0, 40)}
      </Card.Text>
    <Button variant="primary">Go somewhere</Button>
    </Card.Body>
  </Card>
  <br />
  <Card>
    <Card.Img variant="top" src={props.ImageUrls[0].ImageUrl} width='100px' height="150px" alt="Fish Image"/>
    <Card.Body>
      <Card.Title>{props.Name}</Card.Title>
      <Card.Text>
      {props.Description.slice(0, 40)}
      </Card.Text>
    <Button variant="primary">Go somewhere</Button>
    </Card.Body>
  </Card>
  <br />
  <Card>
    <Card.Img variant="top" src={props.ImageUrls[0].ImageUrl} width='100px' height="150px" alt="Fish Image"/>
    <Card.Body>
      <Card.Title>{props.Name}</Card.Title>
      <Card.Text>
      {props.Description.slice(0, 40)}
      </Card.Text>
    <Button variant="primary">Go somewhere</Button>
    </Card.Body>
  </Card>
  <br />
  <Card>
    <Card.Img variant="top" src={props.ImageUrls[0].ImageUrl} width='100px' height="150px" alt="Fish Image"/>
    <Card.Body>
      <Card.Title>{props.Name}</Card.Title>
      <Card.Text>
      {props.Description.slice(0, 40)}
      </Card.Text>
    <Button variant="primary">Go somewhere</Button>
    </Card.Body>
  </Card>
</CardGroup>
  )

        }
  
  
  export default Fish;