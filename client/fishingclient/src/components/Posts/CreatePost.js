import React, { useState } from "react";
import { TextField } from "@mui/material";
import Button from "@mui/material/Button";
import InputAdornment from "@mui/material/InputAdornment";
import TitleIcon from "@mui/icons-material/Title";
import TextsmsIcon from "@mui/icons-material/Textsms";
import ConnectWithoutContactIcon from "@mui/icons-material/ConnectWithoutContact";
import "./CreatePost.css";

const CreatePost = (props) => {
  const [title, setTitle] = useState("");
  const [content, setContent] = useState("");
  const [FormFiles, setFormFiles] = useState([]);

  const jwt = localStorage.getItem("jwt");
  const userId = localStorage.getItem("userId");

  const saveFile = (e) => {
    for (let index = 0; index < e.target.files.length; index++) {
      const element = e.target.files[index];
      setFormFiles((FormFiles) => [...FormFiles, element]);
    }
  };

  const uploadImage = async (e) => {
    e.preventDefault();

    const formData = new FormData();
    for (let index = 0; index < FormFiles.length; index++) {
      const element = FormFiles[index];
      formData.append("FormFiles", element);
    }

    formData.append("title", title);
    formData.append("userId", userId);
    formData.append("content", content);

    fetch("https://localhost:44366/api/Posts/create", {
      method: "POST",
      headers: { Authorization: "Bearer " + jwt },
      body: formData,
    })
      .then((r) => r.json())
      .then((result) => {
        props.onCreate(result);
      });
  };

  const handleSubmit = async (e) => {
    await uploadImage(e);
    setContent("");
    setTitle("");
    setFormFiles([]);
  };

  return (
    <div className="createPost">
      <form onSubmit={(e) => handleSubmit(e)}>
        <h1 className="title__post">
          <ConnectWithoutContactIcon /> Share your thoughts and moments with
          friends
        </h1>

        <div>
          <TextField
            className="textFieldTitle"
            label="Title"
            variant="filled"
            size="large"
            fullWidth
            onChange={(e) => setTitle(e.target.value)}
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
            label="What are your thoughts?"
            id="filled-hidden-label-small"
            variant="filled"
            size="large"
            fullWidth
            onChange={(e) => setContent(e.target.value)}
            required
            multiline
            InputProps={{
              startAdornment: (
                <InputAdornment position="start">
                  <TextsmsIcon />
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
  );
};

export default CreatePost;
