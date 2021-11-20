import React, { useState } from "react";
const CreateKnot = () => {
  const [name, setName] = useState("");
  const [type, setType] = useState("");
  const [description, setDescription] = useState("");
  const [Images, setImages] = useState([]);

  const saveFile = (e) => {
    for (let index = 0; index < e.target.files.length; index++) {
      const element = e.target.files[index];
      setImages((Images) => [...Images, element]);
    }
  };

  const onSubmit = async (e) => {
    e.preventDefault();

    const formData = new FormData();
    for (let index = 0; index < Images.length; index++) {
      const element = Images[index];
      formData.append("Images", element);
    }

    formData.append("Name", name);
    formData.append("Type", type);
    formData.append("Description", description);
    const jwt = localStorage.getItem("jwt");
    fetch("https://localhost:44366/api/Knots/create", {
      method: "POST",
      headers: { Authorization: "Bearer " + jwt },
      body: formData,
    }).catch((error) => {
      console.error("Error:", error);
    });
  };

  return (
    <main className="form-signin">
      <form onSubmit={onSubmit}>
        <h1 className="h3 mb-3 fw-normal">Please fill in data. </h1>
        <h5>Fields with * are required.</h5>

        <div className="form-floating">
          <input
            required
            type="text"
            className="form-control"
            onChange={(e) => setName(e.target.value)}
          />
          <label htmlFor="floatingInput">* Name</label>
        </div>

        <div className="form-floating">
          <input
            required
            type="text"
            className="form-control"
            onChange={(e) => setType(e.target.value)}
          />
          <label htmlFor="floatingInput">* Knot type</label>
        </div>

        <div className="form-floating">
          <input
            required
            type="text"
            className="form-control"
            onChange={(e) => setDescription(e.target.value)}
          />
          <label htmlFor="floatingInput">* Description</label>
        </div>

        <div className="form-floating">
          <input
            required
            multiple
            type="file"
            className="form-control"
            onChange={saveFile}
          />
          <label htmlFor="floatingInput">Images</label>
        </div>

        <button className="w-100 btn btn-lg btn-primary" type="submit">
          Submit
        </button>
        <p className="mt-5 mb-3 text-muted">&copy; 2017–2021</p>
      </form>
    </main>
  );
};

export default CreateKnot;
