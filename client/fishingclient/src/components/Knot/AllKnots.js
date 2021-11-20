import React, { useEffect, useState, useMemo } from "react";
import Knot from './Knot'

const RenderAllKnots = () => {
  const [knots, setKnots] = useState([]);

  useEffect(() => {
    const jwt = localStorage.getItem("jwt");
    (async () => {
      const response = await fetch('https://localhost:44366/api/Knots/getAllKnots',
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