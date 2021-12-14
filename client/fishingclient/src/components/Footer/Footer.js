import "./footer.css";
import { Link } from 'react-router-dom';
import Button from "react-bootstrap/Button";

const Footer = () => {
  var today = new Date();
  var date = today.getFullYear() + '-' + (today.getMonth() + 1) + '-' + today.getDate();

  return (
    <footer className="footer">
      <div className="info">
        <div>Author: Kristiyan Kotsev
          <div>koceww@gmail.com</div>
          <div>{date}</div>
        </div>
      </div>
      <div className="links">
        <Link to="/">
          <Button className="home" type="button">Home</Button>
        </Link>
        {" "}
        <Link to="/Privacy">
          <Button className="home" type="button">Privacy</Button>
        </Link>
        {" "}
        <Link to="/Aboutus">
          <Button className="home" type="button">About us</Button>
        </Link>
        {" "}
        <Link to="/Details">
          <Button className="home" type="button">Details</Button>
        </Link>
      </div>
    </footer>
  )
}

export default Footer;
