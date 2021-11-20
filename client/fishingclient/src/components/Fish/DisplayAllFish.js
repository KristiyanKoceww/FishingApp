import React, { useEffect, useState } from "react";

const DisplayAllFish = () => {
  const [fish, setFish] = useState([]);

  useEffect(() => {
    const jwt = localStorage.getItem("jwt");
    (async () => {
      const response = await fetch(
        "https://localhost:44366/api/Fish/getAllFish",
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
  if (fish === undefined) {
    return null;
  }

  return (
    <div>
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
              <td>{d.Name}</td>
              <td>{d.Weight}</td>
              <td>{d.Lengt}</td>
              <td>{d.Habittat}</td>
              <td>{d.Tips}</td>
              <td>{d.Description}</td>
              <td>
                <img alt="fish" src={d.ImageUrls[0].ImageUrl} width="100px" />
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default DisplayAllFish;
