import React, { useEffect, useState, useMemo } from "react";
import Fish from "./Fish";
import Footer from '../Footer/Footer'
const RenderAllFish = () => {
  const [fish, setFish] = useState([]);
  const getAllFishUrl = process.env.REACT_APP_GETALLFISH;
  useEffect(() => {
    const jwt = localStorage.getItem("jwt");
    (async () => {
      const response = await fetch(
        getAllFishUrl,
        {
          method: "GET",
          headers: {
            "Content-Type": "application/json",
            Authorization: "Bearer " + jwt,
          },
        }
      );
      const content = await response.json();
      setFish(content);
    })();
  }, []);
  const renderFish = useMemo(() => {
    return (
      <div className="container">
        <div className="row m-2">
          {fish.map((fish, index) => {
            return <Fish key={fish.id} index={index} {...fish} />;
          })}
        </div>
        <Footer />
      </div>
    );
  }, [fish]);

  return <div>{renderFish}</div>;
};

export default RenderAllFish;
