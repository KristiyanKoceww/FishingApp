import React, { useState } from "react";
import "./CreateReservoir.css";

const CreateReservoir = () => {
  const [name, setName] = useState("");
  const [type, setType] = useState("");
  const [description, setDescription] = useState("");
  const [latitude, setLatitude] = useState(1);
  const [longitude, setLongitude] = useState(1);
  const [cityId, setCityId] = useState("");
  const [fish, setFish] = useState([]);
  const [images, setImages] = useState([]);

  const saveFile = (e) => {
    for (let index = 0; index < e.target.files.length; index++) {
      const element = e.target.files[index];
      setImages((images) => [...images, element]);
    }
  };

  const uploadImage = async (e) => {
    e.preventDefault();

    const formData = new FormData();
    for (let index = 0; index < images.length; index++) {
      const element = images[index];
      formData.append("images", element);
    }

    formData.append("name", name);
    formData.append("type", type);
    formData.append("description", description);
    formData.append("latitude", latitude);
    formData.append("longitude", longitude);
    formData.append("cityId", cityId);
    
    const jwt = localStorage.getItem("jwt");
    fetch("https://localhost:44366/api/Reservoir/create", {
      method: "POST",
      headers: { Authorization: "Bearer " + jwt },
      body: formData,
    });
  };

  return (
    <main className="form-signin">
      <form onSubmit={uploadImage}>
        <h1 className="h3 mb-3 fw-normal">Fill data to create new reservoir</h1>

        <div className="form-floating">
          <input
            type="text"
            className="form-control"
            onChange={(e) => setName(e.target.value)}
          />
          <label>Reservoir name</label>
        </div>

        <div className="form-floating">
          <input
            type="text"
            className="form-control"
            onChange={(e) => setType(e.target.value)}
          />
          <label>Type</label>
        </div>

        <div className="form-floating">
          <textarea
            className="form-control"
            rows="100"
            onChange={(e) => setDescription(e.target.value)}
          />
          <label>Description</label>
        </div>

        <div className="form-floating">
          <input
            type="text"
            step="any"
            className="form-control"
            onChange={(e) => setLatitude(e.target.value)}
          />
          <label>Latitude</label>
        </div>

        <div className="form-floating">
          <input
            type="text"
            step="any"
            className="form-control"
            onChange={(e) => setLongitude(e.target.value)}
          />
          <label>Longitude</label>
        </div>

        <div className="form-floating">
          <input
            type="text"
            className="form-control"
            onChange={(e) => setCityId(e.target.value)}
          />
          <label>City ID</label>
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
          Submit
        </button>
        <p className="mt-5 mb-3 text-muted">&copy; 2017–2021</p>
      </form>
    </main>
  );
};

export default CreateReservoir;
