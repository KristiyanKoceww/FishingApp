import { React, useState, useContext, useEffect } from "react";
import { Link } from "react-router-dom";
import { UserContext } from "../AcountManagment/UserContext";
import Post from "./Post";
import CreatePost from "./CreatePost";
import Reservoir from "../Reservoir/Reservoir";
import InfiniteScroll from "react-infinite-scroll-component";
import { ScrollToTop } from "../Scroll/ScrollToTop";
import { Button } from "primereact/button";
import Spinner from "../Helpers/Spinners/Spinner";
import EndOfPosts from "../Helpers/InfoForEndOfPosts/EndOfPosts";
import UserDetails from "../AcountManagment/UserDetails/UserDetails";

import "./Posts.css";
import "primereact/resources/themes/lara-light-indigo/theme.css";
import "primereact/resources/primereact.min.css";
import "primeicons/primeicons.css";

const Posts = (props) => {
  const [createFormToggle, setCreateFormToggle] = useState(false);
  const [UserInfo, setUserInfo] = useState();
  const [reservoirs, setReservoirs] = useState();
  const [fish,setFish] = useState();
  const [error, setError] = useState();
  const { appUser, setAppUser } = useContext(UserContext);

  const getFourPopularReservoirs = 'https://localhost:44366/api/Reservoir/getFourPopularReservoirs';
  const getFourPopularFish = 'https://localhost:44366/api/Fish/getFourPopularFish';
  const toggleForm = () => {
    setCreateFormToggle(!createFormToggle);
  };

  const userId = localStorage.getItem("userId");
  const jwt = localStorage.getItem("jwt");

  useEffect(() => {
    fetch(
      `https://localhost:44366/api/AppUsers/getUserInfo/id?userId=` + userId,
      {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + jwt,
        },
      }
    ).then((r) => r.json().then((r) => setUserInfo(r)));
  }, []);

  useEffect(() => {
    (async () => {
      await fetch(getFourPopularReservoirs, {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + jwt,
        },
      })
        .then((r) => {
          if (!r.ok) {
            throw new Error("Fetching the data from server failed!");
          }
          return r.json();
        })
        .then((res) => {
          setReservoirs(res);
        })
        .catch((err) => setError(err.message));
    })();
  }, []);

  useEffect(() => {
    (async () => {
      await fetch(getFourPopularFish, {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
          Authorization: "Bearer " + jwt,
        },
      })
        .then((r) => {
          if (!r.ok) {
            throw new Error("Fetching the data from server failed!");
          }
          return r.json();
        })
        .then((res) => {
          setFish(res);
        })
        .catch((err) => setError(err.message));
    })();
  }, []);


  return (
    <div className="container">
      <div className="row">
        <div className="firstcol">
          {UserInfo ? <UserDetails user={UserInfo} /> : null}
        </div>

        <div className="secondcol">
          <div className="create_post_button">
            {Object.keys(appUser ? appUser : {}).length !== 0 ? (
              <div className="toggleDiv">
                <Button
                  className="toggleFormButton"
                  icon="pi pi-plus"
                  label="Create post"
                  icon="pi pi-check"
                  iconPos="left"
                  onClick={toggleForm}
                />
                {createFormToggle && (
                  <CreatePost onCreate={props.updatePosts} />
                )}
              </div>
            ) : (
              <div className="logintocreatepost">
                <Link to="/Login">Login</Link> to create post
              </div>
            )}
          </div>

          <div>
            {props.posts.length > 0 ? (
              <div className="infiniteScroll">
                <InfiniteScroll
                  dataLength={props.posts.length}
                  next={props.fetchData}
                  hasMore={props.hasMore}
                  loader={<Spinner />}
                  endMessage={<EndOfPosts />}
                >
                  {props.posts &&
                    props.posts.map((item, index) => {
                      return <Post key={index} {...item} />;
                    })}
                </InfiniteScroll>
                <ScrollToTop />
              </div>
            ) : (
              <div className="NoPostsYet">
                There are no posts yet.Be the first person to create post!
              </div>
            )}
          </div>
        </div>

        <div className="thirtcol">
          <h5>Some popular reservoirs:</h5>
          {reservoirs &&
            reservoirs.map((reservoir, index) => {
              return (
                <div>
                  <Link to={"/reservoirInfoPage/" + reservoir.name}>
                    {reservoir.name}
                  </Link>
                  <div>
                    {" "}
                    <img
                      src={reservoir.imageUrls[0].imageUrl}
                      alt="reservoir"
                      width="200px"
                      height="70px"
                    />{" "}
                  </div>
                </div>
              );
            })}
            <h5>Some popular fish:</h5>
            {fish &&
            fish.map((fish, index) => {
              return (
                <div>
                  <Link to={"/fishInfoPage/" + fish.name}>
                    {fish.name}
                  </Link>
                  <div>
                    {" "}
                    <img
                      src={fish.imageUrls[0].imageUrl}
                      alt="fish"
                      width="200px"
                      height="70px"
                    />{" "}
                  </div>
                </div>
              );
            })}
        </div>
      </div>
    </div>
  );
};
export default Posts;
