import React, { useEffect, useState, useMemo } from "react";
import ImageSlider from "../ImageSlider/ImageSlider";
import { useParams, Link } from "react-router-dom";
import './FishInfoPage.css'
const FishInfoPage = (props) => {

    const [fish, setFish] = useState();
    const [isLoading, setisLoading] = useState(true);
    const url = 'https://localhost:44366/api/Fish/getFishByName?fishName=';
    const { id } = useParams();

    useEffect(() => {
        const fetchData = () => {
            const fetchUrl = url + id;
            fetch(fetchUrl)
                .then(res => res.json())
                .then(result => setFish(result))
                .catch(err => {
                    console.log(err);
                })
            setisLoading(false);
        }
        fetchData()
    }, []);

    const renderFish = useMemo(() => {
        if (isLoading === true) {
            return (
                <h1>Loading...</h1>
            )
        }
        else {
            return (
                <div className="container2">
                    <div className="row m-2">
                        <h1 className="text-center">{fish.Name}</h1>
                        <ImageSlider slides={fish.ImageUrls} />
                        <div className='Description'>
                            Description:
                            <div className='Description2'>
                                {fish.Description}
                            </div>
                        </div>
                        <hr/>
                        <div className='weight'>
                         Weight:
                            <div  className='weight2'>
                            {fish.Name} has max weight of {fish.Weight} kg. 
                            </div>
                        </div>
                        <hr/>
                        <div className='lenght'>
                            Lenght:
                            <div className='lenght2'>
                              {fish.Name} has max lenght of {fish.Lenght} sm.
                            </div>
                        </div>
                        <hr/>
                        <div className='habbitat'>
                        Habbitat:
                            <div className='habbitat2'>
                                This kind of fish are in {fish.Habbitat} habbitat.
                            </div>
                        </div>
                        <hr/>
                        <div className='nutrition'>
                        Nutrition:
                            <div className='nutrition2'>
                                This kind of fish are in {fish.Nutrition}.
                            </div>
                        </div>
                        <hr/>
                        <div className='tips'>
                        Some tips:
                            <div className='tips2'>
                               {fish.Tips}
                            </div>
                        </div>
                    </div>
                </div>
            )
        }
    }, [fish])

    return (
        <div>
            {renderFish}
            <Link className='btn btn-primary'  to={"/FishInfo/"}> Back </Link>
        </div>
    )
}

export default FishInfoPage;




