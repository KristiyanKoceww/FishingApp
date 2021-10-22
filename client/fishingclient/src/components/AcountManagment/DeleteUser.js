import React, { useState } from "react";
import { Redirect } from "react-router-dom";
import './Login.css'

const DeleteUser = () => {

const [userId,setUserId] = useState('');
const [redirect,setRedirect] = useState(false);

const submit = async (e) =>{

    e.preventDefault();
 
       try {
        fetch('https://localhost:44343/api/AppUsers/delete?userId='+userId, {
            method: 'POST',
            headers: {
              'Content-Type': 'application/json',
            },
            body: JSON.stringify(userId),
          })
       } catch (error) {
        console.error(error);
       }
         setRedirect(true);
}
if (redirect) {
    return <Redirect to="/"/>
}
    return (
        <main className="form-signin">
            <form onSubmit={submit}>
                <h1 className="h3 mb-3 fw-normal">Please Delete User</h1>

                <div className="form-floating">
                    <input type="text" className="form-control"  onChange={e => setUserId(e.target.value)} />
                    <label for="floatingInput">User id</label>
                </div>
                
                <button className="w-100 btn btn-lg btn-primary" type="submit">DeleteUser</button>
            </form>
        </main>
    )
}

export default DeleteUser;