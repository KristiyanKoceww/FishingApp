import React, { Fragment, useState } from "react";
import { Link } from "react-router-dom";
import axios from "axios";

const Register = () => {
  const [formData, setFormData] = useState({
    firstName: "",
    middleName: "",
    lastName: "",
    age: "",
    phone: "",
    gender: "",
    imageUrl: "",
    email: "",
    username: "",
    password: "",
    password2: "",
  });

  const { firstName,middleName,lastName,age,phone,gender,imageUrl,username, email, password, password2 } = formData;

  const onChange = (e) =>
    setFormData({ ...formData, [e.target.name]: e.target.value });

  const onSubmit = async (e) => {
    e.preventDefault();
    if (password !== password2) {
      console.log("Passwords do not match");
    } else {
      // console.log(formData);
      const newUser = {
        firstName,
        middleName,
        lastName,
        age,
        phone,
        gender,
        imageUrl,
        username,
        email,
        password,
      };

      fetch('https://localhost:44366/api/AppUsers/create', {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify(newUser),
        })
        .catch((error) => {
          console.error('Error:', error);
        });
      }
  };


  return (
    <Fragment>
      <h1 className='large text-primary'>Sign Up</h1>
      <p className='lead'>
        <i className='fas fa-user'></i> Create Your Account
      </p>
      
      <form className='form' onSubmit={(e) => onSubmit(e)}>
      <div className='form-group'>
          <input
            onChange={onChange}
            type='text'
            placeholder='Username'
            name='username'
            value={username}
            required
          />
        </div>
        <div className='form-group'>
          <input
            onChange={onChange}
            type='text'
            placeholder='First name'
            name='firstName'
            value={firstName}
            required
          />
        </div>
        <div className='form-group'>
          <input
            onChange={onChange}
            type='text'
            placeholder='Middle Name'
            name='middleName'
            value={middleName}
            required
          />
        </div>
        <div className='form-group'>
          <input
            onChange={onChange}
            type='text'
            placeholder='Last Name'
            name='lastName'
            value={lastName}
            required
          />
        </div>
        <div className='form-group'>
          <input
            onChange={onChange}
            type='text'
            placeholder='Phone'
            name='phone'
            value={phone}
            required
          />
        </div>
        <div className='form-group'>
          <input
            onChange={onChange}
            type='text'
            placeholder='Age'
            name='age'
            value={age}
            required
          />
        </div>
        <div className='form-group'>
          <input
            onChange={onChange}
            type='text'
            placeholder='Gender'
            name='gender'
            value={gender}
            required
          />
        </div>
        <div className='form-group'>
          <input
            onChange={onChange}
            type='text'
            placeholder='image url'
            name='imageUrl'
            value={imageUrl}
           
          />
        </div>
        <div className='form-group'>
          <input
            onChange={onChange}
            type='email'
            placeholder='Email Address'
            name='email'
            value={email}
          />
        </div>
        <div className='form-group'>
          <input
            onChange={onChange}
            type='password'
            placeholder='Password'
            name='password'
            minLength='6'
            value={password}
          />
        </div>
        <div className='form-group'>
          <input
            onChange={onChange}
            type='password'
            placeholder='Confirm Password'
            name='password2'
            minLength='6'
            value={password2}
          />
        </div>
        <input
          onChange={onChange}
          type='submit'
          className='btn btn-primary'
          value='Register'
        />
      </form>
      <p className='my-1'>
        Already have an account? <Link to='/login'>Sign In</Link>
      </p>
    </Fragment>
  );
};

export default Register;