import React, { useEffect, useState, useMemo } from "react";
import { useParams, Link } from "react-router-dom";

import Footer from '../Footer/Footer'
import './KnotInfoPage.css'
import ImageSlider from "../ImageSlider/ImageSlider";
import ErrorNotification from "../ErrorsManagment/ErrorNotification";

const KnotInfoPage = (props) => {
  const { id } = useParams();
  const [knot, setKnot] = useState();
  const [error, setError] = useState();
  const [isLoading, setisLoading] = useState(true);

  const jwt = localStorage.getItem("jwt");
  const getKnotByNameUrl = process.env.REACT_APP_GETKNOTBYNAME;

  useEffect(() => {
    const fetchData = () => {
      const fetchUrl = getKnotByNameUrl + id;
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
          return res.json()
        })
        .then((result) => {
          setKnot(result);
          setError(null);
          setisLoading(false);
        })
        .catch((err) => {
          setError(err.message);
        });
    };
    fetchData();
  }, [id]);

  return (
    <div>
      {error && <div> <ErrorNotification message={error} /></div>}
      {!error && isLoading && <h1>Loading...</h1>}
      {!error && knot && <div className="container2">

        <div className="row m-2">
          <h1 className="text-center">{knot.name}</h1>
          <ImageSlider slides={knot.imageUrls} />
          <div className="Description">
            Description:
            <div className="Description2">{knot.description}</div>
            <hr />
          </div>
          <div>Type: {knot.type}</div>
          <div>Video: </div>
          <div><iframe width="1024" height="500" src="https:www.youtube.com/embed/watch?v=8BYhymQptAo&list=RD8BYhymQptAo&start_radio=1?" > </iframe></div>
          <hr />
        </div>
      </div>}
      <div className="back-button">
        <Link className="btn btn-primary" to={"/AllKnots/"}>
          Back
        </Link>
      </div>
      <div className="KnotFooter">
        <Footer />
      </div>
    </div>
  )
};
export default KnotInfoPage;
