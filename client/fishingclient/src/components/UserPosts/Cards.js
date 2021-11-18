import "./styles/cards.scss";
import Card from "./Card";
import React, { useEffect, useState, useMemo } from "react";
const Cards = () => {

  const [posts, setPosts] = useState([]);
  
  useEffect(() => {
    const jwt = localStorage.getItem("jwt");
    (async () => {
      const response = await fetch('https://localhost:44366/api/Posts/getAllPosts',
      {
               method: "GET",
               headers: {
                 "Content-Type": "application/json" ,
                 'Authorization': 'Bearer ' + jwt
                },
            }
      )
      const content = await response.json();
      setPosts(content);
    })()
  }, []);

  
  // const renderPosts = useMemo(() => {
    return (
      <div className="cards">
        <h1 className='text-center'>Публикации на потребители:</h1>
        {posts.map((post) => {
          return (
            <Card
              id={post.Id}
              accountName={post.User.FirstName}
              profilePicture={post.User.MainImageUrl}
              image={post.ImageUrls}
              comments={post.Comments}
              likedByNumber={post.Votes.length}
              hours={post.CreatedOn}
              content={post.Content}
              title={post.Title}
              vote = {post.Votes}
            />
          )
        })}
      </div>
    )
  // }, [posts])

  // return (
  //   <div>
  //     {renderPosts}
  //   </div>
  // )
}
export default Cards;
