import React, { Component } from 'react';
import { PropTypes } from 'prop-types';
import { Link } from 'react-router-dom';
import { Glyphicon, Nav, Navbar, NavItem } from 'react-bootstrap';
import { LinkContainer } from 'react-router-bootstrap';

export default class Header extends Component {
  render() {
    return (
      <div className="custom-header col-sm-12">
        <div className="header-item">
          <Link to={'/'} exact>      
            <Glyphicon glyph='glyphicon glyphicon-user' /> My Profile         
          </Link>
          <a href="Account/Logout" exact>            
            <Glyphicon glyph='glyphicon glyphicon-log-out' /> Logout   
          </a>
        </div>
      </div>
    );
  }
}
