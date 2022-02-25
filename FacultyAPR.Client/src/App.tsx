import loadable from "@loadable/component";
import './App.css';
import Footer from './components/Form/footer'
import ProfileSelector from './components/Profile/ProfileSelection'
import 'bootstrap/dist/css/bootstrap.min.css';
import Loading from './Loading';
import {
  BrowserRouter as Router,
  Switch,
  Route,
} from "react-router-dom";

import NavigationBar from './components/common/NavigationBar';
const Login = loadable(() => import('./components/login/Login'), 
{fallback:<Loading/>,
});
const HomeResolver = loadable(() => import('./components/HomeResolver/HomeResolver'), 
{fallback:<Loading/>,
});
const APRForm  = loadable(() => import('./components/Form/APRForm'),
{fallback:<Loading/>,
});
const NotFound = loadable(() => import('./components/common/NotFound'),
{fallback:<Loading/>,
});
const Forbidden = loadable(() => import('./components/common/Forbidden'),
{fallback:<Loading/>,
});

const App = () => {
  return (
    <div className = "main-app">
    <Router>
      <NavigationBar no-print />
      <Switch>
          <Route exact path="/" component = {HomeResolver}/>
          <Route exact path="/Login" component = {Login}/>
          <Route exact path="/FormReview"  component = {APRForm}/>
          <Route path="/Form" component = {APRForm}/>
          <Route path="/Forbidden" component = {Forbidden}/>
          <Route component = {NotFound}/>
        </Switch>
        {<Footer/>}
    </Router>
  </div>);
 
}

export default App;
