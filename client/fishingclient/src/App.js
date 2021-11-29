import './App.css';

import { BrowserRouter as Router, Switch, Route, useHistory } from 'react-router-dom';
import { useState, useEffect } from 'react';

import Header from './components/Header/Header';
import Footer from './components/Footer/Footer';

// primereact
import { Button } from 'primereact/button'
import 'primereact/resources/themes/lara-light-indigo/theme.css';
import "primereact/resources/primereact.min.css";
import 'primeicons/primeicons.css';

import Sidebar from './components/Navbar/Sidebar';

import CreateKnot from './components/Knot/CreateKnot'
import AllKnots from './components/Knot/AllKnots'
import KnotInfoPage from './components/Knot/KnotInfoPage';

import CreateCountry from './components/Country/Country';

import Login from './components/AcountManagment/Login';
import Logout from './components/AcountManagment/Logout';
import Register from './components/AcountManagment/Register';

import CreatePost from './components/Posts/CreatePost';
import Post from './components/Posts/Post';

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
  const [createFormToggle, setCreateFormToggle] = useState(false);
  const jwt = localStorage.getItem("jwt");
  
  
  const updatePosts = (post) => {
    let newState = [];
    newState.push(...posts,post);
    setPosts(newState);
    // not working ? 
    //setPosts(...posts,post);
    setCreateFormToggle();
  }

  // const updatePostComments = (post) => {
  //   // let postInArr = posts.find(x => x.id === post.Id);
  //   // postInArr = post;
  //   // let index = posts.findIndex(x => x.Id === post.Id);
  //   // posts.splice(index, 1)

  //   let myObj = posts.find(obj => obj.Id === post.Id);

  //   console.log();

  //   // updatePosts(post);
  // }

  const toggleForm = () => {
    setCreateFormToggle(!createFormToggle);
  }
  

  const fetchPostData = async () => {
    fetch('https://localhost:44366/api/Posts/getAllPosts',
      {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
          'Authorization': 'Bearer ' + jwt
        },
      })
      .then(r => {
        if (!r.ok) {
          throw new Error(`HTTP error ${r.status}`);
        }
        return r.json();
      })
      .then(result => {
        if (posts != result) {
          setPosts(result)
        }
      })
      .catch(error => {
        console.log(error);
      });
  }

  useEffect(() => {
    fetchPostData()
  }, []);


  return (
    <div >
      <Router>
        <Header />
        <main className="App">
          <Switch>
            {/* <Route path='/' exact component={Cards} /> */}

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


          <Button className='p-button-primary' icon='pi pi-plus' label="Add post" icon="pi pi-check" iconPos="right" onClick={toggleForm} />
          {createFormToggle &&
            <CreatePost onCreate={updatePosts}/>
          }

          {
            posts.map(post => (
               <Post key={post.Id} postId={post.Id} keyToAppend={post.CreatedOn} username={post.User.FirstName} title={post.Title} content={post.Content} images={post.ImageUrls} avatarImage={post.User.MainImageUrl} comments={post.Comments} />
              // <Post key={post.Id} post={post} />
            ))
          }

        </main>
      </Router>
      <Footer />
    </div>
  );
}

export default App;
