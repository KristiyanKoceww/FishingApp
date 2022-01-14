import React, { useEffect, useState, useMemo } from "react";
import Fish from "./Fish";
import './AllFish.css'
import ErrorNotification from "../ErrorsManagment/ErrorNotification";
import Footer from "../Footer/Footer";
const RenderAllFish = () => {
  const [fish, setFish] = useState([]);
  const [error, setError] = useState();
  const getAllFishUrl = process.env.REACT_APP_GETALLFISH;
  const jwt = localStorage.getItem("jwt");

  useEffect(() => {
    (async () => {
      await fetch(getAllFishUrl, {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + jwt,
        },
      })
        .then((r) => {
          if (!r.ok) {
            throw new Error("Failed to get the data from server!");
          }
          return r.json();
        })
        .then((r) => {
          setFish(r);
        })
        .catch((err) => setError(err.message));
    })();
  }, []);

  return (
    <div>
      {error ? (
        <div>
          {" "}
          <ErrorNotification message={error} />
        </div>
      ) : (
        <div className="container">
          <div className="row m-1">
            {fish.map((fish, index) => {
              return <Fish key={fish.id} index={index} {...fish} />;
            })}
          </div>
          <div className="fishfooter">
            <Footer />
          </div>
        </div>
      )}
    </div>
  );
};
export default RenderAllFish;
