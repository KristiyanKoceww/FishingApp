import { useContext, useState } from 'react';
import { UserContext } from '../AcountManagment/UserContext';
import ErrorNotification from '../ErrorsManagment/ErrorNotification';
const Comment = (comment) => {
    const [error, setError] = useState();
    const { appUser, setAppUser } = useContext(UserContext);
    const jwt = localStorage.getItem("jwt");

    const deleteCommentUrl = process.env.REACT_APP_DELETECOMMENT;
    const updateCommentUrl = process.env.REACT_APP_UPDATECOMMENT;

    const deleteComment = (e, id) => {
        e.preventDefault();
        if (window.confirm("Are you sure you want to remove comment?")) {
            fetch(deleteCommentUrl + id,
                {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json",
                        'Authorization': 'Bearer ' + jwt
                    },
                    body: id,
                }).then(r => {
                    if (!r.ok) {
                        throw new Error('Deleting this comment went wrong!')
                    }
                   return r.json();
                })
                .catch(err => setError(err.message))
        }
    }

    const reply = (e, id) => {
        // NEED TO OPEN AGAIN INPUT TEXT FIELD TO INSERT EDITED COMMENT
        e.preventDefault();
    }

    const edit = (e, id) => {
        // NEED TO OPEN AGAIN INPUT TEXT FIELD TO INSERT EDITED COMMENT
        const url = updateCommentUrl;
        const data = {
            commentId: id,
            postId: post.Id,
            userId: userId,
            content: comment,
            parentId: comment.ParentId
        }
    }

    return (
        <div>
            {error && <ErrorNotification message={error} />}
            <div className="post__bubble">
                <strong className="post__user">{comment.user.firstName}:</strong> <div className="post__content">{comment.content}</div>
            </div>
            {Object.keys(appUser ? appUser : {}).length !== 0 ?
                <div className="post__buttons">
                    <button type="submit" onClick={(e) => reply(e, comment.id)} className="button" >Reply</button>
                    <button type="submit" onClick={(e) => edit(e, comment.id)} className="button2">Edit</button>
                    <button type="submit" onClick={(e) => deleteComment(e, comment.id)} className="button3">Delete</button>
                </div> : <br />}
        </div>
    )
}

export default Comment