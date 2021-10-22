import './App.css';
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
import FishPublications from './components/Fish/FishPublication';
import FishWithPictures from './components/Fish/FishWithPictures';
import Fish from './components/Fish/Fish';
import DeleteUser from './components/AcountManagment/DeleteUser';
import GetUserById from './components/AcountManagment/GetUserById';
import UserDetails from './components/AcountManagment/UserDetails';

function App() {
  return (
    <Router>
    <Sidebar/>
    <Switch>
      <Route path='/CreateKnot' exact component={CreateKnot} />
      <Route path='/CreateCountry' exact component={CreateCountry} />
      <Route path='/Posts' component={Publications}/>
      <Route path='/CreatePost' component={CreatePost}/>
      <Route path='/CreateReservoir' component={CreateReservoir}/>
      <Route path='/Login' component={Login}/>
      <Route path='/Register' component={Register}/>
      <Route path='/Logout' component={logout}/>
      <Route path='/AllFish' component={DisplayAllFish}/>
      <Route path='/FishPublications' component={FishPublications}/>
      <Route path='/FishWithPictures' component={FishWithPictures}/>
      <Route path='/DeleteUser' component={DeleteUser}/>
      <Route path='/GetUserById' component={GetUserById}/>
      <Route path='/UserDetails' component={UserDetails}/>
    </Switch>
  </Router>
  );
}

export default App;
