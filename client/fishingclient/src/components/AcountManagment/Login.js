import './Login.css'
import React, { useState } from "react";
import { Redirect } from "react-router-dom";

const Login = () => {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [redirect, setRedirect] = useState(false);

    const submit = async (e) => {
        e.preventDefault();

        const newUser = {
            username,
            password,
        };

       await fetch('https://localhost:44366/api/AppUsers/login', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            credentials: 'include',
            body: JSON.stringify(newUser),
        })

        setRedirect(true);
    }
    if (redirect) {
        return <Redirect to="/"/>
    }
    return (
        <main className="form-signin">
            <form onSubmit={submit}>
                <h1 className="h3 mb-3 fw-normal">Please sign in</h1>

                <div className="form-floating">
                    <input type="text" className="form-control" placeholder="name@example.com" onChange={e => setUsername(e.target.value)} />
                    <label for="floatingInput">Username</label>
                </div>
                <div className="form-floating">
                    <input type="password" className="form-control" placeholder="Password" onChange={e => setPassword(e.target.value)} />
                    <label for="floatingPassword">Password</label>
                </div>
                <div className="checkbox mb-3">
                    <label>
                        <input type="checkbox" value="remember-me" /> Remember me
                    </label>
                </div>
                <button className="w-100 btn btn-lg btn-primary" type="submit">Sign in</button>
                <p className="mt-5 mb-3 text-muted">&copy; 2017–2021</p>
            </form>
        </main>
    )
}

export default Login;