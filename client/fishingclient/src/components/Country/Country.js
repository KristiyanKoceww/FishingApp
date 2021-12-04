import React, { useState } from "react";
import PublicIcon from '@mui/icons-material/Public';
import { TextField } from "@mui/material";
import InputAdornment from "@mui/material/InputAdornment";
import TitleIcon from "@mui/icons-material/Title";
import './Country.css'

import { Button } from "primereact/button";
import "primereact/resources/themes/lara-light-indigo/theme.css";
import "primereact/resources/primereact.min.css";
import "primeicons/primeicons.css";

const CreateCountry = () => {
  const [name, setName] = useState("");
  const jwt = localStorage.getItem("jwt");

  const onSubmit = async (e) => {
    e.preventDefault();
    let newCountry = {
      Name: name
    }
    fetch("https://localhost:44366/api/Countries/create", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + jwt,
      },
      body: JSON.stringify(newCountry),
    })
    .catch((error) => {
      console.error("Error:", error);
    });
  };

  return (
    <div className="createCountry">
      <form onSubmit={onSubmit}>
        <h1 className="title__country"> <PublicIcon /> {" "} Please enter name of country</h1>

        <div>
          <TextField
            className="textFieldTitle"
            label="Country name"
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
        {/* <Button className="createCountryButton" type="submit" variant="outlined">
              Submit
            </Button> */}

            <Button
          className="createCountryButton"
          icon="pi pi-plus"
          label="Create post"
          icon="pi pi-check"
          iconPos="left"
          label="Submit"
          type="submit"
        />
      </form>
    </div>
  );
};

export default CreateCountry;
