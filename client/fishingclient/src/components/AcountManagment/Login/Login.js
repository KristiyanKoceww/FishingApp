import React, { useContext, useState } from "react";
import { Redirect } from "react-router-dom";

import "./Login.css";
import { UserContext } from "../UserContext";

import Button from "@mui/material/Button";
import PublicIcon from "@mui/icons-material/Public";
import { TextField } from "@mui/material";
import InputAdornment from "@mui/material/InputAdornment";
import TitleIcon from "@mui/icons-material/Title";
import PasswordIcon from '@mui/icons-material/Password';

const Login = () => {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [redirect, setRedirect] = useState(false);

  const { appUser, setAppUser } = useContext(UserContext);

  const submit = async (e) => {
    e.preventDefault();

    const user = {
      username,
      password,
    };

    await fetch("https://localhost:44366/api/AppUsers/login", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      credentials: "include",
      body: JSON.stringify(user),
    })
      .then((response) => response.json())
      .then((res) => {
        if (res.AccessToken) {
          localStorage.setItem("jwt", res.AccessToken);
          setRedirect(true);
        }

        // remove this
        localStorage.setItem("userId", res.UserId);
        setAppUser(res.User);
      });
  };

  if (redirect) {
    return <Redirect to="/" />;
  }
  return (
    <div className="Login">
      <form onSubmit={submit}>
        <h1 className="title__login">
          {" "}
          <PublicIcon /> Please , fill your data to login.
        </h1>
        <div>
          <TextField
            className="textFieldTitle"
            label="Username"
            variant="filled"
            size="large"
            fullWidth
            onChange={(e) => setUsername(e.target.value)}
            required
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
            className="textFieldTitle"
            label="Password"
            variant="filled"
            size="large"
            fullWidth
            onChange={(e) => setPassword(e.target.value)}
            required
            InputProps={{
              startAdornment: (
                <InputAdornment position="start">
                  <PasswordIcon />
                </InputAdornment>
              ),
            }}
          />
        </div>
        <br />

        <div className="LoginButtonDiv">
          <Button className="LoginButton" type="submit" variant="outlined">
            Login
          </Button>
        </div>
      </form>
    </div>
  );
};
export default Login;