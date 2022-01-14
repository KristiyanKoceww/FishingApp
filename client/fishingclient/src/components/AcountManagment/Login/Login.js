import React, { useContext, useState } from "react";
import { Link, Redirect } from "react-router-dom";

import "./Login.css";
import Footer from "../../Footer/Footer";
import { UserContext } from "../UserContext";

import Button from "@mui/material/Button";
import PublicIcon from "@mui/icons-material/Public";
import { TextField } from "@mui/material";
import InputAdornment from "@mui/material/InputAdornment";
import TitleIcon from "@mui/icons-material/Title";
import PasswordIcon from "@mui/icons-material/Password";
import ErrorNotification from "../../ErrorsManagment/ErrorNotification";
const Login = () => {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [redirect, setRedirect] = useState(false);
  const [error, setError] = useState();
  const { appUser, setAppUser } = useContext(UserContext);
  const loginUrl = process.env.REACT_APP_LOGIN;
  const submit = async (e) => {
    e.preventDefault();

    const user = {
      username,
      password,
    };

    await fetch(loginUrl, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      credentials: "include",
      body: JSON.stringify(user),
    })
      .then((response) => {
        if (!response.ok) {
          throw new Error("Login failed! Try again.");
        }
        return response.json();
      })
      .then((res) => {
        if (res.accessToken) {
          localStorage.setItem("jwt", res.accessToken);
          localStorage.setItem("refresh", res.refreshToken);
          localStorage.setItem("userId", res.userId);
          setRedirect(true);
        }
        setAppUser(res.user);
        console.log(res.user);
      })
      .catch((err) => setError(err.message))
      .finally(setError(null));
  };

  if (redirect) {
    return <Redirect to="/" />;
  }
  return (
    <div className="Login">
      {error ? (
        <div>
          {" "}
          <ErrorNotification message={error} />
        </div>
      ) : (
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
              type="password"
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
          <hr />
          <div className="row">
            <Link className="col" to="/register">
              <div>Forgotten password?</div>
            </Link>
            <Link className="col-2" to="/register">
              <div>Register</div>
            </Link>
          </div>
        </form>
      )}

      <Footer />
    </div>
  );
};
export default Login;
