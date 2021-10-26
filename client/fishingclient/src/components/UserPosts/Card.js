import "./styles/card.scss";
import Profile from "./Profile";
import CardMenu from "./CardMenu";
import Comment from "./Comment";
import ImageSlider from "../ImageSlider/ImageSlider";

const Card = (props) => {
  const {
    storyBorder,
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

  return (
    <div className="card">
      <header>
        <Profile iconSize="medium" image={profilePicture} storyBorder={storyBorder} accountName={accountName} />
      </header>
      <p className='text-center'>{title}</p>
      <p className='text-center'>  {content}</p>
      <ImageSlider slides={image} />

      <CardMenu />
      <div className="likedBy">
        <Profile iconSize="small" hideAccountName={true} />
        <span>
          Liked by <strong>{likedByText}</strong> and{" "}
          <strong>{likedByNumber} others</strong>
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
      <div className="timePosted">{hours} HOURS AGO</div>
      <div className="addComment">
        <div className="commentText">Add a comment...</div>
        <div className="postText">Post</div>
      </div>
    </div>
  );
}

export default Card;
