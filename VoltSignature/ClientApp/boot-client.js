import 'jquery';
import './css/site.css'
import 'bootstrap/dist/css/bootstrap.css';
import 'bootstrap/dist/css/bootstrap-theme.css';
import React from 'react';
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux'; 
import { createBrowserHistory } from 'history';
import { ConnectedRouter} from 'react-router-redux'; 
import configureStore from './configureStore'; 
import App from './components/app';
 
const history = createBrowserHistory();
// Get the application-wide store instance, prepopulating with state from the server where available.
const initialState = window.initialReduxState;
const store = configureStore(history,initialState);
//const history = syncHistoryWithStore(createBrowserHistory(), store);

// This code starts up the React app when it runs in a browser. It sets up the routing configuration
// and injects the app into a DOM element.
ReactDOM.render(
  <Provider store={store}>
    <ConnectedRouter history={history}>
      <App />
    </ConnectedRouter>
  </Provider>,
  document.getElementById('react-app')
);

