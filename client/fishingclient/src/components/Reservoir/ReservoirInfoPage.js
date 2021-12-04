import React, { useEffect, useState, useMemo } from "react";
import ImageSlider from "../ImageSlider/ImageSlider";
import { useParams, Link } from "react-router-dom";
import "./ReservoirInfoPage.css";
import Map from "../GoogleMap/Map";

const ReservoirInfoPage = (props) => {
  const [reservoir, setReservoir] = useState();
  const [isLoading, setisLoading] = useState(true);
  const [latitude, setLatitude] = useState(0);
  const [longitude, setLongitude] = useState(0);
  const url = "https://localhost:44366/api/Reservoir/getByName?reservoirName=";
  const { id } = useParams();

  useEffect(() => {
    const jwt = localStorage.getItem("jwt");
    const fetchData = () => {
      const fetchUrl = url + id;
      fetch(fetchUrl, {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + jwt,
        },
      })
        .then((res) => res.json())
        .then((result) => {
          setReservoir(result),
            setLatitude(reservoir.Latitude),
            setLongitude(reservoir.Longitude);
        })
        .catch((err) => {
          console.log(err);
        });
      setisLoading(false);
    };
    fetchData();
  }, [id]);

  const zoomOutProperties = {
    duration: 4000,
    transitionDuration: 800,
    infinite: true,
    scale: 0.4,
  };

  const renderReservoir = useMemo(() => {
    if (isLoading === true) {
      return <h1>Loading...</h1>;
    } else {
      return (
        <div className="container2">
          <Link className="btn btn-primary" to={"/AllReservoirs/"}>
            {" "}
            Back{" "}
          </Link>
          <div className="row m-2">
            <h1 className="text-center">{reservoir.Name}</h1>
            <div className="slider">
              <ImageSlider slides={reservoir.ImageUrls} />
            </div>
            <div className="Description">
              Description:
              <div className="Description2">{reservoir.Description}</div>
              <hr />
              Type:
              <div className="Description2">{reservoir.Type}</div>
              <hr />
              Reservoir coordinates:
              <div className="coords">
                <p>Latitude: {reservoir.Latitude}</p>
                <p>Longitude: {reservoir.Longitude}</p>
              </div>
            </div>
            <hr />
            <div className="city">
              City:
              <div className="city2">
                {reservoir.Name} is located in{" "}
                {reservoir.City.Name ? reservoir.City.Name : "No name"},
                {reservoir.City.CountryName}.{reservoir.City.Name} is{" "}
                {reservoir.City.Description}
              </div>
            </div>
          </div>
          <hr />
          <h1 className="text-center">{reservoir.Name} location:</h1>
          <div className="d-flex justify-content-center">
            {/* <Map props={latitude,longitude} /> */}
          </div>
        </div>
      );
    }
  }, [reservoir]);

  return <div>{renderReservoir}</div>;
};
export default ReservoirInfoPage;
