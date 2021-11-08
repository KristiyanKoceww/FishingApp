import "./styles/cardMenu.scss";
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faThumbsDown } from '@fortawesome/free-solid-svg-icons'
import { faThumbsUp } from '@fortawesome/free-solid-svg-icons'
import { faCommentAlt } from '@fortawesome/free-solid-svg-icons'

const CardMenu = () => {
  let upVote = true;
  let downVote= false;

  const convertValue = (string) =>{
    switch(string.toLowerCase().trim()){
        case "true": case "yes": case "1": return true;
        case "false": case "no": case "0": case null: return false;
        default: return Boolean(string);
    }
}
  
  const Vote = (e) => {
    e.preventDefault();
    const vote = convertValue(e.target.value);
    const userId = '161b2a1d-e05b-41db-8423-542d6afc706b';
    const postId = 25;

    const data = {
      IsUpVote: vote,
      UserId: userId,
      PostId: postId
    }

    fetch('https://localhost:44366/api/Votes/vote', {
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
    <div className="grid-container">
      <div>
        <FontAwesomeIcon icon={faThumbsUp} size="2x" />
        &nbsp;&nbsp;
        <button onClick={Vote} value= {upVote} className="btn btn-primary">Харесвам</button>
      </div>
      <div>
        <FontAwesomeIcon icon={faThumbsDown} size="2x" />
        &nbsp;&nbsp;
        <button onClick={Vote} value= {downVote} className="btn btn-danger">Не харесвам</button>
      </div>
      {/* <div>
        <FontAwesomeIcon icon={faCommentAlt} size="2x" />
        &nbsp;
        <button className="btn btn-warning">Коментиране</button>
      </div> */}
    </div>
  );
}
export default CardMenu;
