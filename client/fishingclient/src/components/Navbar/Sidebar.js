import { useContext, useEffect, useState } from 'react';
import styled from 'styled-components'
import { Link } from 'react-router-dom';
import * as FaIcons from 'react-icons/fa';
import * as AiIcons from 'react-icons/ai';
import { SidebarData } from './SidebarData';
import SubMenu from './SubMenu';
import { IconContext } from 'react-icons/lib';
import { UserContext } from '../AcountManagment/UserContext';

const Nav = styled.div`
  background: #15171c;
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
  background: #15171c;
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

  console.log(appUser);



  const showSidebar = () => setSidebar(!sidebar);

  if (Object.keys(appUser).length ===0) {
    return (
      <>
        <IconContext.Provider value={{ color: '#fff' }}>
          <Nav>
            <NavIcon to='#'>
              <FaIcons.FaBars onClick={showSidebar} />
            </NavIcon>
            <h1 className="text-center">Hello anonymous</h1>
            <div>
              <Link to="/Login">Login</Link>
            </div>
            <div>
              <Link to="/Register">Register</Link>
            </div>
          </Nav>
          <SidebarNav sidebar={sidebar}>
            <SidebarWrap>
              <NavIcon to='#'>
                <AiIcons.AiOutlineClose onClick={showSidebar} />
              </NavIcon>
              {SidebarData.map((item, index) => {
                return <SubMenu item={item} key={index} />;
              })}
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
            <h1 className="text-center">Hello {appUser.FirstName}</h1>
            <Link to="/">Home</Link>
            <Link to="/Logout" >Logout</Link>
          </Nav>
          <SidebarNav sidebar={sidebar}>
            <SidebarWrap>
              <NavIcon to='#'>
                <AiIcons.AiOutlineClose onClick={showSidebar} />
              </NavIcon>
              {SidebarData.map((item, index) => {
                return <SubMenu item={item} key={index} />;
              })}
            </SidebarWrap>
          </SidebarNav>
        </IconContext.Provider>
      </>
    );
  }

  // return (
  //   <>
  //     <IconContext.Provider value={{ color: '#fff' }}>
  //       <Nav>
  //         <NavIcon to='#'>
  //           <FaIcons.FaBars onClick={showSidebar} />
  //         </NavIcon>
  //         <h1 className="text-center">Hello {}</h1>
  //       </Nav>
  //       <SidebarNav sidebar={sidebar}>
  //         <SidebarWrap>
  //           <NavIcon to='#'>
  //             <AiIcons.AiOutlineClose onClick={showSidebar} />
  //           </NavIcon>
  //           {SidebarData.map((item, index) => {
  //             return <SubMenu item={item} key={index} />;
  //           })}
  //         </SidebarWrap>
  //       </SidebarNav>
  //     </IconContext.Provider>
  //   </>
  // );
};

export default Sidebar;