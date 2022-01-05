import React, { useEffect, useState, useMemo } from "react";
import Reservoir from "./Reservoir";
import Footer from '../Footer/Footer'
import './AllReservoirs.css'
const RenderAllReservoirs = () => {
  const [reservoir, setReservoir] = useState([]);
  const getAllReservoirsUrl = process.env.REACT_APP_GETALLRESERVOIRS;
  useEffect(() => {
    const jwt = localStorage.getItem("jwt");
    (async () => {
      const response = await fetch(getAllReservoirsUrl,
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
        <div className="footer3">
          <Footer />
        </div>
      </div>
    );
  }, [reservoir]);

  return <div>{renderReservoirs}</div>;
};

export default RenderAllReservoirs;
