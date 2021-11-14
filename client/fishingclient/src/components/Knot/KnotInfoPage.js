import React, { useEffect, useState, useMemo } from "react";
import ImageSlider from "../ImageSlider/ImageSlider";
import { useParams, Link } from "react-router-dom";
//import './ReservoirInfoPage.css'
const KnotInfoPage = (props) => {

    const [knot, setKnot] = useState();
    const [isLoading, setisLoading] = useState(true);
    const url = 'https://localhost:44366/api/Knot/getByName?knotName=';
    const { id } = useParams();

    useEffect(() => {
        const fetchData = () => {
            const fetchUrl = url + id;
            fetch(fetchUrl)
                .then(res => res.json())
                .then(result => setKnot(result))
                .catch(err => {
                    console.log(err);
                })
            setisLoading(false);
            
        }
        fetchData()
    }, []);

    const renderKnot = useMemo(() => {
        if (isLoading === true) {
            return (
                <h1>Loading...</h1>
            )
        }
        else {
            return (
                <div className="container2">
                    <Link className='btn btn-primary' to={"/AllKnots/"}> Back </Link>
                    <div className="row m-2">
                        <h1 className="text-center">{knot.Name}</h1>
                        <ImageSlider slides={knot.ImageUrls} />
                        <div className='Description'>
                            Description:
                            <div className='Description2'>
                                {knot.Description}
                            </div>
                            <hr />
                        </div>
                        <hr />
                    </div>
                    <hr/>
                </div>
            )
        }
    }, [knot])

    return (
        <div>
            {renderKnot}
        </div>
    )
}
export default KnotInfoPage;




