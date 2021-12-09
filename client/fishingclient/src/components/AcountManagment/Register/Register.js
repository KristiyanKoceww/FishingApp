import React, { useState } from "react";
import { Redirect } from "react-router-dom";
import "./Register.css";

import { TextField } from "@mui/material";
import Button from "@mui/material/Button";
import InputAdornment from "@mui/material/InputAdornment";
import TitleIcon from "@mui/icons-material/Title";
import ShortTextIcon from "@mui/icons-material/ShortText";
import ConnectWithoutContactIcon from "@mui/icons-material/ConnectWithoutContact";
import DescriptionIcon from "@mui/icons-material/Description";
import IconButton from "@mui/material/IconButton";
import OutlinedInput from "@mui/material/OutlinedInput";
import InputLabel from "@mui/material/InputLabel";
import Visibility from "@mui/icons-material/Visibility";
import VisibilityOff from "@mui/icons-material/VisibilityOff";
import Radio from "@mui/material/Radio";
import RadioGroup from "@mui/material/RadioGroup";
import FormControlLabel from "@mui/material/FormControlLabel";
import FormControl from "@mui/material/FormControl";
import FormLabel from "@mui/material/FormLabel";
import MenuItem from "@mui/material/MenuItem";
import Select from "@mui/material/Select";

const Register = () => {
  const [firstName, setFirstName] = useState("");
  const [middleName, setMiddleName] = useState("");
  const [lastName, setLastName] = useState("");
  const [age, setAge] = useState(18);
  const [phoneNumber, setPhoneNumber] = useState("");
  const [gender, setGender] = useState(1);
  const [email, setEmail] = useState("");
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [showPassword, setshowPassword] = useState(false);
  const [redirect, setRedirect] = useState(false);
  const [MainImage, setMainImage] = useState();

  const saveFile = (e) => {
    setMainImage(e.target.files[0]);
  };

  const handleChange = (e) => {
    setGender(e.target.value);
  };

  const handleClickShowPassword = () => {
    setshowPassword(!showPassword);
  };

  const handleMouseDownPassword = (event) => {
    event.preventDefault();
  };

  const submit = async (e) => {
    e.preventDefault();

    const formData = new FormData();

    formData.append("MainImage", MainImage);
    formData.append("FirstName", firstName);
    formData.append("MiddleName", middleName);
    formData.append("LastName", lastName);
    formData.append("Age", age);
    formData.append("PhoneNumber", phoneNumber);
    formData.append("Gender", gender);
    formData.append("Email", email);
    formData.append("UserName", username);
    formData.append("Password", password);

    try {
      await fetch("https://localhost:44366/api/AppUsers/register", {
        method: "POST",
        body: formData,
      });
    } catch (error) {
      console.error(error);
    }
    setRedirect(true);
  };
  if (redirect) {
    return <Redirect to="/Login" />;
  }
  return (
    <div className="Register">
      <form onSubmit={(e) => submit(e)}>
        <h1 className="title__register">
          <ConnectWithoutContactIcon /> Please , fill your details to register.
        </h1>
        <div>
          <TextField
            className="textFieldTitle"
            label="First name"
            variant="filled"
            size="large"
            fullWidth
            onChange={(e) => setFirstName(e.target.value)}
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
            className="textFieldTitle"
            label="Middle name"
            variant="filled"
            size="large"
            fullWidth
            onChange={(e) => setMiddleName(e.target.value)}
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
            label="Last name"
            id="filled-hidden-label-small"
            variant="filled"
            size="large"
            fullWidth
            onChange={(e) => setLastName(e.target.value)}
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
            typ="number"
            label="Age"
            id="filled-hidden-label-small"
            variant="filled"
            size="large"
            fullWidth
            onChange={(e) => setAge(e.target.value)}
            required
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
          <FormControl fullWidth>
            <InputLabel id="demo-simple-select-label">Gender</InputLabel>
            <Select
              required
              labelId="demo-simple-select-label"
              id="demo-simple-select"
              value={gender}
              label="Gender"
              onChange={handleChange}
            >
              <MenuItem value="Male">Male</MenuItem>
              <MenuItem value="Female">Female</MenuItem>
            </Select>
          </FormControl>
        </div>
        <br />
        <div>
          <TextField
            label="Phone number"
            id="filled-hidden-label-small"
            variant="filled"
            size="large"
            fullWidth
            onChange={(e) => setPhoneNumber(e.target.value)}
            required
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
            label="Email"
            id="filled-hidden-label-small"
            variant="filled"
            size="large"
            fullWidth
            onChange={(e) => setEmail(e.target.value)}
            required
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
            className="username"
            label="Username"
            id="filled-hidden-label-small"
            variant="filled"
            size="large"
            fullWidth
            onChange={(e) => setUsername(e.target.value)}
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

          <FormControl className="password" variant="filled">
            <InputLabel htmlFor="outlined-adornment-password">
              Password
            </InputLabel>
            <OutlinedInput
              className="passwordInput"
              id="filled-hidden-label-small"
              type={showPassword ? "text" : "password"}
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              endAdornment={
                <InputAdornment position="end">
                  <IconButton
                    aria-label="toggle password visibility"
                    onClick={handleClickShowPassword}
                    onMouseDown={handleMouseDownPassword}
                    edge="end"
                  >
                    {showPassword ? <VisibilityOff /> : <Visibility />}
                  </IconButton>
                </InputAdornment>
              }
              label="Password"
            />
          </FormControl>
        </div>
        <br />
        <div className="UploadProfilePicture">
          <Button
            className="ProfilePictureButton"
            variant="contained"
            component="label"
          >
            Upload profile picture
            <input type="file" hidden onChange={saveFile} />
          </Button>
        </div>
        <br />
        <div className="submit">
          <Button className="submit__button" type="submit" variant="outlined">
            Register
          </Button>
        </div>
        <br />
      </form>
    </div>
  );
};

export default Register;
