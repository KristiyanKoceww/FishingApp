import './App.css';

import { BrowserRouter as Router, Switch, Route, useHistory } from 'react-router-dom';
import { useState, useEffect } from 'react';

import Header from './components/Header/Header';
import Footer from './components/Footer/Footer';

import Sidebar from './components/Navbar/Sidebar';

import CreateKnot from './components/Knot/CreateKnot'
import AllKnots from './components/Knot/AllKnots'
import KnotInfoPage from './components/Knot/KnotInfoPage';

import CreateCountry from './components/Country/Country';

import Login from './components/AcountManagment/Login';
import Logout from './components/AcountManagment/Logout';
import Register from './components/AcountManagment/Register';

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

import Weather from './components/WeatherForecast/Weather';

function App() {
  const [posts, setPosts] = useState([]);
  const jwt = localStorage.getItem("jwt");

  const updatePosts = (post) => {
    let newState = [];
    newState.unshift(...posts, post);
    setPosts(newState);
  }

  // const updatePostComments = (post) => {
  //   // let postInArr = posts.find(x => x.id === post.Id);
  //   // postInArr = post;
  //   // let index = posts.findIndex(x => x.Id === post.Id);
  //   // posts.splice(index, 1)

  //   let myObj = posts.find(obj => obj.Id === post.Id)aa;

  //   console.log();

  //   // updatePosts(post);
  // }

  const [hasMore, sethasMore] = useState(true);
  const [page, setpage] = useState(1);

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
    <div >
      <Router>
        <Header />
        <main className="App">
          <Switch>
            {
              posts ? <Route path='/' exact render={() => <Posts posts={posts} updatePosts={updatePosts} hasMore={hasMore} fetchData={fetchData} />} /> : <div>Loading...</div>
            }
            <Route path='/CreateKnot' component={CreateKnot} />
            <Route path='/AllKnots' component={AllKnots} />
            <Route path='/KnotInfoPage/:id' component={KnotInfoPage} />

            <Route path='/CreateCountry' exact component={CreateCountry} />

            <Route path='/CreatePost' render={() => <CreatePost onChange={updatePosts} />} />

            <Route path='/CreateReservoir' component={CreateReservoir} />
            <Route path='/AllReservoirs' component={AllReservoirs} />
            <Route path='/ReservoirInfoPage/:id' component={ReservoirInfoPage} />

            <Route path='/Login' component={Login} />
            <Route path='/Register' component={Register} />
            <Route path='/Logout' component={Logout} />

            <Route path='/Weather' component={Weather} />

            <Route path='/AllFish' component={DisplayAllFish} />
            <Route path='/FishInfo' component={FishInfo} />
            <Route path='/FishInfoPage/:id' component={FishInfoPage} />

            <Route path='/DeleteUser' component={DeleteUser} />
            <Route path='/GetUserById' component={GetUserById} />
            <Route path='/UserDetails' component={UserDetails} />
          </Switch>
        </main>
      </Router>
      <Footer />
    </div>
  );
}

export default App;
