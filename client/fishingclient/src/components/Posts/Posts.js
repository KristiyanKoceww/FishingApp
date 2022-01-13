import { React, useState, useContext, useEffect } from "react";
import { Link } from 'react-router-dom';
import { Button } from "primereact/button";
import Post from "./Post";
import CreatePost from "./CreatePost";
import InfiniteScroll from "react-infinite-scroll-component";
import { ScrollToTop } from '../Scroll/ScrollToTop';
import { UserContext } from '../AcountManagment/UserContext';
import Spinner from "../Helpers/Spinners/Spinner";
import EndOfPosts from '../Helpers/InfoForEndOfPosts/EndOfPosts'
import UserDetails from "../AcountManagment/UserDetails/UserDetails"

import "./Posts.css";
import "primereact/resources/themes/lara-light-indigo/theme.css";
import "primereact/resources/primereact.min.css";
import "primeicons/primeicons.css";

const Posts = (props) => {
  const [createFormToggle, setCreateFormToggle] = useState(false);
  const { appUser, setAppUser } = useContext(UserContext);
  const [testUser, setTestUser] = useState();

  const toggleForm = () => {
    setCreateFormToggle(!createFormToggle);
  };

  const userId = localStorage.getItem('userId');
  const jwt = localStorage.getItem('jwt');
  useEffect(() => {

    fetch(`https://localhost:44366/api/AppUsers/getUserInfo/id?userId=` + userId, {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
        Authorization: "Bearer " + jwt
      }
    }).then(r => r.json().then(r => setTestUser(r)));


  },[]);

  return (
    <div className="container">
      <div className="row">
        <div className="firstcol">
          {testUser ?
            <UserDetails user={testUser} /> : null}
        </div>

        <div className="secondcol">
          {Object.keys(appUser ? appUser : {}).length !== 0 ?
            <div className="toggleDiv">
              <Button
                className="toggleFormButton"
                icon="pi pi-plus"
                label="Create post"
                icon="pi pi-check"
                iconPos="left"
                onClick={toggleForm}
              />
              {createFormToggle && <CreatePost onCreate={props.updatePosts} />}
            </div>
            : <div className="logintocreatepost">
              <Link to="/Login">Login</Link> to create post
            </div>}

          {props.posts.length > 0 ?
            <div className="infiniteScroll">
              <InfiniteScroll
                dataLength={props.posts.length}
                next={props.fetchData}
                hasMore={props.hasMore}
                loader={<Spinner />}
                endMessage={<EndOfPosts />}
              >
                {props.posts && props.posts.map((item, index) => {
                  return <Post key={index} {...item} />;
                })}
              </InfiniteScroll>
              <ScrollToTop />
            </div>
            : <div className="NoPostsYet">There are no posts yet.Be the first person to create post!</div>}
        </div>

        <div className="thirtcol">
          <p>orem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.</p>
        </div>
      </div>
    </div>
  );
};
export default Posts;
