import React, { Component } from 'react';
import { Route } from 'react-router';
import Layout from './Layout';
import Home from './Home';
import Counter from './Counter';
import FetchData from './FetchData';

 
class App extends Component {
  render() {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/counter' component={Counter} />
        <Route path='/fetchdata/:startDateIndex?' component={FetchData} /> 
      </Layout>
    );
  }
} 
export default App;
