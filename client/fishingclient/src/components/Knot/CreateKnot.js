import React, { Fragment, useState } from "react";
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
      const newKnot = {
       name,type,description
      };

      fetch('https://localhost:44366/api/Knots/create', {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify(newKnot),
        })
        .catch((error) => {
          console.error('Error:', error);
        });
      }

  return (
    <Fragment >
      <div className="d-flex justify-content-center">
      
      <h1 className='large text-primary'>Create Knot</h1>
      </div>
      <div className="d-flex justify-content-center">
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
        <br/>
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
        <br/>
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
        <br/>
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

export default CreateKnot;