import React, { useEffect, useState, useMemo } from "react";
import { useParams, Link } from "react-router-dom";

import "./ReservoirInfoPage.css";
import Map from "../GoogleMap/Map";
import Footer from '../Footer/Footer'
import ImageSlider from "../ImageSlider/ImageSlider";
import ErrorNotification from '../ErrorsManagment/ErrorNotification'

const ReservoirInfoPage = (props) => {
  const [reservoir, setReservoir] = useState();
  const [isLoading, setisLoading] = useState(true);
  const [error, setError] = useState();
  const { id } = useParams();
  const jwt = localStorage.getItem("jwt");
  const getReservoirByNameUrl = process.env.REACT_APP_GETRESERVOIRBYNAME;

  const fetchData = () => {
    const fetchUrl = getReservoirByNameUrl + id;
    fetch(fetchUrl, {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + jwt,
      },
    })
      .then(res => {
        console.log(res);
        if (!res.ok) {
          throw new Error('Failed to fetch the data from server!');
        }
        return res.json()
      })
      .then((result) => {
        setReservoir(result);
        setisLoading(false);
      })
      .catch((err) => {
        setisLoading(false);
        setError(err.message);
      });

  };

  useEffect(() => {
    fetchData();
  }, [id]);

  return (
    <div>
      {error && <div> <ErrorNotification message={error} /></div>}
      {isLoading && <h1>Loading...</h1>}
      {reservoir &&
        <div className="container2">
          <Link className="btn btn-primary" to={"/AllReservoirs/"}>
            {" "}
            Back{" "}
          </Link>
          <div className="row m-2">
            <h1 className="text-center">{reservoir.name}</h1>
            <div className="slider">
              <ImageSlider slides={reservoir.imageUrls} />
            </div>
            <div className="Description">
              Description:
              <div className="Description2">{reservoir.description}</div>
              <hr />
              Type:
              <div className="Description2">{reservoir.type}</div>
              <hr />
              Reservoir coordinates:
              <div className="coords">
                <p>Latitude: {reservoir.latitude}</p>
                <p>Longitude: {reservoir.longitude}</p>
              </div>
            </div>
            <hr />
            <div className="city">
              City:
              <div className="city2">
                {reservoir.name} ???? ???????????? ??{" "}
                {reservoir.city.name ? reservoir.city.name : "No name"},
                {reservoir.city.countryName}.
                {reservoir.city.description}
              </div>
            </div>
          </div>
          <hr />
          <h1 className="text-center">{reservoir.name} location:</h1>
          <div className="d-flex justify-content-center">
            <Map props={reservoir} />
          </div>
          <div className="footer4">
            <Footer />
          </div>
        </div>
      }
    </div>
  )
};
export default ReservoirInfoPage;