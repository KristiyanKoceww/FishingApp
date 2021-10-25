import React, { useState } from "react";
import { Form, Button } from "react-bootstrap";
import "./CreateReservoir.css";

const CreateReservoir = () => {
//   const [state, setState] = useState({
//     name: "",
//     type: "",
//     description: "",
//     latitude: 0,
//     longitude: 0,
//     image: null,
//     city: null,
//     fish: [],
//   });

  const [name, setName] = useState('');
  const [type, setType] = useState('');
  const [description, setDescription] = useState('');
  const [latitude, setLatitude] = useState(0);
  const [longitude, setLongitude] = useState(0);
  const [image, setImage] = useState(null);
  const [city, setCity] = useState(null);
  const [fish, setFish] = useState([]);

//   const { name, type, description, latitude, longitude, image, city, fish } =
//   name,type,description,latitude,longitude,image,city,fish;

  const onSubmit = async (e) => {
    e.preventDefault();
    const newReservoir = {
      name,
      type,
      description,
      latitude,
      longitude,
      image,
      city,
      fish,
    };

    console.log(newReservoir);
    fetch("https://localhost:44366/api/Reservoir/create", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(newReservoir),
    }).catch((error) => {
      console.error("Error:", error);
    });
  };


 
  return (
    <div>
      <h1 className="d-flex align-items-center justify-content-center">
        Create new reservoir
      </h1>
      <div className="d-flex align-items-center justify-content-center">
        <Form className="form" onSubmit={onSubmit}>
          <Form.Group className="mb-3" >
            <Form.Label>Reservoir name</Form.Label>
            <Form.Control  type="text" placeholder="Enter name"  onChange = {(event) => setName({name:event.target.value})}/>
          </Form.Group>

          <Form.Group className="mb-3" >
            <Form.Label>Reservoir type</Form.Label>
            <Form.Control type="text" placeholder="Enter type" onChange = {(event) => setType({type:event.target.value})} />
          </Form.Group>

          <Form.Group className="mb-3" >
            <Form.Label>Reservoir description</Form.Label>
            <Form.Control type="text" placeholder="Enter description" onChange = {(event) => setDescription({description:event.target.value})} />
          </Form.Group>

          <Form.Group className="mb-3" >
            <Form.Label>Reservoir latitude</Form.Label>
            <Form.Control type="text" placeholder="Enter latitude" onChange = {(event) => setLatitude({latitude:event.target.value})} />
          </Form.Group>

          <Form.Group className="mb-3" >
            <Form.Label>Reservoir longitude</Form.Label>
            <Form.Control type="text" placeholder="Enter longitude" onChange = {(event) => setLongitude({longitude:event.target.value})}/>
          </Form.Group>

          <Form.Group className="mb-3" >
            <Form.Label>Reservoir's fish</Form.Label>
            <Form.Control type="text" placeholder="Enter fish" onChange = {(event) => setFish({fish:event.target.value})}/>
          </Form.Group>

          <Form.Group className="mb-3" >
            <Form.Label>Reservoir's city</Form.Label>
            <Form.Control type="text" placeholder="Enter city" onChange = {(event) => setCity({city:event.target.value})} />
          </Form.Group>

          
          <Form.Group className="mb-3" >
            <Form.Label>Reservoir images</Form.Label>
            <Form.Control type="file" placeholder="Select images" onChange = {(event) => setImage({image:event.target.value})}/>
          </Form.Group>

          <Button variant="primary" type="submit">
            Submit
          </Button>
        </Form>
      </div>
    </div>
  );
};

export default CreateReservoir;
