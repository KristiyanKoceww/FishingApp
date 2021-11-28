import { React, useEffect, useState } from 'react'
import Avatar from '@mui/material/Avatar';
import ImageSlider from '../ImageSlider/ImageSlider';
import './Post.css';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faThumbsDown } from '@fortawesome/free-solid-svg-icons'
import { faThumbsUp } from '@fortawesome/free-solid-svg-icons'


const Post = ({ postId, keyToAppend, username, title, content, images, avatarImage }) => {
    const [comments, setComments] = useState([]);
    const [comment, setComment] = useState('');

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
        const userId = localStorage.getItem("userId");

        const data = {
            Content: comment,
            UserId: userId,
            PostId: postId,
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

    const reply = (e, id) => {
        // NEED TO OPEN AGAIN INPUT TEXT FIELD TO INSERT EDITED COMMENT
        e.preventDefault();
    }

    const edit = (e, id) => {
        e.preventDefault();

        // NEED TO OPEN AGAIN INPUT TEXT FIELD TO INSERT EDITED COMMENT
        const url = 'https://localhost:44366/api/Comments/update?commentId='
        const data = {
            commentId: id,
            postId: postId,
            userId: userId,
            content: comment,
            parentId: comment.ParentId
        }

        fetch(url + id,
            {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                    'Authorization': 'Bearer ' + jwt
                },
                body: json.stringify(data),
            })
    }

    const deleteComment = (e, id) => {
        e.preventDefault();

        if (window.confirm("Are you sure you want to remove comment?")) {
            const url = 'https://localhost:44366/api/Comments/delete?commentId='
            fetch(url + id,
                {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json",
                        'Authorization': 'Bearer ' + jwt
                    },
                    body: id,
                })
        }
    }

    // useEffect(() => {
    //     if (postId) {
    //         const url = 'https://localhost:44366/api/Posts/getPostCommentsByPostId/Id?postId='
    //         fetch(url + postId,
    //             {
    //                 method: "GET",
    //                 headers: {
    //                     "Content-Type": "application/json",
    //                     'Authorization': 'Bearer ' + jwt
    //                 },
    //             })
    //             .then(r => {
    //                 if (!r.ok) {
    //                     throw new Error(`HTTP error ${r.status}`);
    //                 }
    //                 return r.json();
    //             })
    //             .then(result => setComments(result))
    //             .catch(error => {

    //             });
    //     };

    // }, [postId])

    return (
        
        <div className="post" key={keyToAppend}>
            <div className="post__header">
                <Avatar
                    className="post__avatar"
                    alt="avatar"
                    src={avatarImage}
                />
                <h3 className="username">{username}</h3>

            </div>
            <h1 className="title">{title}</h1>
            <p className="content">{content}</p>
            <div className="slider">
                {typeof images != "undefined" ? <ImageSlider slides={images} /> : ""}
            </div>

            <div className="post__likebuttons">
                <div className="post__like">
                    <FontAwesomeIcon icon={faThumbsUp} size="2x" />
                    &nbsp;&nbsp;
                    <button onClick={(e) => Vote(e, postId)} value={upVote} className="btn__like">Like</button>
                </div>
                <div className="post__unlike">
                    <FontAwesomeIcon icon={faThumbsDown} size="2x" />
                    &nbsp;&nbsp;
                    <button onClick={(e) => Vote(e, postId)} value={downVote} className="btn__unlike">Dislike</button>
                </div>
            </div>

            {/* <div className="post__comments">

                {comments.map((comment) =>
                    <div>
                        <div className="post__bubble">
                            <strong className="post__user">{comment.User.FirstName}:</strong> <div className="post__content">{comment.Content}</div>
                        </div>
                        <div className="post__buttons">
                            <button type="submit" onClick={(e) => reply(e, comment.Id)} className="button" >Reply</button>
                            <button type="submit" onClick={(e) => edit(e, comment.Id)} className="button2">Edit</button>
                            <button type="submit" onClick={(e) => deleteComment(e, comment.Id)} className="button3">Delete</button>
                        </div>
                    </div>
                )}
            </div> */}

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
        </div>
    )
}


export default Post;