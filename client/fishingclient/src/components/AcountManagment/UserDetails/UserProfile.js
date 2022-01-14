import React, { useState, useContext } from "react"
import { UserContext } from '../UserContext';
import './UserProfile.css'
import ErrorNotification from '../../ErrorsManagment/ErrorNotification'
import { TextField } from "@mui/material";
import Button from "@mui/material/Button";
import InputAdornment from "@mui/material/InputAdornment";
import TitleIcon from "@mui/icons-material/Title";
import ShortTextIcon from "@mui/icons-material/ShortText";
import DescriptionIcon from "@mui/icons-material/Description";
const UserProfile = () => {
    const { appUser, setAppUser } = useContext(UserContext);
    const [editOn, setEditOn] = useState(false);
    const [error, setError] = useState(null);
    const [message, setMessage] = useState(null);

    const updateUserInfoUrl = process.env.REACT_APP_UPDATEUSERINFO;
    const jwt = localStorage.getItem('jwt');

    const [firstName, setFirstName] = useState("");
    const [middleName, setMiddleName] = useState("");
    const [lastName, setLastName] = useState("");
    const [age, setAge] = useState();
    const [phoneNumber, setPhoneNumber] = useState("");
    const [email, setEmail] = useState("");

    const submit = async (e) => {
        e.preventDefault();

        const updatedUser = {
            id: appUser.id,
            firstName: firstName,
            middleName: middleName,
            lastName: lastName,
            age: age,
            phoneNumber: phoneNumber,
            email: email
        };

        await fetch(updateUserInfoUrl + appUser.id, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                'Authorization': 'Bearer ' + jwt
            },
            body: JSON.stringify(updatedUser),
        }).then(res => {
            if (!res.ok) {
                setEditOn(false);
                throw new Error('Update failed! Try again.')
            }
            setMessage('Update is successful!');
            setEditOn(false);
            return r.json();
        }).then(r => setAppUser(r.user))
            .catch(err => {
                setError(err.message);
            })

    }
    return (
        <div className="userprofile">
            <div className="usercard">
                <div className="user">
                    <div className="user2" >
                        <div className="user3" >
                            <img src={appUser.mainImageUrl ? appUser.mainImageUrl : 'https://res.cloudinary.com/kocewwcloud/image/upload/v1639749727/FishApp/DefaultProfilePicture/Default_profile_picture_cyg6fr.png'} alt="profile" className="rounded-circle" width="150" />
                            <div className="userInfo">
                                <h4>{appUser.firstName} {appUser.lastName}</h4>
                                <p>{appUser.email}</p>
                                <button className="btn btn-warning">Follow</button> {' '}
                                <button className="btn btn-success">Message</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <hr />
            <div className="row">
                <div className="userDetails">
                    <div className="card-body">
                        <div className="row">
                            <div className="col-sm-3">
                                <h6>First Name</h6>
                            </div>
                            <div className="col-sm-9">
                                {appUser.firstName}
                            </div>
                        </div>
                        <hr />
                        <div className="row">
                            <div className="col-sm-3">
                                <h6>Middle Name</h6>
                            </div>
                            <div className="col-sm-9">
                                {appUser.middleName ? appUser.middleName : 'not inserted'}
                            </div>
                        </div>
                        <hr />
                        <div className="row">
                            <div className="col-sm-3">
                                <h6>Last Name</h6>
                            </div>
                            <div className="col-sm-9">
                                {appUser.lastName}
                            </div>
                        </div>
                        <hr />
                        <div className="row">
                            <div className="col-sm-3">
                                <h6>Age</h6>
                            </div>
                            <div className="col-sm-9">
                                {appUser.age}
                            </div>
                        </div>
                        <hr />
                        <div className="row">
                            <div className="col-sm-3">
                                <h6>Email</h6>
                            </div>
                            <div className="col-sm-9">
                                {appUser.email}
                            </div>
                        </div>
                        <hr />
                        <div className="row">
                            <div className="col-sm-3">
                                <h6>Phone</h6>
                            </div>
                            <div className="col-sm-9">
                                {appUser.phoneNumber}
                            </div>
                        </div>
                        <hr />
                        <div className="row">
                            <div className="col-sm-3">
                                <h6 className="mb-0">Username</h6>
                            </div>
                            <div className="col-sm-9">
                                {appUser.userName}
                            </div>
                        </div>
                        <hr />
                        <div className="row">
                            <button className="btn btn-warning" onClick={(e) => setEditOn(!editOn)}>Edit</button>
                        </div>
                    </div>
                </div>
                <div className="editform" >
                    {error ? <ErrorNotification message={error} /> : null}
                    {message ? <div>{message}</div> : null}
                    {editOn ?
                        <form className="edit" onSubmit={(e) => submit(e)}>
                            <div>
                                <TextField
                                    className="firstnameinput"
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
                                    type="number"
                                    label="Age"
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
                                <TextField
                                    label="Phone number"
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
                            <div>
                                <Button className="submit__button" type="submit" variant="outlined">
                                    Save
                                </Button>
                            </div>
                        </form> : null}
                </div>
            </div>

            
        </div>
    )
}

export default UserProfile