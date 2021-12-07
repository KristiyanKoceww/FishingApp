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
        <a to="/">
          <Button className="home" type="button">Home</Button>
        </a>
        {" "}
        <a to="/Pricacy">
          <Button className="home" type="button">Privacy</Button>
        </a>
        {" "}
        <a to="/Aboutus">
          <Button className="home" type="button">About us</Button>
        </a>
        {" "}
        <a to="/Details">
          <Button className="home" type="button">Details</Button>
        </a>
      </div>
    </footer>
  )
}

export default Footer;
