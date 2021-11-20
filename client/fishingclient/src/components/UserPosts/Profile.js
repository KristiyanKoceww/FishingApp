import "./styles/profile.scss";
import ProfileIcon from "./ProfileIcon";

const Profile = (props) => {
  const {
    caption,
    iconSize,
    captionSize,
    hideAccountName,
    image,
    accountName
  } = props;

  return (
    <div className="profile">
      <ProfileIcon
        iconSize={iconSize}
        image={image}
      />
      {(accountName || caption) && !hideAccountName && (
        <div className="textContainer">
          <span className="accountName">{accountName}</span>
          <span className={`caption ${captionSize}`}>{caption}</span>
        </div>
      )}
    </div>
  );
}

export default Profile;
