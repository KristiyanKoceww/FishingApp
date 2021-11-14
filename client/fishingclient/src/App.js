// import './App.css';
import './components/UserPosts/styles/App.scss'
import Sidebar from './components/Navbar/Sidebar';
import { BrowserRouter as Router, Switch, Route } from 'react-router-dom';
import CreateKnot from './components/Knot/CreateKnot'
import CreateCountry from './components/Country/Country';
import CreateReservoir from './components/Reservoir/CreateReservoir';
import Login from './components/AcountManagment/Login';
import Register from './components/AcountManagment/Register';
import CreatePost from './components/UserPosts/CreatePost';
import Logout from './components/AcountManagment/Logout';
import DisplayAllFish from './components/Fish/DisplayAllFish';
import FishInfo from './components/Fish/FishInfo';
import DeleteUser from './components/AcountManagment/DeleteUser';
import GetUserById from './components/AcountManagment/GetUserById';
import UserDetails from './components/AcountManagment/UserDetails';
import FishInfoPage from './components/Fish/FishInfoPage';
import AllReservoirs from './components/Reservoir/AllReservoirs'
import Cards from './components/UserPosts/Cards'
import Footer from './components/UserPosts/Footer';
import ReservoirInfoPage from './components/Reservoir/ReservoirInfoPage';
import AllKnots from './components/Knot/AllKnots'
import KnotInfoPage from './components/Knot/KnotInfoPage';
import WeatherCity from './components/WeatherForecast/WeatherCity';
import Weather from './components/WeatherForecast/Weather';

function App() {
  return (
    <div className="App">
      <Router>
        <Sidebar />
        <Switch>
        <Route path='/' exact component={Cards} />
        
          <Route path='/CreateKnot' component={CreateKnot} />
          <Route path='/AllKnots' component={AllKnots} />
          <Route path='/KnotInfoPage/:id' component={KnotInfoPage} />

          <Route path='/CreateCountry' exact component={CreateCountry} />
          <Route path='/CreatePost' component={CreatePost} />

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
      </Router>
      <main>
        <div className="container">
        
        </div>
      </main>
      <Footer />
    </div>
  );
}

export default App;
