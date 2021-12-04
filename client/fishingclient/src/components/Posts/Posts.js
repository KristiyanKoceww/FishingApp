import { React, useState } from "react";
import { Button } from "primereact/button";
import Post from "./Post";
import CreatePost from "./CreatePost";
import InfiniteScroll from "react-infinite-scroll-component";
import "./Posts.css";

import "primereact/resources/themes/lara-light-indigo/theme.css";
import "primereact/resources/primereact.min.css";
import "primeicons/primeicons.css";

const Posts = (props) => {
  const [createFormToggle, setCreateFormToggle] = useState(false);

  const toggleForm = () => {
    setCreateFormToggle(!createFormToggle);
  };

  return (
    <div className="container">
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

      <div className="infiniteScroll">
        <InfiniteScroll
          dataLength={props.posts.length}
          next={props.fetchData}
          hasMore={props.hasMore}
          loader={<h4>Loading...</h4>}
          endMessage={
            <p style={{ textAlign: "center" }}>
              <b>Yay! You have seen it all</b>
            </p>
          }
        >
          {props.posts.map((item) => {
            return <Post {...item} />;
          })}
        </InfiniteScroll>
      </div>
    </div>
  );
};
export default Posts;
