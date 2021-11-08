import "./styles/profileIcon.scss";

const ProfileIcon = (props) => {
  const { iconSize, image } = props;

  // let profileImage = image
  //   ? image
  //   : `https://i.pravatar.cc/150?img=${randomId}`;

  return (
      <img
        className={`profileIcon ${iconSize}`}
        src={image}
        alt="profile"
      />
  );
}
export default ProfileIcon;
