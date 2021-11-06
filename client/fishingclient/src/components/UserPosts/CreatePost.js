import React, { useState, useEffect } from "react";
import { Redirect } from "react-router-dom";
import "../AcountManagment/Login.css";

const CreatePost = (props) => {
  const [title, setTitle] = useState("");
  const [content, setContent] = useState("");
  const [userId, setUserId] = useState();
  const [redirect, setRedirect] = useState(false);

  useEffect(() => {
    const jwt = localStorage.getItem("jwt");
    const fetchUrl = `https://localhost:44366/api/AppUsers/user`;

    const fetchData = () => {
      fetch((fetchUrl),
        {
          method: "POST",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify(jwt),
        })
        // .then((res) => res.json())
          .then((result) => setUserId(result))
          .catch((err) => {
            console.log(err);
          });
    };
    fetchData();
  }, []);

  // useEffect(() => {
  //     ( () => {
  //         const jwt = localStorage.getItem('jwt');
  //          fetch('https://localhost:44366/api/AppUsers/user',
  //             {
  //                 method: "POST",
  //                 headers: { 'Content-Type': 'application/json' },
  //                 body: JSON.stringify(jwt),
  //                 credentials: 'include',
  //             })
  //             .then(res => res.json())
  //             .then(result => setUserId(result));

  //        console.log(userId);
  //     })()
  // }, []);

  const [loading, setLoading] = useState(false);
  const [FormFiles, setFormFiles] = useState([]);

  const saveFile = (e) => {
    for (let index = 0; index < e.target.files.length; index++) {
      const element = e.target.files[index];
      setFormFiles((FormFiles) => [...FormFiles, element]);
    }
  };

  const uploadImage = async (e) => {
    e.preventDefault();

    const formData = new FormData();
    for (let index = 0; index < FormFiles.length; index++) {
      const element = FormFiles[index];
      formData.append("FormFiles", element);
    }

    formData.append("title", title);
    formData.append("userId", userId);
    formData.append("content", content);

    fetch("https://localhost:44366/api/Posts/create", {
      method: "POST",
      body: formData,
    });
  };

  return (
    <main className="form-signin">
      <form onSubmit={uploadImage}>
        <h1 className="h3 mb-3 fw-normal">
          Share your thoughts and moments with friends
        </h1>

        <div className="form-floating">
          <input
            type="text"
            className="form-control"
            onChange={(e) => setTitle(e.target.value)}
          />
          <label>Post title</label>
        </div>

        <div className="form-floating">
          <input
            type="text"
            className="form-control"
            onChange={(e) => setContent(e.target.value)}
          />
          <label>Content</label>
        </div>

        <div className="form-floating">
          <input
            multiple
            type="file"
            className="form-control"
            onChange={saveFile}
          />
          <label>Image/s</label>
        </div>

        <button className="w-100 btn btn-lg btn-primary" type="submit">
          Post
        </button>
        <p className="mt-5 mb-3 text-muted">&copy; 2017–2021</p>
      </form>
    </main>
  );
};

export default CreatePost;
