import React, {useMemo} from "react";
import useFetch from "../customHooks/useFetch";
import Fish from './Fish'


const RenderAllFish = () => {
 const[fish,isFishLoading] = useFetch(`https://localhost:44366/api/Fish/getAllFish`,{});

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