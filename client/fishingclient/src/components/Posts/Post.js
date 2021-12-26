import { React, useEffect, useState, useContext } from 'react'
import Avatar from '@mui/material/Avatar';
import ImageSlider from '../ImageSlider/ImageSlider';
import Comment from './Comment';
import './Post.css';

import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faThumbsDown } from '@fortawesome/free-solid-svg-icons'
import { faThumbsUp } from '@fortawesome/free-solid-svg-icons'
import FavoriteBorderIcon from '@mui/icons-material/FavoriteBorder';
import { UserContext } from '../AcountManagment/UserContext';

const Post = (post) => {
    const [postComments, setPostComments] = useState([]);
    const [comment, setComment] = useState('');

    const { appUser, setAppUser } = useContext(UserContext);

    const jwt = localStorage.getItem("jwt");
    const userId = localStorage.getItem("userId");

    const upVote = true;
    const downVote = false;

    const convertValue = (string) => {
        switch (string.toLowerCase().trim()) {
            case "true": case "yes": case "1": return true;
            case "false": case "no": case "0": case null: return false;
            default: return Boolean(string);
        }
    }

    const postComment = (event) => {
        event.preventDefault();
        const data = {
            Content: comment,
            UserId: userId,
            PostId: post.Id,
        };

        fetch("https://localhost:44366/api/Comments/create", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                Authorization: "Bearer " + jwt,
            },
            body: JSON.stringify(data),
        }).catch((error) => {
            console.error("Error:", error);
        });

        setComment('');
    };

    const Vote = (e, postId) => {
        e.preventDefault();
        const vote = convertValue(e.target.value);
        const id = postId;

        const data = {
            IsUpVote: vote,
            UserId: userId,
            PostId: id,
        }

        fetch('https://localhost:44366/api/Votes/vote', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + jwt
            },
            body: JSON.stringify(data)
        })
            .catch((error) => {
                console.error('Error:', error);
            });
    }

    useEffect(() => {
        if (post.Id) {
            const url = 'https://localhost:44366/api/Posts/getPostCommentsByPostId/Id?postId='
            fetch(url + post.Id,
                {
                    method: "GET",
                    headers: {
                        "Content-Type": "application/json",
                        'Authorization': 'Bearer ' + jwt
                    },
                })
                .then(r => {
                    if (!r.ok) {
                        throw new Error(`HTTP error ${r.status}`);
                    }
                    return r.json();
                })
                .then(result => setPostComments(result))
                .catch(error => {
                    console.log(error);
                });
        };

    }, [post.Id])

    return (

        <div className="post">
            <div className="post__header">
                <Avatar
                    className="post__avatar"
                    alt="avatar"
                    src={post.User.MainImageUrl}
                />
                <h3 className="username">{post.User.FirstName}</h3>

            </div>
            <h1 className="title">{post.Title}</h1>
            <p className="content">{post.Content}</p>
            <div className="slider">
                {typeof post.ImageUrls != "undefined" ? <ImageSlider slides={post.ImageUrls} /> : ""}
            </div>

            <div className="likescount">
                <FavoriteBorderIcon /> {post.Votes.filter(vote => vote.Type === 1).length} likes
            </div>

            {Object.keys(appUser ? appUser : {}).length !== 0 ?
                <div className="post__likebuttons">
                    <div className="post__like">
                        <FontAwesomeIcon icon={faThumbsUp} size="2x" />
                        &nbsp;&nbsp;
                        <button onClick={(e) => Vote(e, post.Id)} value={upVote} className="btn__like">Like</button>
                    </div>
                    <div className="post__unlike">
                        <FontAwesomeIcon icon={faThumbsDown} size="2x" />
                        &nbsp;&nbsp;
                        <button onClick={(e) => Vote(e, post.Id)} value={downVote} className="btn__unlike">Dislike</button>
                    </div>
                </div>
                : null}

            <div className="post__comments">
                {postComments.map((item, index) => {
                    return <Comment key={index} {...item} />;
                })}
            </div>

            {Object.keys(appUser ? appUser : {}).length !== 0 ?
                <form className="post__commentBox">
                    <input
                        className="post__input"
                        type="text"
                        placeholder="Add a comment..."
                        value={comment}
                        onChange={(e) => setComment(e.target.value)}
                    />
                    <button
                        className="post__button"
                        disabled={!comment}
                        type="submit"
                        onClick={postComment}
                    >Submit</button>
                </form>
                : null}
        </div>
    )
}
export default Post;