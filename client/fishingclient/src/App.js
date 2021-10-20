import './App.css';
import Sidebar from './components/Navbar/Sidebar';
import { BrowserRouter as Router, Switch, Route } from 'react-router-dom';
import CreateKnot from './components/Knot/CreateKnot'
import CreateCountry from './components/Country/Country';
import Publications from './components/Posts/Publications'
import CreateReservoir from './components/Reservoir/CreateReservoir';
import Login from './components/AcountManagment/Login';
import Register from './components/AcountManagment/Register';
import CreatePost from './components/Posts/CreatePost';
import { useState, useEffect } from 'react';

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
    </Switch>
  </Router>
  );
}

export default App;
