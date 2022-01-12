import { React, useEffect, useState, useContext } from 'react'
import './Post.css';
import Comment from './Comment';
import Avatar from '@mui/material/Avatar';
import ImageSlider from '../ImageSlider/ImageSlider';
import ErrorNotification from "../ErrorsManagment/ErrorNotification";

import { UserContext } from '../AcountManagment/UserContext';
import { faThumbsUp } from '@fortawesome/free-solid-svg-icons'
import { faThumbsDown } from '@fortawesome/free-solid-svg-icons'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import FavoriteBorderIcon from '@mui/icons-material/FavoriteBorder';

const Post = (post) => {
    const [postComments, setPostComments] = useState([]);
    const [comment, setComment] = useState('');
    const [newComment, setNewComment] = useState('');
    const [votes, setVotes] = useState();
    const [newVote, setNewVote] = useState();
    const [error, setError] = useState();

    const { appUser, setAppUser } = useContext(UserContext);
    const jwt = localStorage.getItem("jwt");

    const createPostCommentUrl = process.env.REACT_APP_CREATECOMMENT;
    const getPostCommentsUrl = process.env.REACT_APP_GETPOSTCOMMENTS;
    const createPostVoteUrl = process.env.REACT_APP_CREATEVOTE;
    const getPostVotesUrl = process.env.REACT_APP_GETPOSTVOTES;

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
            UserId: appUser.id,
            PostId: post.id,
        };

        fetch(createPostCommentUrl, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                Authorization: "Bearer " + jwt,
            },
            body: JSON.stringify(data),
        }).then(r => {
            if (!r.ok) {
                // throw new Error('Creating new post comment failed!')
                window.alert('Creating new post comment failed!')
            }
            setNewComment(comment);
            setComment('');
        }).catch(err => setError(err.message))
    };

    const Vote = (e, postId) => {
        e.preventDefault();
        const vote = convertValue(e.target.value);
        const id = postId;

        const data = {
            IsUpVote: vote,
            UserId: appUser.id,
            PostId: id,
        }

        fetch(createPostVoteUrl, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + jwt
            },
            body: JSON.stringify(data)
        }).then(r => {
            if (!r.ok) {
                // throw new Error('Creating new vote failed!')
                window.alert('Creating new vote failed!')
            }
            setNewVote(true);
        }).catch(err => setError(err.message))
    }

    useEffect(() => {
        if (post.id) {
            const url = getPostVotesUrl
            fetch(url + post.id,
                {
                    method: "GET",
                    headers: {
                        "Content-Type": "application/json",
                        'Authorization': 'Bearer ' + jwt
                    },
                })
                .then(r => {
                    if (!r.ok) {
                        // throw new Error('Getting post votes failed!')
                        window.alert('Getting post votes failed!')
                    }
                    return r.json();
                })
                .then(result => {
                    setVotes(result)
                })
                .catch(err => setError(err.message))
        };

    }, [post.id]);

    useEffect(() => {
        if (newVote) {
            const url = getPostVotesUrl
            fetch(url + post.id,
                {
                    method: "GET",
                    headers: {
                        "Content-Type": "application/json",
                        'Authorization': 'Bearer ' + jwt
                    },
                })
                .then(r => {
                    if (!r.ok) {
                        // throw new Error('Getting post votes failed!')
                        window.alert('Getting post votes failed!')
                    }
                    return r.json();
                })
                .then(result => {
                    setVotes(result)
                    setNewVote(false);
                })
                .catch(err => setError(err.message))
        };

    }, [newVote]);

    useEffect(() => {
        if (newComment != "") {
            const url = getPostCommentsUrl
            fetch(url + post.id,
                {
                    method: "GET",
                    headers: {
                        "Content-Type": "application/json",
                        'Authorization': 'Bearer ' + jwt
                    },
                })
                .then(r => {
                    if (!r.ok) {
                        // throw new Error('Getting post comments failed!')
                        //window.alert('Getting post comments failed!')
                    }
                    return r.json();
                })
                .then(result => {
                    setPostComments(result)
                    setNewComment("");
                })
                .catch(err => setError(err.message))
        };
    }, [newComment]);

    useEffect(() => {
        if (post.id) {
            const url = getPostCommentsUrl
            fetch(url + post.id,
                {
                    method: "GET",
                    headers: {
                        "Content-Type": "application/json",
                        'Authorization': 'Bearer ' + jwt
                    },
                })
                .then(r => {
                    if (!r.ok) {
                        // throw new Error('Getting post comments failed!')
                        //window.alert('Getting post comments failed!')
                    }
                    return r.json();
                })
                .then(result => {
                    setPostComments(result)
                })
                .catch(err => setError(err.message))
        };
    }, [post.Id])

    return (
        <div className="post">
            <div className="post__header">
                <Avatar
                    className="post__avatar"
                    alt="avatar"
                    src={post.user.mainImageUrl}
                />
                <h3 className="username">{post.user.firstName}</h3>

            </div>
            <h1 className="title">{post.title}</h1>
            <p className="content">{post.content}</p>
            <div className="slider">
                {typeof post.imageUrls != "undefined" ? <ImageSlider slides={post.imageUrls} /> : ""}
            </div>

            <div className="likescount">
                <FavoriteBorderIcon /> {votes} likes
            </div>

            {Object.keys(appUser ? appUser : {}).length !== 0 ?
                <div className="post__likebuttons">
                    <div className="post__like">
                        <FontAwesomeIcon icon={faThumbsUp} size="2x" />
                        &nbsp;&nbsp;
                        <button onClick={(e) => Vote(e, post.id)} value={upVote} className="btn__like">Like</button>
                    </div>
                    <div className="post__unlike">
                        <FontAwesomeIcon icon={faThumbsDown} size="2x" />
                        &nbsp;&nbsp;
                        <button onClick={(e) => Vote(e, post.id)} value={downVote} className="btn__unlike">Dislike</button>
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