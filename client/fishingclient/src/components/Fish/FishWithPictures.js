import React, { useEffect, useState, useMemo } from "react";
import Fish from "./Fish";

const FishWithPictures = () => {
  const [fish, setFish] = useState([]);
useEffect( async ()  =>{
    const response = await fetch(
        "https://localhost:44343/api/Fish/getAllFish"
      );
      const content = await response.json();
      console.log(content);
      setFish(content);
},[])
//   useEffect(() => {
//     (async () => {
//       const response = await fetch(
//         "https://localhost:44343/api/Fish/getAllFish"
//       );
//       const content = await response.json();
//       console.log(content);
//       setFish(content);
//     })();
//   });

  return (
    <div className="container">
        <div className="row m-2">
          {fish.map((item) => {
            return <Fish key={item.id} {...fish} />;
          })}
        </div>
      </div>
  );
};

export default FishWithPictures
