import "./styles/cards.scss";
import Card from "./Card";
import React, { useEffect, useState, useMemo } from "react";
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
            profilePicture={post.User.MainImageUrl}
            storyBorder={true}
            image={post.ImageUrls}
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
}
export default Cards;
