import "./styles/cards.scss";
import Card from "./Card";
import React, { useEffect, useState, useMemo } from "react";
import { propTypes } from "react-bootstrap/esm/Image";

const Cards = () => {

  const [posts, setPosts] = useState([]);

  useEffect(() => {
    (async () => {
      const response = await fetch('https://localhost:44366/api/Posts/getAllPosts',
      )
      const content = await response.json();
      setPosts(content);

      console.log(content);
    })()
  }, []);

  const renderPosts = useMemo(() => {

    return (
      <div className="cards">

        {posts.map((post) => {

          return(
            <Card
            accountName={post.User.FirstName}
            storyBorder={true}
            image={post.ImageUrls[0].ImageUrl}
            comments={post.Comments}
            likedByText="dadatlacak"
            likedByNumber={post.Votes.Count}
            hours={post.CreatedOn}
            content = {post.Content}
            title = {post.Title}
          />
          )
         
        })}
      </div>
    )
  }, [posts])



  return (
    <div>
      {renderPosts}
    </div>
  )
  // const commentsOne = [
  //   {
  //     user: "raffagrassetti",
  //     text: "Woah dude, this is awesome! 🔥",
  //     id: 1,
  //   },
  //   {
  //     user: "therealadamsavage",
  //     text: "Like!",
  //     id: 2,
  //   },
  //   {
  //     user: "mapvault",
  //     text: "Niceeeee!",
  //     id: 3,
  //   },
  // ];

  // const commentsTwo = [
  //   {
  //     user: "mapvault",
  //     text: "Amazing content, keep it up!",
  //     id: 4,
  //   },
  // ];

  // const commentsThree = [
  //   {
  //     user: "dadatlacak",
  //     text: "Love this!",
  //     id: 5,
  //   },
  // ];

  // return (
  //   <div className="cards">

  //     <Card
  //       accountName="rafagrassetti"
  //       storyBorder={true}
  //       image="https://picsum.photos/800/900"
  //       comments={commentsOne}
  //       likedByText="dadatlacak"
  //       likedByNumber={89}
  //       hours={16}
  //     />
  //     <Card
  //       accountName="mapvault"
  //       image="https://picsum.photos/800"
  //       comments={commentsTwo}
  //       likedByText="therealadamsavage"
  //       likedByNumber={47}
  //       hours={12}
  //     />
  //     <Card
  //       accountName="dadatlacak"
  //       storyBorder={true}
  //       image="https://picsum.photos/800/1000"
  //       comments={commentsThree}
  //       likedByText="mapvault"
  //       likedByNumber={90}
  //       hours={4}
  //     />
  //   </div>
  // );
}

export default Cards;
