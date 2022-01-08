import React, { useEffect, useState } from "react";
import Knot from './Knot'
import Footer from '../Footer/Footer'
import ErrorNotification from "../ErrorsManagment/ErrorNotification";
const RenderAllKnots = () => {
  const [knots, setKnots] = useState([]);
  const [error, setError] = useState();
  const [isLoading, setIsloading] = useState(true);

  const jwt = localStorage.getItem("jwt");
  const getAllKnotsUrl = process.env.REACT_APP_GETALLKNOTS;

  useEffect(() => {
    (async () => {
      await fetch(getAllKnotsUrl,
        {
          method: "GET",
          headers: {
            "Content-Type": "application/json",
            Authorization: "Bearer " + jwt,
          },
        })
        .then(r => {
          if (!r.ok) {
            throw new Error('Failed to get the data from server!')
          }
          return r.json();
        })
        .then(r => {
          setKnots(r);
          setError(null);
          setIsloading(false);
        })
        .catch(err => setError(err.message))
    })()
  }, []);

  return (
    <div>
      {error && <div> <ErrorNotification message={error} /></div>}
      {!error && isLoading && <h1>Loading...</h1>}
      {!isLoading && knots && <div className="container">
        <div className="row m-2">
          {knots.map((knot, index) => {
            return <Knot key={index} index={index} {...knot} />;
          })}
        </div>
      </div>}
      <Footer />
    </div>
  )
}

export default RenderAllKnots;