import React, { useEffect, useState, useMemo } from "react";
import ImageSlider from "../ImageSlider/ImageSlider";
import { useParams, Link } from "react-router-dom";
import Footer from '../Footer/Footer'

const KnotInfoPage = (props) => {
  const [knot, setKnot] = useState();
  const [isLoading, setisLoading] = useState(true);
  const getKnotByNameUrl = process.env.REACT_APP_GETKNOTBYNAME;
  const { id } = useParams();

  useEffect(() => {
    const jwt = localStorage.getItem("jwt");
    const fetchData = () => {
      const fetchUrl = getKnotByNameUrl + id;
      fetch(fetchUrl, {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + jwt,
        },
      })
        .then((res) => res.json())
        .then((result) =>{
          setKnot(result);
          console.log(result);
        } )
        .catch((err) => {
          console.log(err);
        });
      setisLoading(false);
    };
    fetchData();
  }, [id]);

  const renderKnot = useMemo(() => {
    if (isLoading === true) {
      return <h1>Loading...</h1>;
    } else {
      return (
        <div className="container2">
          <Link className="btn btn-primary" to={"/AllKnots/"}>
            {" "}
            Back{" "}
          </Link>
          <div className="row m-2">
            <h1 className="text-center">{knot.name}</h1>
            <ImageSlider slides={knot.imageUrls} />
            <div className="Description">
              Description:
              <div className="Description2">{knot.description}</div>
              <hr />
            </div>
            <div>Type: {knot.type}</div>
            <div>Video: </div>
            <div><iframe width="1024" height="500" src="https://www.youtube.com/embed/watch?v=8BYhymQptAo&list=RD8BYhymQptAo&start_radio=1?" > </iframe></div>
            <hr />
          </div>
          <hr />
          <Footer />
        </div>
      );
    }
  }, [knot]);

  return <div>{renderKnot}</div>;
};
export default KnotInfoPage;
