import React, { Component } from 'react';
import { PropTypes } from 'prop-types';
import NavMenu from './navMenu';
import Header from './Header'
import { Col, Grid, Row } from 'react-bootstrap';

class Layout extends Component {
  render() {
    return <div className='container-fluid'>
      <div className="row">
        <Header />
      </div>
      <div className='row'>
        <div className='col-sm-3'>
          <NavMenu />
        </div>
        <div className='col-sm-offset-3 col-sm-9'>
   
          {this.props.children}
        </div>
      </div>
    </div>;
  }
} 

 

export default Layout;
