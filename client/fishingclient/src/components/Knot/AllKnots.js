import React, { useEffect, useState, useMemo } from "react";
import Knot from './Knot'
import Footer from '../Footer/Footer'
const RenderAllKnots = () => {
  const [knots, setKnots] = useState([]);
  const getAllKnotsUrl = process.env.REACT_APP_GETALLKNOTS;
  useEffect(() => {
    const jwt = localStorage.getItem("jwt");
    (async () => {
      const response = await fetch(getAllKnotsUrl,
      {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + jwt,
        },
      }
      )
      const content = await response.json();
      setKnots(content);
    })()
  }, []);
  const renderKnots = useMemo(() => {
    return (
      <div className="container">
        <div className="row m-2">
          {knots.map((knot, index) => {
            return <Knot key={index} index={index} {...knot} />;
          })}
        </div>
        <Footer />
      </div>
    )
  }, [knots])

  return (
    <div>
      {renderKnots}
    </div>
  )
}

export default RenderAllKnots;