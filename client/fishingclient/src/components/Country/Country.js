import React, { useState, useContext } from "react";
import PublicIcon from "@mui/icons-material/Public";
import { TextField } from "@mui/material";
import InputAdornment from "@mui/material/InputAdornment";
import TitleIcon from "@mui/icons-material/Title";
import "./Country.css";
import Footer from '../Footer/Footer'

import Button from "@mui/material/Button";
import ErrorNotification from '../ErrorsManagment/ErrorNotification'

const CreateCountry = () => {
  const [name, setName] = useState("");
  const [error, setError] = useState();
  const [errorText,setErrorText] = useState();
  const jwt = localStorage.getItem("jwt");
  const createCountryUrl = process.env.REACT_APP_CREATECOUNTRY;
  const onSubmit = async (e) => {
    e.preventDefault();
    let newCountry = {
      Name: name,
    };
    fetch(createCountryUrl, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + jwt,
      },
      body: JSON.stringify(newCountry),
    }).then(res => {
      const data = res.json();
      data.then(r=> setErrorText(r.errors.name[0]));
      if (!res.ok) {
        throw new Error(errorText)
      }
      return res.json();
    }).catch((error) => {
      setError(error.message);
    });
  };

  return (
    <div className="createCountry">
      {error ? <div> <ErrorNotification message={error} /></div> :
        <form onSubmit={onSubmit}>
          <h1 className="title__country">
            {" "}
            <PublicIcon /> Please enter name of country
          </h1>
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
          <Button className="createCountryButton" type="submit" variant="outlined">
            Create
          </Button>
        </form>
      }
      <Footer />
    </div>
  );
};

export default CreateCountry;
