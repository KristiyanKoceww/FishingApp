import React, { useEffect,useState,useMemo} from "react";
import Fish from './Fish'


const RenderAllFish = () => {
  const[fish,setFish] = useState([]) //useFetch(`https://localhost:44366/api/Fish/getAllFish`,{});

  useEffect(() => {
     (async () => {
       const response = await fetch('https://localhost:44343/api/Fish/getAllFish',
       )
       const content = await response.json();
       setFish(content);
     })()
   });
    
console.log(fish);
 const renderFish = useMemo(() => {
    return fish.map((fish, index) => {
      return (
        <Fish key={fish.id} index={index} {...fish} />
      )
    })
  }, [fish])

  return (
      <div>
        {renderFish}
      </div>
    )
  }

  export default RenderAllFish;