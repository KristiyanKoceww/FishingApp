import React, { useEffect, useState, useMemo } from "react";
import './AllReservoirs.css'
import Reservoir from "./Reservoir";
import Footer from '../Footer/Footer'
import ErrorNotification from "../ErrorsManagment/ErrorNotification";
const RenderAllReservoirs = () => {
  const [reservoir, setReservoir] = useState([]);
  const [error, setError] = useState();

  const jwt = localStorage.getItem("jwt");
  const getAllReservoirsUrl = process.env.REACT_APP_GETALLRESERVOIRS;

  useEffect(() => {
    (async () => {
      await fetch(getAllReservoirsUrl,
        {
          method: "GET",
          headers: {
            "Content-Type": "application/json",
            Authorization: "Bearer " + jwt,
          },
        }
      ).then(r => {
        if (!r.ok) {
          throw new Error('Fetching the data from server failed!')
        }
        return r.json()
      })
        .then(res => {
          setReservoir(res);
        })
        .catch(err => setError(err.message))
    })();
  }, []);

  return (
    <div className="container">
      {error && <div> <ErrorNotification message={error} /></div>}
      {!error &&
        <div>
          <div className="row m-2">
            {reservoir.map((reservoir, index) => {
              return <Reservoir key={index} index={index} {...reservoir} />;
            })}
          </div>
          <div className="footer3">
            <Footer />
          </div>
        </div>
      }
    </div>
  );
};

export default RenderAllReservoirs;
