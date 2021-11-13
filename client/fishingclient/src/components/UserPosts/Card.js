import "./styles/card.scss";
import Profile from "./Profile";
import CardMenu from "./CardMenu";
import Comment from "./Comment";
import ImageSlider from "../ImageSlider/ImageSlider";
import React from "react";
import { useState, useEffect } from "react";

const Card = (props) => {
  const [text, setText] = useState("");
  const [showMore, setShowMore] = useState(false);
  const [userId, setUserId] = useState();

  const {
    id,
    key,
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

  const submitComment = (e, id) => {
    e.preventDefault();

    const jwt = localStorage.getItem("jwt");
    const fetchUrl = `https://localhost:44366/api/AppUsers/user`;

    const fetchData = () => {
      fetch((fetchUrl),
        {
          method: "POST",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify(jwt),
        })
        .then((res) => res.json())
        .then((result) => setUserId(result))
        .catch((err) => {
          console.log(err);
        });
    };

    fetchData();

    const postId = id;
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

    setText('');
  }
  return (
    <div className="card" key={id}>
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
        {comments?.slice(0, 5).map((comment) => (

          <Comment
            key={comment.id}
            accountName={comment.User.FirstName ? comment.User.FirstName : null}
            comment={comment.Content}
          />
        )
        )}

        {showMore && comments?.slice(5).map((comment) => (
          <Comment
            key={comment.id}
            accountName={comment.User.FirstName ? comment.User.FirstName : null}
            comment={comment.Content}
          />
        ))}

        <button type="button" className="button" onClick={() => setShowMore(true)}>Show more comments</button>
      </div>
      <div className="timePosted">Преди {(new Date().getHours(hours))} часа.</div>
      <div className="timePosted">Преди {hours} часа.</div>
      <form data={id} onSubmit={e => submitComment(e, id)}>
        <div className="addComment">
          <textarea type="text" value={text} placeholder="Напишете коментар" className="commentText" onChange={(e) => setText(e.target.value)} />
          <button className="btn btn-primary" type="submit"  >Публикувай</button>
        </div>
      </form>
    </div>
  );
}

export default Card;
