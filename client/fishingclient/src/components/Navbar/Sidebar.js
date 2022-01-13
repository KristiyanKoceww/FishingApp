import { useContext, useEffect, useState } from 'react';
import styled from 'styled-components'
import { Link } from 'react-router-dom';
import * as FaIcons from 'react-icons/fa';
import * as AiIcons from 'react-icons/ai';
import { SidebarData } from './SidebarData';
import { SidebarDataForUsers, SidebarDataForAdmin } from './SidebarData';
import SubMenu from './SubMenu';
import './Sidebar.css'
import { IconContext } from 'react-icons/lib';
import { UserContext } from '../AcountManagment/UserContext';
import Button from "react-bootstrap/Button";
import AccountCircleIcon from '@mui/icons-material/AccountCircle';

const Nav = styled.div`
  background: #087AEA;
  height: 80px;
  display: flex;
  justify-content: flex-start;
  align-items: center;
`;

const NavIcon = styled(Link)`
  margin-left: 2rem;
  font-size: 2rem;
  height: 80px;
  display: flex;
  justify-content: flex-start;
  align-items: center;
`;

const SidebarNav = styled.nav`
  background: #087AEA;
  width: 250px;
  height: 100vh;
  display: flex;
  justify-content: center;
  position: fixed;
  top: 0;
  left: ${({ sidebar }) => (sidebar ? '0' : '-100%')};
  transition: 350ms;
  z-index: 10;
`;

const SidebarWrap = styled.div`
  width: 100%;
`;

const Sidebar = () => {
  const [sidebar, setSidebar] = useState(false);
  const { appUser, setAppUser } = useContext(UserContext);
  const showSidebar = () => setSidebar(!sidebar);

  if (Object.keys(appUser ? appUser : {}).length === 0) {
    return (
      <>
        <IconContext.Provider value={{ color: '#fff' }}>
          <Nav>
            <h1 className="hello__msg"> <AccountCircleIcon /> Hello, guest</h1>
            <div className="login_center">
              <Link to="/">
                <Button className="home" type="button">Home</Button>
              </Link>
              {" "}
              <Link to="/Login">
                <Button className="login" type="button">Login</Button>
              </Link>
              {" "}
              <Link to="/Register">
                <Button className="register" type="button">Register</Button>
              </Link>
            </div>
          </Nav>
          <SidebarNav sidebar={sidebar}>
            <SidebarWrap>
              <NavIcon to='#'>
                <AiIcons.AiOutlineClose onClick={showSidebar} />
              </NavIcon>
            </SidebarWrap>
          </SidebarNav>
        </IconContext.Provider>
      </>
    );
  }
  else {
    return (
      <>
        <IconContext.Provider value={{ color: '#fff' }}>
          <Nav>
            <NavIcon to='#'>
              <FaIcons.FaBars onClick={showSidebar} />
            </NavIcon>
            <div>
              <h1 className="hello__msg"><AccountCircleIcon /> Hello, {appUser.firstName}</h1>
              <Link to="/UserProfile">
                <img className="imagecircle" src={appUser.mainImageUrl ? appUser.mainImageUrl : null} />
              </Link>
            </div>
            <div className="logout_center">

              <Link to="/">
                <Button className="home" type="button">Home</Button>
              </Link>
              {" "}
              <Link to="/Logout">
                <Button className="logout" type="button">Logout</Button>
              </Link>
            </div>
          </Nav>
          <SidebarNav sidebar={sidebar}>
            <SidebarWrap>
              <NavIcon to='#'>
                <AiIcons.AiOutlineClose onClick={showSidebar} />
              </NavIcon>
              {
                appUser.userName == 'admin' ?
                  SidebarDataForAdmin.map((item, index) => {
                    return <SubMenu item={item} key={index} />;
                  })
                  :
                  SidebarDataForUsers.map((item, index) => {
                    return <SubMenu item={item} key={index} />;
                  })
              }

            </SidebarWrap>
          </SidebarNav>
        </IconContext.Provider>
      </>
    );
  }
};

export default Sidebar;