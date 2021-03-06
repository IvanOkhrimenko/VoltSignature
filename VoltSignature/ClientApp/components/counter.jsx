import React, { Component } from 'react';
import { PropTypes } from 'prop-types';
import { connect } from 'react-redux';
import { incrementCount } from '../reducers/counter';

class Counter extends Component {
  render() {
    return (
      <div>
        <h1>Counter</h1>
        <p>This is a simple example of a React component.</p>
        <p>Current count: <strong>{ this.props.count }</strong></p>
        <button onClick={ () => { this.props.increment() } }>Increment</button>
     </div>
    );
  }
}



const mapStateToProps = (state) => ({ 
  count: state.counter.count
});

const mapDispatchToProps = (dispatch) => ({
  increment: () => {
    dispatch(incrementCount());
  }
});

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(Counter);
