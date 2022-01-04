import React, { useState } from "react";
import { TextField } from "@mui/material";
import Button from "@mui/material/Button";
import InputAdornment from "@mui/material/InputAdornment";
import TitleIcon from "@mui/icons-material/Title";
import ShortTextIcon from '@mui/icons-material/ShortText';
import ConnectWithoutContactIcon from "@mui/icons-material/ConnectWithoutContact";
import DescriptionIcon from '@mui/icons-material/Description';
import Footer from '../Footer/Footer'
import './CreateKnot.css'

const CreateKnot = () => {
  const [name, setName] = useState("");
  const [type, setType] = useState("");
  const [description, setDescription] = useState("");
  const [Images, setImages] = useState([]);

  const saveFile = (e) => {
    for (let index = 0; index < e.target.files.length; index++) {
      const element = e.target.files[index];
      setImages((Images) => [...Images, element]);
    }
  };

  const onSubmit = async (e) => {
    e.preventDefault();

    const formData = new FormData();
    for (let index = 0; index < Images.length; index++) {
      const element = Images[index];
      formData.append("Images", element);
    }

    formData.append("Name", name);
    formData.append("Type", type);
    formData.append("Description", description);
    const jwt = localStorage.getItem("jwt");
    fetch("https://localhost:44366/api/Knots/create", {
      method: "POST",
      headers: { Authorization: "Bearer " + jwt },
      body: formData,
    }).catch((error) => {
      console.error("Error:", error);
    });
  };

  return (
    <div className="CreateKnot">
      <form onSubmit={(e) => onSubmit(e)}>
        <h1 className="KnotTittle">
          <ConnectWithoutContactIcon /> Please , fill the data.
        </h1>

        <div>
          <TextField
            className="textFieldTitle"
            label="Knot name"
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
      <Footer />
    </div>
  );
};

export default CreateKnot;
