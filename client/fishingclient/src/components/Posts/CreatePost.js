import React, { useState, useEffect } from "react";
import { Redirect } from "react-router-dom";
import '../AcountManagment/Login.css'


const CreatePost = (props) => {
const [title,setTitle] = useState('');
const [content,setContent] = useState('');
const [image,setImage] = useState('');
const [redirect,setRedirect] = useState(false);


const [userId,setUserId] = useState('');

useEffect(() => {
  (async () => {
    const response = await fetch('https://localhost:44343/api/AppUsers/user',
      {
        headers: { 'Content-Type': 'application/json' },
        credentials: 'include',
      });

    const content = await response.json();
    console.log(content);
    setUserId(content.userId);
  })()
});



const submit = async (e) =>{
    setUserId(props.userId);
    e.preventDefault();

    const newPost = {
       title,
       content,
       image,
       userId,
       };
 
       try {
        fetch('https://localhost:44343/api/Posts/create', {
            method: 'POST',
            headers: {
              'Content-Type': 'application/json',
            },
            body: JSON.stringify(newPost),
          })
       } catch (error) {
        console.error(error);
       }
         setRedirect(true);
}
// if (redirect) {
//     return <Redirect to="/Posts"/>
// }
    return (
        <main className="form-signin">
            <form onSubmit={submit}>
                <h1 className="h3 mb-3 fw-normal">Share your thoughts and moments with friends</h1>

                <div className="form-floating">
                    <input type="text" className="form-control"  onChange={e => setTitle(e.target.value)} />
                    <label for="floatingInput">Post title</label>
                </div>

                <div className="form-floating">
                    <input type="text" className="form-control" onChange={e => setContent(e.target.value)} />
                    <label for="floatingInput">Content</label>
                </div>

                {/* <div className="form-floating">
                    <input type="file" multiple className="form-control" onChange={e => setImage(e.target.value)} />
                </div> */}

<div className="form-floating">
                    <input type="text" className="form-control" onChange={e => setImage(e.target.value)} />
                    <label for="floatingInput">Image link</label>
                </div>
                
                <button className="w-100 btn btn-lg btn-primary" type="submit">Post</button>
                <p className="mt-5 mb-3 text-muted">&copy; 2017–2021</p>
            </form>
        </main>
    )
}

export default CreatePost;