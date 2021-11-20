import React, { useEffect, useState, useMemo } from "react";
import Reservoir from "./Reservoir";

const RenderAllReservoirs = () => {
  const [reservoir, setReservoir] = useState([]);
 
  useEffect(() => {
    const jwt = localStorage.getItem("jwt");
    (async () => {
      const response = await fetch(
        "https://localhost:44366/api/Reservoir/getAllReservoirs",
        {
          method: "GET",
          headers: {
            "Content-Type": "application/json",
            Authorization: "Bearer " + jwt,
          },
        }
      );
      const content = await response.json();
      setReservoir(content);
    })();
  }, []);
  const renderReservoirs = useMemo(() => {
    return (
      <div className="container">
        <h1>Search..</h1>
        <div className="row m-2">
          {reservoir.map((reservoir, index) => {
            return <Reservoir key={index} index={index} {...reservoir} />;
          })}
        </div>
      </div>
    );
  }, [reservoir]);

  return <div>{renderReservoirs}</div>;
};

export default RenderAllReservoirs;
