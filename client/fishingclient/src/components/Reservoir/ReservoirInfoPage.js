import React, { useEffect, useState, useMemo } from "react";
import ImageSlider from "../ImageSlider/ImageSlider";
import { useParams, Link } from "react-router-dom";
import './ReservoirInfoPage.css'
const ReservoirInfoPage = (props) => {

    const [reservoir, setReservoir] = useState();
    const [isLoading, setisLoading] = useState(true);
    const url = 'https://localhost:44366/api/Reservoir/getByName?reservoirName=';
    const { id } = useParams();

    useEffect(() => {
        const fetchData = () => {
            const fetchUrl = url + id;
            fetch(fetchUrl)
                .then(res => res.json())
                .then(result => setReservoir(result))
                .catch(err => {
                    console.log(err);
                })
            setisLoading(false);
        }
        fetchData()
    }, []);

    const renderReservoir = useMemo(() => {
        if (isLoading === true) {
            return (
                <h1>Loading...</h1>
            )
        }
        else {
            return (
                <div className="container2">
                    <div className="row m-2">
                        <h1 className="text-center">{reservoir.Name}</h1>
                        <ImageSlider slides={reservoir.ImageUrls} />
                        <div className='Description'>
                            Description:
                            <div className='Description2'>
                                {reservoir.Description}
                            </div>
                            <hr/>
                            Type:
                            <div className='Description2'>
                                {reservoir.Type}
                            </div>
                            <hr/>
                            Reservoir coordinates:
                            <div className='coords'>
                               <p>Latitude: {reservoir.Latitude}</p>
                               <p>Longitude: {reservoir.Longitude}</p>
                            </div>
                        </div>
                        <hr/>
                        <div className='city'>
                         City:
                            <div  className='city2'>
                            {reservoir.Name} is located in {reservoir.City.Name},{reservoir.City.CountryName}.
                            {reservoir.City.Name} is {reservoir.City.Description}
                            </div>
                        </div>
                    </div>
                </div>
            )
        }
    }, [reservoir])

    return (
        <div>
            {renderReservoir}
            <Link className='btn btn-primary'  to={"/AllReservoirs/"}> Back </Link>
        </div>
    )
}

export default ReservoirInfoPage;




