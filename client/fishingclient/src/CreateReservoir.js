import { render } from '@testing-library/react';

import React, { useState } from 'react';

export function CreateReservoir(){

    const[name, setName] = useState('');
    const[type, setType] = useState('');
    const[description, setDescription] = useState('');
    const[latitude, setLatitude] = useState('');
    const[longitude, setLongitude] = useState('');


   const changeName = e =>{
        setName(e.target.value);
    }
   const changeType = e =>{
        setType(e.target.value);
    }
    const changeDescription = e =>{
        setDescription(e.target.value);
    }
    const changeLatitude = e =>{
        setLatitude(e.target.value);
    }
    const changeLongitude = e =>{
        setLongitude(e.target.value);
    }

    function handleSubmit () {

        fetch('https://localhost:44366/api/Reservoir/create',
        {
            method: "POST",
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
              },
            body: JSON.stringify({
                Name:name,
                Type:type,
                Description:description,
                Latitude:latitude,
                Longitude:longitude
            })
        })
        .then(r => r.json())
        .then(res => {
            alert(res)
            alert('Mission succsess');
            
        },(error)=>{
            alert('Request failed')
        })
    };
 
        return(
            <div>
            <div>
                <h1>Create form</h1>
            </div>
            <form onSubmit={handleSubmit}>
            <div>
             <label htmlFor="name"> Name: <input type="text" value={name} onChange={changeName} /> </label>
             </div>
             <div>
             <label htmlFor="type"> Type: <input type="text" value={type} onChange={changeType} /> </label>
             </div>
             <div>
             <label htmlFor="description"> Description: <input type="text" value={description} onChange={changeDescription} /> </label>
             </div>
             <div>
             <label htmlFor="latitude"> Latitute: <input type="number" value={latitude} onChange={changeLatitude} /> </label>
             </div>
             <div>
             <label htmlFor="longitude"> Longitude: <input type="number" value={longitude} onChange={changeLongitude} /> </label>
             </div>
            
            <input type="submit" value="Submit" />
                </form>
                </div>
        )
}
