import Sidebar from '../Navbar/Sidebar';
import './Header.css'

const Header = () => {
    var today = new Date();
    var date = today.getFullYear() + '-' + (today.getMonth() + 1) + '-' + today.getDate();
    return (
        <div className="header">
            <div className="EmailAndDate">koceww@gmail.com {date}</div>
            <Sidebar />
        </div>
    )
}

export default Header;