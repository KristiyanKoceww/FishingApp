import "./styles/cardMenu.scss";
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faThumbsDown } from '@fortawesome/free-solid-svg-icons'
import { faThumbsUp } from '@fortawesome/free-solid-svg-icons'

const CardMenu = (postId) => {
  let upVote = true;
  let downVote = false;
  const userId = localStorage.getItem("userId");

  const convertValue = (string) => {
    switch (string.toLowerCase().trim()) {
      case "true": case "yes": case "1": return true;
      case "false": case "no": case "0": case null: return false;
      default: return Boolean(string);
    }
  }

  const Vote = (e, postId) => {
    e.preventDefault();
    const vote = convertValue(e.target.value);
    const id = postId.postId;
    const jwt = localStorage.getItem("jwt");
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
  return (
    <div className="grid-container">
      <div>
        <FontAwesomeIcon icon={faThumbsUp} size="2x" />
        &nbsp;&nbsp;
        <button onClick={(e) => Vote(e, postId)} value={upVote} className="btn btn-primary">Харесвам</button>
      </div>
      <div>
        <FontAwesomeIcon icon={faThumbsDown} size="2x" />
        &nbsp;&nbsp;
        <button onClick={(e) => Vote(e, postId)} value={downVote} className="btn btn-danger">Не харесвам</button>
      </div>
    </div>
  );
}
export default CardMenu;
