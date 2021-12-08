import React, { useState } from "react";
import { Redirect } from "react-router-dom";
import "./Login.css";

import { TextField } from "@mui/material";
import { Field } from "@mui/material";
import Button from "@mui/material/Button";
import InputAdornment from "@mui/material/InputAdornment";
import TitleIcon from "@mui/icons-material/Title";
import ShortTextIcon from "@mui/icons-material/ShortText";
import ConnectWithoutContactIcon from "@mui/icons-material/ConnectWithoutContact";
import DescriptionIcon from "@mui/icons-material/Description";

import Radio from "@mui/material/Radio";
import RadioGroup from "@mui/material/RadioGroup";
import FormControlLabel from "@mui/material/FormControlLabel";
import FormControl from "@mui/material/FormControl";
import FormLabel from "@mui/material/FormLabel";



import Box from '@mui/material/Box';
import IconButton from '@mui/material/IconButton';
import Input from '@mui/material/Input';
import FilledInput from '@mui/material/FilledInput';
import OutlinedInput from '@mui/material/OutlinedInput';
import InputLabel from '@mui/material/InputLabel';
import FormHelperText from '@mui/material/FormHelperText';
import Visibility from '@mui/icons-material/Visibility';
import VisibilityOff from '@mui/icons-material/VisibilityOff';

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
     setshowPassword(!showPassword)
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
      fetch("https://localhost:44366/api/AppUsers/register", {
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
    <div className="CreateKnot">
      <form onSubmit={(e) => submit(e)}>
        <h1 className="KnotTittle">
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

        <FormControl component="fieldset">
          <FormLabel component="legend">Gender</FormLabel>
          <RadioGroup
            aria-label="gender"
            name="controlled-radio-buttons-group"
            value={gender}
            onChange={handleChange}
          >
            <FormControlLabel
              value="female"
              control={<Radio />}
              label="Female"
            />
            <FormControlLabel value="male" control={<Radio />} label="Male" />
          </RadioGroup>
        </FormControl>

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
        </div>
        <br />


        <FormControl  variant="filled">
          <InputLabel htmlFor="outlined-adornment-password">Password</InputLabel>
          <OutlinedInput
            id="filled-hidden-label-small"
            type={showPassword ? 'text' : 'password'}
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
        {/* <div>
          <TextField
            label="Password"
            id="filled-hidden-label-small"
            variant="filled"
            size="large"
            fullWidth
            onChange={(e) => setPassword(e.target.value)}
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
        <br /> */}

        <div className="choose__files">
          <Button className="submit__button" type="submit" variant="outlined">
            Submit
          </Button>
        </div>
        <br />
      </form>
    </div>

    // <main className="form-signin">
    //     <form onSubmit={submit}>
    //         <h1 className="h3 mb-3 fw-normal">Please fill in your data to register. </h1>
    //         <h5>Fields with * are required.</h5>

    //         <div className="form-floating">
    //             <input required type="text" className="form-control" onChange={e => setFirstName(e.target.value)} />
    //             <label for="floatingInput">* First Name</label>
    //         </div>

    //         <div className="form-floating">
    //             <input type="text" className="form-control" onChange={e => setMiddleName(e.target.value)} />
    //             <label for="floatingInput">Middle Name</label>
    //         </div>

    //         <div className="form-floating">
    //             <input required type="text" className="form-control" onChange={e => setLastName(e.target.value)} />
    //             <label for="floatingInput">*Last Name</label>
    //         </div>

    //         <div className="form-floating">
    //             <input required type="number" className="form-control" onChange={e => setAge(e.target.value)} />
    //             <label for="floatingInput">* Age</label>
    //         </div>

    //         <div className="form-floating">
    //             <input type="phone" className="form-control" onChange={e => setPhoneNumber(e.target.value)} />
    //             <label for="floatingInput">PhoneNumber</label>
    //         </div>

    //         {/* <div className="form-floating">
    //             <input required type="text" className="form-control" onChange={e => setGender(e.target.value)} />
    //             <label for="floatingInput">* Gender</label>
    //         </div> */}

    //         <div className="form-floating">
    //             <select className="form-control" onChange={e => setGender(e.target.value)} >
    //                 <option value="1">Male</option>
    //                 <option value="2">Female</option>
    //             </select>
    //         </div>

    //         <div className="form-floating">
    //             <input type="file" className="form-control" onChange={saveFile} />
    //             <label for="floatingInput">Profile picture</label>
    //         </div>

    //         <div className="form-floating">
    //             <input required type="email" className="form-control" onChange={e => setEmail(e.target.value)} />
    //             <label for="floatingInput">* Email</label>
    //         </div>

    //         <div className="form-floating">
    //             <input required type="text" className="form-control" onChange={e => setUsername(e.target.value)} />
    //             <label for="floatingInput">* Username</label>
    //         </div>

    //         <div className="form-floating">
    //             <input required type="password" className="form-control" onChange={e => setPassword(e.target.value)} />
    //             <label for="floatingPassword">* Password</label>
    //         </div>
    //         <div className="checkbox mb-3">
    //             <label>
    //                 <input type="checkbox" value="remember-me" /> Remember me
    //             </label>
    //         </div>
    //         <button className="w-100 btn btn-lg btn-primary" type="submit">Register</button>
    //         <p className="mt-5 mb-3 text-muted">&copy; 2017–2021</p>
    //     </form>
    // </main>
  );
};

export default Register;
