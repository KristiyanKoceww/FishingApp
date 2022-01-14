import React, { useEffect, useState } from "react";
import { useParams, Link } from "react-router-dom";

import "./FishInfoPage.css";
import Footer from '../Footer/Footer'
import ImageSlider from "../ImageSlider/ImageSlider";
const FishInfoPage = (props) => {
  const [fish, setFish] = useState();
  const [error, setError] = useState();
  const [isLoading, setisLoading] = useState(true);
  const getFishByNameUrl = process.env.REACT_APP_GETFISHBYNAME;
  const { id } = useParams();

  useEffect(() => {
    const jwt = localStorage.getItem("jwt");
    const fetchData = () => {
      const fetchUrl = getFishByNameUrl + id;
      fetch(fetchUrl, {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + jwt,
        },
      })
        .then((res) => {
          if (!res.ok) {
            throw new Error('Failed to get the data from server!')
          }
          return res.json();
        })
        .then((result) => {
          setFish(result);
          setisLoading(false);
        })
        .catch((err) => {
          setError(err.message);
        });
    };
    fetchData();
  }, []);


  return (
    <div>
      {isLoading && <h1>Loading...</h1>}
      {error && <div> <ErrorNotification message={error} /></div>}
      {fish && <div className="container2">
        <div className="row m-2">
          <h1 className="text-center">{fish.name}</h1>
          <ImageSlider slides={fish.imageUrls} />
          <div className="Description">
            Description:
            <div className="Description2">{fish.description}</div>
          </div>
          <hr />
          <div className="weight">
            Weight:
            <div className="weight2">
              {fish.Name} has max weight of {fish.weight} kg.
            </div>
          </div>
          <hr />
          <div className="lenght">
            Lenght:
            <div className="lenght2">
              {fish.Name} has max lenght of {fish.lenght} sm.
            </div>
          </div>
          <hr />
          <div className="habbitat">
            Habbitat:
            <div className="habbitat2">
               {fish.habittat}
            </div>
          </div>
          <hr />
          <div className="nutrition">
            Nutrition:
            <div className="nutrition2">
             {fish.nutrition}
            </div>
          </div>
          <hr />
          <div className="tips">
            Some tips:
            <div className="tips2">{fish.tips}</div>
          </div>
        </div>
      </div>}
      <div className="back-button">
        <Link className="btn btn-primary" to={"/FishInfo/"}>Back</Link>
      </div>
      <div className="FishFooter">
        <Footer />
      </div>
    </div>
  )
};

export default FishInfoPage;
