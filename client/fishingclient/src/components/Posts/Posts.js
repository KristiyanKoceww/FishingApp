import { React, useState, useContext } from "react";
import { Link } from 'react-router-dom';
import { Button } from "primereact/button";
import Post from "./Post";
import CreatePost from "./CreatePost";
import InfiniteScroll from "react-infinite-scroll-component";
import { ScrollToTop } from '../Scroll/ScrollToTop';
import { UserContext } from '../AcountManagment/UserContext';
import Spinner from "../Helpers/Spinners/Spinner";
import EndOfPosts from '../Helpers/InfoForEndOfPosts/EndOfPosts'

import "./Posts.css";
import "primereact/resources/themes/lara-light-indigo/theme.css";
import "primereact/resources/primereact.min.css";
import "primeicons/primeicons.css";

const Posts = (props) => {
  const [createFormToggle, setCreateFormToggle] = useState(false);
  const { appUser, setAppUser } = useContext(UserContext);

  const toggleForm = () => {
    setCreateFormToggle(!createFormToggle);
  };

  return (
    <div className="container">
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
            endMessage={<EndOfPosts/>}
          >
            {props.posts && props.posts.map((item, index) => {
              return <Post key={index} {...item} />;
            })}
          </InfiniteScroll>
          <ScrollToTop />
        </div>
        : <div className="NoPostsYet">There are no posts yet.Be the first person to create post!</div>}
    </div>
  );
};
export default Posts;
