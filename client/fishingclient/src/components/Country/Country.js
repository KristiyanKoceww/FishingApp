import React, { Fragment, useState } from "react";

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
     
        fetch('https://localhost:44366/api/Countries/create', {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify(newCountry),
        })
        .catch((error) => {
          console.error('Error:', error);
        });
      }
  
  return (
    
    <Fragment>
    <div className="d-flex justify-content-center">
      <h1 className='large text-primary'>Create new country</h1>
      </div>
      <div className="d-flex justify-content-center">
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
      </div>
    </Fragment>
    
  );
}

export default CreateCountry