// import './App.css';
import './components/UserPosts/styles/App.scss'
import Sidebar from './components/Navbar/Sidebar';
import { BrowserRouter as Router, Switch, Route, useHistory } from 'react-router-dom';
import CreateKnot from './components/Knot/CreateKnot'
import CreateCountry from './components/Country/Country';
import Publications from './components/Posts/Publications'
import CreateReservoir from './components/Reservoir/CreateReservoir';
import Login from './components/AcountManagment/Login';
import Register from './components/AcountManagment/Register';
import CreatePost from './components/Posts/CreatePost';
import { useState, useEffect } from 'react';
import logout from './components/AcountManagment/Logout';
import DisplayAllFish from './components/Fish/DisplayAllFish';
import FishInfo from './components/Fish/FishInfo';
import DeleteUser from './components/AcountManagment/DeleteUser';
import GetUserById from './components/AcountManagment/GetUserById';
import UserDetails from './components/AcountManagment/UserDetails';

import Cards from './components/UserPosts/Cards'

function App() {
  return (
    <div className="App">
      <Router>
        <Sidebar />
        <Switch>
          <Route path='/CreateKnot' exact component={CreateKnot} />
          <Route path='/CreateCountry' exact component={CreateCountry} />
          <Route path='/Posts' component={Publications} />
          <Route path='/CreatePost' component={CreatePost} />
          <Route path='/CreateReservoir' component={CreateReservoir} />
          <Route path='/Login' component={Login} />
          <Route path='/Register' component={Register} />
          <Route path='/Logout' component={logout} />
          <Route path='/AllFish' component={DisplayAllFish} />
          <Route path='/FishInfo' component={FishInfo} />
          <Route path='/DeleteUser' component={DeleteUser} />
          <Route path='/GetUserById' component={GetUserById} />
          <Route path='/UserDetails' component={UserDetails} />
        </Switch>
      </Router>
      <main>
      <div className="container">
        <Cards />
      </div>
      </main>
    </div>
  );
}

export default App;
