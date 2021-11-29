import { useEffect, useState } from 'react';
import styled from 'styled-components'
import { Link } from 'react-router-dom';
import * as FaIcons from 'react-icons/fa';
import * as AiIcons from 'react-icons/ai';
import { SidebarData } from './SidebarData';
import SubMenu from './SubMenu';
import { IconContext } from 'react-icons/lib';


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
  const [name, setName] = useState('user');
  const [user,setUser] = useState([]);

  const showSidebar = () => setSidebar(!sidebar);

  // if (name === '' || name === undefined) {
  //   return (
  //     <>
  //       <IconContext.Provider value={{ color: '#fff' }}>
  //         <Nav>
  //           <NavIcon to='#'>
  //             <FaIcons.FaBars onClick={showSidebar} />
  //           </NavIcon>
  //           <h1 className="text-center">Hello anonymous</h1>
  //           <div>
  //             <Link to="/Login">Login</Link>
  //           </div>
  //           <div>
  //             <Link to="/Register">Register</Link>
  //           </div>
  //         </Nav>
  //         <SidebarNav sidebar={sidebar}>
  //           <SidebarWrap>
  //             <NavIcon to='#'>
  //               <AiIcons.AiOutlineClose onClick={showSidebar} />
  //             </NavIcon>
  //             {SidebarData.map((item, index) => {
  //               return <SubMenu item={item} key={index} />;
  //             })}
  //           </SidebarWrap>
  //         </SidebarNav>
  //       </IconContext.Provider>
  //     </>
  //   );
  // }
  // else {
  //   return (
  //     <>
  //       <IconContext.Provider value={{ color: '#fff' }}>
  //         <Nav>
  //           <NavIcon to='#'>
  //             <FaIcons.FaBars onClick={showSidebar} />
  //           </NavIcon>
  //           <h1 className="text-center">Hello {name}</h1>
  //           <Link to="/Logout" onClick={logout}>Logout</Link>
  //         </Nav>
  //         <SidebarNav sidebar={sidebar}>
  //           <SidebarWrap>
  //             <NavIcon to='#'>
  //               <AiIcons.AiOutlineClose onClick={showSidebar} />
  //             </NavIcon>
  //             {SidebarData.map((item, index) => {
  //               return <SubMenu item={item} key={index} />;
  //             })}
  //           </SidebarWrap>
  //         </SidebarNav>
  //       </IconContext.Provider>
  //     </>
  //   );
  // }
  
//   useEffect(() => {
//     const userId = localStorage.getItem("userId");
//     const jwt = localStorage.getItem("jwt");
//     const url = "https://localhost:44366/api/AppUsers/getUser/id?userId=";

//     fetch(url + userId,
//       {
//         method: "GET",
//         headers: {
//           "Content-Type": "application/json",
//           'Authorization': 'Bearer ' + jwt
//         },
//       }
//     ).then(r => r.json()).then(result => setUser(result))
// console.log(user);
//   }, []);

  return (
    <>
      <IconContext.Provider value={{ color: '#fff' }}>
        <Nav>
          <NavIcon to='#'>
            <FaIcons.FaBars onClick={showSidebar} />
          </NavIcon>
          <h1 className="text-center">Hello {name}</h1>
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
};

export default Sidebar;