import './App.css';

import { BrowserRouter as Router, Switch, Route, useHistory } from 'react-router-dom';
import { useState, useEffect, useMemo } from 'react';

import Header from './components/Header/Header';
import Footer from './components/Footer/Footer';

import Sidebar from './components/Navbar/Sidebar';

import CreateKnot from './components/Knot/CreateKnot'
import AllKnots from './components/Knot/AllKnots'
import KnotInfoPage from './components/Knot/KnotInfoPage';

import CreateCountry from './components/Country/Country';

import Login from './components/AcountManagment/Login/Login';
import Logout from './components/AcountManagment/Logout';
import Register from './components/AcountManagment/Register/Register';

import CreatePost from './components/Posts/CreatePost';
import Posts from './components/Posts/Posts'

import FishInfo from './components/Fish/FishInfo';
import DisplayAllFish from './components/Fish/DisplayAllFish';
import FishInfoPage from './components/Fish/FishInfoPage';

import GetUserById from './components/AcountManagment/GetUserById';
import DeleteUser from './components/AcountManagment/DeleteUser';
import UserDetails from './components/AcountManagment/UserDetails';

import CreateReservoir from './components/Reservoir/CreateReservoir';
import AllReservoirs from './components/Reservoir/AllReservoirs'
import ReservoirInfoPage from './components/Reservoir/ReservoirInfoPage';

import Weather from './components/WeatherForecast/CurrentDayWeatherForecast/Weather';
import FiveDaysWeatherForecast from './components/WeatherForecast/FiveDaysWeatherForecast/FiveDaysWeatherForecast'

import ProtectedRoute from './components/AcountManagment/ProtectedRoute';
import Privacy from './components/PrivacyPolicy/Privacy'
import FacebookLoginUser from './components/Facebooklogin/FacebookLogin'

import Error from './Error'

import IdleMonitor from './components/AcountManagment/SessionManagment/ExtendSession'

import { UserContext } from './components/AcountManagment/UserContext';

function App() {
  const [posts, setPosts] = useState([]);
  const [appUser, setAppUser] = useState({});
  const [page, setpage] = useState(1);
  const [hasMore, sethasMore] = useState(true);

  const jwt = localStorage.getItem("jwt");

  const isAuthenticated = Object.keys(appUser ? appUser : {}).length !== 0;
  const value = useMemo(() => ({ appUser, setAppUser }), [appUser, setAppUser]);


  const updatePosts = (post) => {
    let newState = [];
    newState.unshift(post, ...posts);
    setPosts(newState);
  }

  useEffect(() => {
    const userId = localStorage.getItem("userId");
    const jwt = localStorage.getItem("jwt");
    if (userId && jwt) {
      fetch(
        `https://localhost:44366/api/AppUsers/getUser/id?userId=${userId}`,
        {
          method: "GET",
          headers: {
            "Content-Type": "application/json",
            'Authorization': 'Bearer ' + jwt
          },
        }
      )
        .then(r => r.json())
        .then(result => {
          setAppUser(result)
        });
    }
  }, []);

  useEffect(() => {
    const getPosts = async () => {
      const res = await fetch(
        `https://localhost:44366/api/Posts/GetPosts?id=0`,
        {
          method: "GET",
          headers: {
            "Content-Type": "application/json",
            'Authorization': 'Bearer ' + jwt
          },
        }
      );
      const data = await res.json();
      setPosts(data);
    };

    getPosts();
  }, []);

  const fetchPosts = async () => {
    const res = await fetch(
      `https://localhost:44366/api/Posts/GetPosts?id=${page}`,
      {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
          'Authorization': 'Bearer ' + jwt
        },
      }
    );
    const data = await res.json();
    return data;
  };

  const fetchData = async () => {
    const postsFromServer = await fetchPosts();

    setPosts([...posts, ...postsFromServer]);

    if (postsFromServer.length === 0 || postsFromServer.length < 1) {
      sethasMore(false);
    }
    setpage(page + 1);
  };



  
  return (
    <UserContext.Provider value={value}>
      <div >
        <Router>
          <Header />
          <main className="App">
            <Switch>
              {
                posts ? <Route path='/' exact render={() => <Posts posts={posts} updatePosts={updatePosts} hasMore={hasMore} fetchData={fetchData} />} /> : <div>Loading...</div>
              }
              <ProtectedRoute path='/CreateKnot' component={CreateKnot} auth={isAuthenticated} />
              <ProtectedRoute path='/AllKnots' component={AllKnots} auth={isAuthenticated} />
              <ProtectedRoute path='/KnotInfoPage/:id' component={KnotInfoPage} auth={isAuthenticated} />

              <ProtectedRoute path="/CreateCountry" component={CreateCountry} auth={isAuthenticated} />

              <Route path='/CreatePost' render={() => <CreatePost onChange={updatePosts} />} />

              <ProtectedRoute path='/CreateReservoir' component={CreateReservoir} auth={isAuthenticated} />
              <ProtectedRoute path='/AllReservoirs' component={AllReservoirs} auth={isAuthenticated} />
              <ProtectedRoute path='/ReservoirInfoPage/:id' component={ReservoirInfoPage} auth={isAuthenticated} />

              <Route path='/Login' component={Login} />
              <Route path='/Register' component={Register} />
              <Route path='/Logout' component={Logout} />

              <ProtectedRoute path='/Weather' component={Weather} auth={isAuthenticated} />
              <Route path='/FiveDaysWeatherForecast' component={FiveDaysWeatherForecast} />

              <ProtectedRoute path='/AllFish' component={DisplayAllFish} auth={isAuthenticated} />
              <ProtectedRoute path='/FishInfo' component={FishInfo} auth={isAuthenticated} />
              <ProtectedRoute path='/FishInfoPage/:id' component={FishInfoPage} auth={isAuthenticated} />

              <Route path='/DeleteUser' component={DeleteUser} />
              <Route path='/GetUserById' component={GetUserById} />
              <Route path='/UserDetails' component={UserDetails} />

              <Route path='/FacebookLoginUser' component={FacebookLoginUser} />

              <Route path='/Privacy' component={Privacy} />
              <Route path='/Error' component={Error} />

            </Switch>
            <IdleMonitor/>
          </main>
          <Footer />
        </Router>
      </div>
    </UserContext.Provider >
  );
}
export default App;
