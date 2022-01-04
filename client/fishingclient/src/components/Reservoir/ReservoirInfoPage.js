import React, { useEffect, useState, useMemo } from "react";
import { useParams, Link } from "react-router-dom";
import Footer from '../Footer/Footer'
import ImageSlider from "../ImageSlider/ImageSlider";
import Map from "../GoogleMap/Map";
import "./ReservoirInfoPage.css";

const ReservoirInfoPage = (props) => {
  const [reservoir, setReservoir] = useState();
  const [isLoading, setisLoading] = useState(true);

  const { id } = useParams();
  const jwt = localStorage.getItem("jwt");
  const url = "https://localhost:44366/api/Reservoir/getByName?reservoirName=";

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
        setReservoir(result)
      })
      .catch((err) => {
        console.log(err);
      });
    setisLoading(false);
  };

  useEffect(() => {
    fetchData();
  }, [id]);

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
                {reservoir.name} се намира в{" "}
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
      );
    }
  }, [reservoir]);

  return <div>{renderReservoir}</div>;
};
export default ReservoirInfoPage;