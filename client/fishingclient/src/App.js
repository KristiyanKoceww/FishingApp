import logo from './logo.svg';
import './App.css';
import {Reservoir} from './Reservoir';
import {CreateReservoir} from './CreateReservoir';
import {BrowserRouter, Route, Switch,NavLink} from 'react-router-dom';

function App() {
  return (
    <BrowserRouter>
    <div className="App container">
      <h3 className="d-flex justify-content-center m-3">
        React JS Frontend
      </h3>
        
      <nav className="navbar navbar-expand-sm bg-light navbar-dark">
        <ul className="navbar-nav">
          <li className="nav-item- m-1">
            <NavLink className="btn btn-light btn-outline-primary" to="/Reservoir">
            All Reservoir
            </NavLink>
            
            <NavLink className="btn btn-light btn-outline-primary" to="/Create">
            Create Reservoir
            </NavLink>
          </li>
        </ul>
      </nav>

      <Switch>
        <Route path='/Reservoir' component={Reservoir}/>
        <Route path='/Create' component={CreateReservoir}/>
      </Switch>
    </div>
    </BrowserRouter>
  );
}

export default App;
