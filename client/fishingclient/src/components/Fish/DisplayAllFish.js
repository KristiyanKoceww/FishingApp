import React, { useEffect, useState } from "react";
import Footer from '../Footer/Footer'
import ErrorNotification from '../ErrorsManagment/ErrorNotification'
const DisplayAllFish = () => {
  const [fish, setFish] = useState([]);
  const [error, setError] = useState();
  const jwt = localStorage.getItem("jwt");

  const getAllFishUrl = process.env.REACT_APP_GETALLFISH;

  useEffect(() => {
    (async () => {
      await fetch(
        getAllFishUrl,
        {
          method: "GET",
          headers: {
            "Content-Type": "application/json",
            Authorization: "Bearer " + jwt,
          },
        }
      ).then(r => {
        if (!r.ok) {
          throw new Error('Failed to get the data from server!')
        }
        return r.json();
      })
        .then(res => setFish(res))
        .catch(err => setError(err.message))
    })();
  }, []);


  if (fish === undefined) {
    return null;
  }
  return (
    <div>
      {error ? <div> <ErrorNotification message={error} /></div> :
        <table responsive className="table table-striped ">
          <thead>
            <tr className="text-justify">
              <th className="text-justify">Fish</th>
            </tr>
            <tr>
              <th>Name</th>
              <th>Weight</th>
              <th>Lengt</th>
              <th>Habittat</th>
              <th>Tips</th>
              <th>Description</th>
              <th>Image</th>
            </tr>
          </thead>
          <tbody>
            {fish.map((d) => (
              <tr key={Math.random()}>
                <td>{d.name}</td>
                <td>{d.weight}</td>
                <td>{d.lengt}</td>
                <td>{d.habittat}</td>
                <td>{d.tips}</td>
                <td>{d.description}</td>
                <td>
                  <img alt="fish" src={d.imageUrls[0].imageUrl} width="100px" />
                </td>
              </tr>
            ))}
          </tbody>
        </table>}
      <Footer />
    </div>
  );
};

export default DisplayAllFish;
