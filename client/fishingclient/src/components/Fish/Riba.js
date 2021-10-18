import React, {useMemo} from "react";
import useFetch from "../../customHooks/useFetch";
import {
    Card, CardImg, CardText, CardBody,
    CardTitle, CardSubtitle, Button
  } from 'react-bootstrap';
  import { CardGroup } from 'react-bootstrap';


  
const RenderRibi = () => {
    const[fish,isFishLoading] = useFetch(`https://localhost:44366/api/Fish/getAllFish`,{});

return(
    <div>
       {fish.map((w,i) => {
          return <CardGroup>
             <Card>
               <Card.Img variant="top" src={w.ImageUrls[0].ImageUrl} width='100px' height="150px" alt="Fish Image"/>
               <Card.Body>
                 <Card.Title>{w.Name}</Card.Title>
                <Card.Text>
                   {w.Description.slice(0, 40)}
                 </Card.Text>
               <Button variant="primary">Learn more</Button>
               </Card.Body>
             </Card>
             <br />
             </CardGroup>
       })}
       </div>
  )
    }

    export default RenderRibi;