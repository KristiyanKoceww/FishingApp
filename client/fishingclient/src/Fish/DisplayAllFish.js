import React, { useEffect, useState } from "react";
import Fish from './Fish'

const DisplayAllFish = () => {
    const [fish, setFish] = useState([]);
    useEffect(() => {
        
        const url = "https://localhost:44366/api/Fish/getAllFish"
        const fetchData = async () => {
            try {
                const response = await fetch(url);
                const json = await response.json();
                console.log(json);
                // console.log(json.data);
                setFish(json);
                } catch (error) {
                console.log("error", error);
                }
        };

        fetchData();
    },[]);
   

      if (fish === undefined) {
        console.log(fish);
        return null;
      }
     
      return(
          
       <div>
     <table className="table table-striped">
     <thead>
         <tr className="text-justify">
             <th className="text-justify">
                Reservoirs
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