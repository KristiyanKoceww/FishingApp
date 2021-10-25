import React, { useState } from "react";
import { Redirect } from "react-router-dom";
import './Login.css'

const Register = () => {
const [firstName,setFirstName] = useState('');
const [middleName,setMiddleName] = useState('');
const [lastName,setLastName] = useState('');
const [age,setAge] = useState(18);
const [phoneNumber,setPhoneNumber] = useState('');
const [gender,setGender] = useState(1);
const [image,setImage] = useState('https://t3.ftcdn.net/jpg/03/46/83/96/360_F_346839683_6nAPzbhpSkIpb8pmAwufkC7c5eD7wYws.jpg');
const [email,setEmail] = useState('');
const [username,setUsername] = useState('');
const [password,setPassword] = useState('');

const [redirect,setRedirect] = useState(false);

const submit = async (e) =>{

    e.preventDefault();

    const newUser = {
        firstName,
        middleName,
        lastName,
        age,
        phoneNumber,
        gender,
        image,
        email,
        username,
        password,
       };
 
       try {
        fetch('https://localhost:44366/api/AppUsers/register', {
            method: 'POST',
            headers: {
              'Content-Type': 'application/json',
            },
            body: JSON.stringify(newUser),
          })
       } catch (error) {
        console.error(error);
       }
         setRedirect(true);
}
if (redirect) {
    return <Redirect to="/Login"/>
}
    return (
        <main className="form-signin">
            <form onSubmit={submit}>
                <h1 className="h3 mb-3 fw-normal">Please register</h1>

                <div className="form-floating">
                    <input type="text" className="form-control"  onChange={e => setFirstName(e.target.value)} />
                    <label for="floatingInput">First Name</label>
                </div>

                <div className="form-floating">
                    <input type="text" className="form-control" onChange={e => setMiddleName(e.target.value)} />
                    <label for="floatingInput">Middle Name</label>
                </div>

                <div className="form-floating">
                    <input type="text" className="form-control" onChange={e => setLastName(e.target.value)} />
                    <label for="floatingInput">Last Name</label>
                </div>

                <div className="form-floating">
                    <input type="number" className="form-control" onChange={e => setAge(e.target.value)} />
                    <label for="floatingInput">Age</label>
                </div>

                <div className="form-floating">
                    <input type="phone" className="form-control" onChange={e => setPhoneNumber(e.target.value)} />
                    <label for="floatingInput">PhoneNumber</label>
                </div>

                <div className="form-floating">
                    <input type="text" className="form-control" onChange={e => setGender(e.target.value)} />
                    <label for="floatingInput">Gender</label>
                </div>

                <div className="form-floating">
                    <input type="text" className="form-control" onChange={e => setImage(e.target.value)} />
                    <label for="floatingInput">Image</label>
                </div>

                <div className="form-floating">
                    <input type="email" className="form-control" onChange={e => setEmail(e.target.value)} />
                    <label for="floatingInput">Email</label>
                </div>

                <div className="form-floating">
                    <input type="text" className="form-control" onChange={e => setUsername(e.target.value)} />
                    <label for="floatingInput">Username</label>
                </div>

                <div className="form-floating">
                    <input type="password" className="form-control" onChange={e => setPassword(e.target.value)}  />
                    <label for="floatingPassword">Password</label>
                </div>
                <div className="checkbox mb-3">
                    <label>
                        <input type="checkbox" value="remember-me" /> Remember me
                    </label>
                </div>
                <button className="w-100 btn btn-lg btn-primary" type="submit">Register</button>
                <p className="mt-5 mb-3 text-muted">&copy; 2017–2021</p>
            </form>
        </main>
    )
}

export default Register;