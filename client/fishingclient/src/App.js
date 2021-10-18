import './App.css';
import Sidebar from './components/Navbar/Sidebar';
import { BrowserRouter as Router, Switch, Route } from 'react-router-dom';
import CreateKnot from './components/Knot/CreateKnot'
import CreateCountry from './components/Country/Country';
import Publications from './components/Posts/Publications'
import CreateReservoir from './components/Reservoir/CreateReservoir';

function App() {

  return (
    <Router>
    <Sidebar />
    <Switch>
      <Route path='/CreateKnot' exact component={CreateKnot} />
      <Route path='/CreateCountry' exact component={CreateCountry} />
      <Route path='/Posts' component={Publications}/>
      <Route path='/CreateReservoir' component={CreateReservoir}/>
    </Switch>
  </Router>
  );
}

export default App;
