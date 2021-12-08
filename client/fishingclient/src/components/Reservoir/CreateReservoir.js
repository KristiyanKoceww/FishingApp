import React, { useState } from "react";
import "./CreateReservoir.css";

import { TextField } from "@mui/material";
import Button from "@mui/material/Button";
import InputAdornment from "@mui/material/InputAdornment";
import TitleIcon from "@mui/icons-material/Title";
import ShortTextIcon from '@mui/icons-material/ShortText';
import ConnectWithoutContactIcon from "@mui/icons-material/ConnectWithoutContact";
import DescriptionIcon from '@mui/icons-material/Description';

const CreateReservoir = () => {
  const [name, setName] = useState("");
  const [type, setType] = useState("");
  const [description, setDescription] = useState("");
  const [latitude, setLatitude] = useState(1);
  const [longitude, setLongitude] = useState(1);
  const [cityId, setCityId] = useState("");
  const [images, setImages] = useState([]);

  const saveFile = (e) => {
    for (let index = 0; index < e.target.files.length; index++) {
      const element = e.target.files[index];
      setImages((images) => [...images, element]);
    }
  };

  const CreateRes= async (e) => {
    e.preventDefault();

    const formData = new FormData();
    for (let index = 0; index < images.length; index++) {
      const element = images[index];
      formData.append("images", element);
    }

    formData.append("name", name);
    formData.append("type", type);
    formData.append("description", description);
    formData.append("latitude", latitude);
    formData.append("longitude", longitude);
    formData.append("cityId", cityId);
    
    const jwt = localStorage.getItem("jwt");
    fetch("https://localhost:44366/api/Reservoir/create", {
      method: "POST",
      headers: { Authorization: "Bearer " + jwt },
      body: formData,
    });
  };

  return (

<div className="CreateReservoir">
      <form onSubmit={(e) => CreateRes(e)}>
        <h1 className="ReservoirTitle">
          <ConnectWithoutContactIcon /> Fill data to create new reservoir
        </h1>

        <div>
          <TextField
            className="textFieldTitle"
            label="Reservoir name"
            variant="filled"
            size="large"
            fullWidth
            onChange={(e) => setName(e.target.value)}
            required
            multiline
            InputProps={{
              startAdornment: (
                <InputAdornment position="start">
                  <TitleIcon />
                </InputAdornment>
              ),
            }}
          />
        </div>
        <br />
        <div>
          <TextField
            label="Type"
            id="filled-hidden-label-small"
            variant="filled"
            size="large"
            fullWidth
            onChange={(e) => setType(e.target.value)}
            required
            multiline
            InputProps={{
              startAdornment: (
                <InputAdornment position="start">
                  <ShortTextIcon />
                </InputAdornment>
              ),
            }}
          />
        </div>
        <br />
        <div>
          <TextField
            label="Description"
            id="filled-hidden-label-small"
            variant="filled"
            size="large"
            fullWidth
            onChange={(e) => setDescription(e.target.value)}
            required
            multiline
            InputProps={{
              startAdornment: (
                <InputAdornment position="start">
                  <DescriptionIcon />
                </InputAdornment>
              ),
            }}
          />
        </div>
        <br />
        <div>
          <TextField
            label="Latitude"
            id="filled-hidden-label-small"
            variant="filled"
            size="large"
            fullWidth
            onChange={(e) => setLatitude(e.target.value)}
            required
            multiline
            InputProps={{
              startAdornment: (
                <InputAdornment position="start">
                  <ShortTextIcon />
                </InputAdornment>
              ),
            }}
          />
        </div>
        <br />

        <div>
          <TextField
            label="Longitude"
            id="filled-hidden-label-small"
            variant="filled"
            size="large"
            fullWidth
            onChange={(e) => setLongitude(e.target.value)}
            required
            multiline
            InputProps={{
              startAdornment: (
                <InputAdornment position="start">
                  <ShortTextIcon />
                </InputAdornment>
              ),
            }}
          />
        </div>
        <br />
        <div>
          <TextField
            label="City id"
            id="filled-hidden-label-small"
            variant="filled"
            size="large"
            fullWidth
            onChange={(e) => setCityId(e.target.value)}
            required
            multiline
            InputProps={{
              startAdornment: (
                <InputAdornment position="start">
                  <ShortTextIcon />
                </InputAdornment>
              ),
            }}
          />
        </div>
        <br />

        <label htmlFor="btn-upload">
          <input
            id="btn-upload"
            name="btn-upload"
            style={{ display: "none" }}
            multiple
            type="file"
            onChange={saveFile}
          />
          <div className="choose__files">
            <Button
              className="choose__button"
              variant="outlined"
              component="span"
            >
              Choose Files
            </Button>{" "}
            <Button className="submit__button" type="submit" variant="outlined">
              Submit
            </Button>
          </div>
        </label>
        <br />
      </form>
    </div>


    // <main className="form-signin">
    //   <form onSubmit={uploadImage}>
    //     <h1 className="h3 mb-3 fw-normal">Fill data to create new reservoir</h1>

    //     <div className="form-floating">
    //       <input
    //         type="text"
    //         className="form-control"
    //         onChange={(e) => setName(e.target.value)}
    //       />
    //       <label>Reservoir name</label>
    //     </div>

    //     <div className="form-floating">
    //       <input
    //         type="text"
    //         className="form-control"
    //         onChange={(e) => setType(e.target.value)}
    //       />
    //       <label>Type</label>
    //     </div>

    //     <div className="form-floating">
    //       <textarea
    //         className="form-control"
    //         rows="100"
    //         onChange={(e) => setDescription(e.target.value)}
    //       />
    //       <label>Description</label>
    //     </div>

    //     <div className="form-floating">
    //       <input
    //         type="text"
    //         step="any"
    //         className="form-control"
    //         onChange={(e) => setLatitude(e.target.value)}
    //       />
    //       <label>Latitude</label>
    //     </div>

    //     <div className="form-floating">
    //       <input
    //         type="text"
    //         step="any"
    //         className="form-control"
    //         onChange={(e) => setLongitude(e.target.value)}
    //       />
    //       <label>Longitude</label>
    //     </div>

    //     <div className="form-floating">
    //       <input
    //         type="text"
    //         className="form-control"
    //         onChange={(e) => setCityId(e.target.value)}
    //       />
    //       <label>City ID</label>
    //     </div>

    //     <div className="form-floating">
    //       <input
    //         multiple
    //         type="file"
    //         className="form-control"
    //         onChange={saveFile}
    //       />
    //       <label>Image/s</label>
    //     </div>

    //     <button className="w-100 btn btn-lg btn-primary" type="submit">
    //       Submit
    //     </button>
    //     <p className="mt-5 mb-3 text-muted">&copy; 2017–2021</p>
    //   </form>
    // </main>
  );
};

export default CreateReservoir;
