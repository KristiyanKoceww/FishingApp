import React, { useEffect, useState, useMemo } from "react";
import Fish from './Fish'

const RenderAllFish = () => {
  const [fish, setFish] = useState([]);

  useEffect(() => {
    (async () => {
      const response = await fetch('https://localhost:44366/api/Fish/getAllFish',
      )
      const content = await response.json();
      setFish(content);
    })()
  }, []);

  const renderFish = useMemo(() => {

    return (
      <div className="container">
        <div className="row m-2">
          {fish.map((fish, index) => {
            return <Fish key={fish.id} index={index} {...fish} />;
          })}
        </div>
      </div>
    )
  }, [fish])

  return (
    <div>
      {renderFish}
    </div>
  )
}

export default RenderAllFish;