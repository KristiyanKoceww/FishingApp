import React, { useEffect, useState} from "react";
import ImageSlider from "../ImageSlider/ImageSlider";

const FishInfoPage = (props) =>{

 const [fish,setFish] = useState();
 const [isLoading,setisLoading] = useState(false);
 const [fishId,setfishId] = useState('');
 const url = 'https://localhost:44366/api/Fish/getFishById?fishId=';

 useEffect(() => {
    setfishId(props.location.state.myProp);
        setisLoading(true);
      const response =  fetch(url + fishId)
      const content =  response.json();
      setFish(content);
      setisLoading(false);
    },[]);

  if (isLoading) {
      return(
          <div>Loading...</div>
      )
  }
  else{
    return (
        <div>
        <h1>{fish.Name}</h1>
        <ImageSlider slides={fish.ImageUrls} />
        </div>
    )
  }
}

export default FishInfoPage;




