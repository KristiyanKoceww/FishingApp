import React, { useEffect, useState } from "react";
import useFetch from "../customHooks/useFetch";
import Fish from './Fish'

const DisplayAllFish = () => {

   const[fish,isFishLoading] = useFetch(`https://localhost:44366/api/Fish/getAllFish`,{});

      if (fish === undefined) {
        return null;
      }
     
      return(
          
       <div>
     <table className="table table-striped">
     <thead>
         <tr className="text-justify">
             <th className="text-justify">
                Fish
             </th>
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
     
         {fish.map(d=>
             <tr key={Math.random()}>
                 <td>{d.Name}</td>
                 <td>{d.Weight}</td>
                 <td>{d.Lengt}</td>
                 <td>{d.Habittat}</td>
                 <td>{d.Tips}</td>
                 <td>{d.Description}</td>
                 <td><img alt="fish"  src={d.ImageUrls[0].ImageUrl} width='100px' /></td> 
                 </tr>
                 )}
     </tbody>
 </table>
</div>
/* <Fish {...fish} /> */
)
};

export default DisplayAllFish;