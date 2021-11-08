import "./styles/card.scss";
import Profile from "./Profile";
import CardMenu from "./CardMenu";
import Comment from "./Comment";
import ImageSlider from "../ImageSlider/ImageSlider";
import React from "react";
import { useState } from "react";

const Card = (props) => {
  const [text, setText] = useState("");
  const {
    profilePicture,
    image,
    comments,
    likedByText,
    likedByNumber,
    hours,
    content,
    title,
    accountName,
  } = props;

  const submitComment = (e) => {
    e.preventDefault();
    const userId = '161b2a1d-e05b-41db-8423-542d6afc706b';
    const postId = 13;

    const data = {
      Content: text,
      UserId: userId,
      PostId: postId,
    }

    fetch('https://localhost:44366/api/Comments/create', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(data)
    })
      .catch((error) => {
        console.error('Error:', error);
      });
  }


  return (
    <div className="card">
      <header>
        <Profile iconSize="big" image={profilePicture} accountName={accountName} />
      </header>
      <p className='text-center'>{title}</p>
      <p className='text-center'>  {content}</p>
      <ImageSlider slides={image} />

      <CardMenu />
      <div className="likedBy">
        <Profile iconSize="small" image={profilePicture} />
        <span>
          Харесано от <strong>{likedByText}</strong> и{" "}
          <strong>{comments.Lenght} 50 други</strong>
        </span>
      </div>
      <div className="comments">
        {comments.map((comment) => {
          return (
            <Comment
              key={comment.id}
              accountName={comment.User.FirstName ? comment.User.FirstName : null}
              comment={comment.Content}
            />
          );
        })}
      </div>
      <div className="timePosted">Преди {hours} часа.</div>
      <form onSubmit={submitComment}> 
      <div className="addComment">
        <textarea type="text" value={text} placeholder="Напишете коментар" className="commentText" onChange={(e) => setText(e.target.value)} />
        <button className="btn btn-primary"  type="submit"  >Публикувай</button>
      </div>
      </form>
    </div>
  );
}

export default Card;
