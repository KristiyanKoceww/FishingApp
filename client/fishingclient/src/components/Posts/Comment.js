import { useContext } from 'react';
import { UserContext } from '../AcountManagment/UserContext';

const Comment = (comment) => {
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
                })
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