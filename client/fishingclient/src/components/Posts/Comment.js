import { useContext } from 'react';
import { UserContext } from '../AcountManagment/UserContext';

const Comment = (comment) => {
    const { appUser, setAppUser } = useContext(UserContext);
    const jwt = localStorage.getItem("jwt");

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

    const reply = (e, id) => {
        // NEED TO OPEN AGAIN INPUT TEXT FIELD TO INSERT EDITED COMMENT
        e.preventDefault();
    }

    const edit = (e, id) => {
        // NEED TO OPEN AGAIN INPUT TEXT FIELD TO INSERT EDITED COMMENT
        const url = 'https://localhost:44366/api/Comments/update?commentId='
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
                <strong className="post__user">{comment.User.FirstName}:</strong> <div className="post__content">{comment.Content}</div>
            </div>
            {Object.keys(appUser ? appUser : {}).length !== 0 ?
                <div className="post__buttons">
                    <button type="submit" onClick={(e) => reply(e, comment.Id)} className="button" >Reply</button>
                    <button type="submit" onClick={(e) => edit(e, comment.Id)} className="button2">Edit</button>
                    <button type="submit" onClick={(e) => deleteComment(e, comment.Id)} className="button3">Delete</button>
                </div> : <br />}
        </div>
    )
}

export default Comment