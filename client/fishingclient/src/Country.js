import React, { Fragment, useState } from "react";
import { Link } from "react-router-dom";
import axios from "axios";

const CreateCountry = () => {
  const [formData, setFormData] = useState({
    name: "",
  });

  const { name } = formData;

  const onChange = (e) =>
    setFormData({ ...formData, [e.target.name]: e.target.value });

  const onSubmit = async (e) => {
    e.preventDefault();
      const newCountry = {
        name
      };
      try {
        const config = {
          headers: {
            "Content-Type": "application/json",
          },
        };
        const body = JSON.stringify(newCountry);
        const res = await axios.post("https://localhost:44366/api/Countries/create", body, config);
        console.log(res.data);
      } catch (err) {
        console.error(err.response.data);
      }
    }
    
  
  return (
    <Fragment>
      <h1 className='large text-primary'>Country</h1>
      <p className='lead'>
        <i className='fas fa-user'></i> Create Country
      </p>
      
      <form className='form' onSubmit={(e) => onSubmit(e)}>
      <div className='form-group'>
          <input
            onChange={onChange}
            type='text'
            placeholder='Country name'
            name='name'
            value={name}
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
}

export default CreateCountry