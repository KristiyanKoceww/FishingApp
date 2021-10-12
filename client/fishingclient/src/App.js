import logo from './logo.svg';
import './App.css';
import Navbar from './NavbarMenu/Navbar';
import {Reservoir} from './Reservoir';
import CreateKnot from './CreateKnot';
import Register from './Account/Register';
import {BrowserRouter, Route, Switch,NavLink} from 'react-router-dom';
import CreateCountry from './Country';
import DisplayAllFish from './Fish/DisplayAllFish';
import { useState } from 'react';
import Publications from './Posts/Publications';


function App() {

  // [data,setData] = useState([]);
 
  // getPosts = async() =>{
  //   const data = await getData();
  //   setData(data);
  // }

  // componentDidMount() {
  //   getPosts();
  // };

  return (
    <BrowserRouter>
    <div className="App container">
      
      <Switch>
        
        <Route path='/Reservoir' component={Reservoir}/>
        <Route path='/DisplayAllFish' component={DisplayAllFish}/>
        <Route path='/Create' component={CreateKnot}/>
        <Route exact path='/Register' component={Register}/>
        <Route exact path='/Country' component={CreateCountry}/>

        <Navbar/>
        <Switch>
          <Route path='/Register'  component={Register} />
          <Route path='/Create' component={CreateKnot} />
          <Route path='/Reservoir' component={Reservoir} />
          <Route exact path='/DisplayAllFish' component={DisplayAllFish}/>
          <Route exact path='/Country' component={CreateCountry}/>
        </Switch>
      </Switch>

      <Publications/>
    </div>
    </BrowserRouter>
  );
}

export default App;
