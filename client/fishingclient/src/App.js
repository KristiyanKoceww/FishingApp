// import { useState } from 'react';
// import logo from './logo.svg';
// import './App.css';
// import Navbar from './NavbarMenu/Navbar';
// import {Reservoir} from './Reservoir';
// import CreateKnot from './CreateKnot';
// import Register from './Account/Register';
// import {BrowserRouter, Route, Switch,NavLink} from 'react-router-dom';
// import CreateCountry from './Country';
// import DisplayAllFish from './Fish/DisplayAllFish';
// import Publications from './Posts/Publications';
// import Fish from './Fish/Fish';
// import FishPublications from './Fish/FishPublication';
// import RenderRibi from './Fish/Riba';

import './App.css';
import Sidebar from './NavbarMenu/Sidebar';
import { BrowserRouter as Router, Switch, Route } from 'react-router-dom';
import Overview from './pages/Overview';
import { Reports, ReportsOne, ReportsTwo, ReportsThree } from './pages/Reports';
import CreateKnot from './CreateKnot'

function App() {

  return (

    <Router>
    <Sidebar />
    <Switch>
      <Route path='/overview' exact component={Overview} />
      <Route path='/reports' exact component={Reports} />
      <Route path='/reports/reports1' exact component={ReportsOne} />
      <Route path='/reports/reports2' exact component={ReportsTwo} />
      <Route path='/reports/reports3' exact component={ReportsThree} />
      <Route path='/Create' exact component={CreateKnot} />
    </Switch>
  </Router>
    // <BrowserRouter>
    // <div className="App container">
      
    //   <Switch>
        
    //     <Route path='/Reservoir' component={Reservoir}/>
    //     <Route path='/DisplayAllFish' component={DisplayAllFish}/>
    //     <Route path='/Create' component={CreateKnot}/>
    //     <Route exact path='/Register' component={Register}/>
    //     <Route exact path='/Country' component={CreateCountry}/>
    //     <Route exact path='/Posts' component={Publications}/>
    //     <Route exact path='/FishPublication' component={FishPublications}/>
    //     <Route exact path='/Ribi' component={RenderRibi}/>

    //     <Navbar/>
    //     <Switch>
    //       <Route path='/Register'  component={Register} />
    //       <Route path='/Create' component={CreateKnot} />
    //       <Route path='/Reservoir' component={Reservoir} />
    //       <Route exact path='/DisplayAllFish' component={DisplayAllFish}/>
    //       <Route exact path='/Country' component={CreateCountry}/>
    //     </Switch>
        
    //   </Switch> */}
      
    // </div>
    // </BrowserRouter>
  );
}

export default App;
