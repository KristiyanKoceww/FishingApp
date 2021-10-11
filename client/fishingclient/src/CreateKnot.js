import React, { Fragment, useState } from "react";
import { Link } from "react-router-dom";
import axios from "axios";

const CreateKnot = () => {
  const [formData, setFormData] = useState({
    name: "",
    type: "",
    description: "",
  });

  const { name,type,description} = formData;

  const onChange = (e) =>
    setFormData({ ...formData, [e.target.name]: e.target.value });

  const onSubmit = async (e) => {
    e.preventDefault();
    
      // console.log(formData);
      const newKnot = {
       name,type,description
      };
      try {
        const config = {
          headers: {
            "Content-Type": "application/json",
          },
        };
        const body = JSON.stringify(newKnot);
        const res = await axios.post("https://localhost:44366/api/Knots/create", body, config);
        console.log(res.data);
      } catch (err) {
        console.error(err.response.data);
      }
    
    
  };


  return (
    <Fragment>
      <h1 className='large text-primary'>Create Knot</h1>
      <p className='lead'>
        <i className='fas fa-user'></i> Create Your new knot
      </p>
      
      <form className='form' onSubmit={(e) => onSubmit(e)}>
      <div className='form-group'>
          <input
            onChange={onChange}
            type='text'
            placeholder='Name'
            name='name'
            value={name}
            required
          />
        </div>
        <div className='form-group'>
          <input
            onChange={onChange}
            type='text'
            placeholder='Type'
            name='type'
            value={type}
            required
          />
        </div>
        <div className='form-group'>
          <input
            onChange={onChange}
            type='text'
            placeholder='Description'
            name='description'
            value={description}
            required
          />
        </div>
       
        <input
          onChange={onChange}
          type='submit'
          className='btn btn-primary'
          value='Create'
        />
      </form>
     
    </Fragment>
  );
};

export default CreateKnot;