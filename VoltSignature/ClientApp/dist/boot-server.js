(function(e, a) { for(var i in a) e[i] = a[i]; }(exports, /******/ (function(modules) { // webpackBootstrap
/******/ 	// The module cache
/******/ 	var installedModules = {};
/******/
/******/ 	// The require function
/******/ 	function __webpack_require__(moduleId) {
/******/
/******/ 		// Check if module is in cache
/******/ 		if(installedModules[moduleId])
/******/ 			return installedModules[moduleId].exports;
/******/
/******/ 		// Create a new module (and put it into the cache)
/******/ 		var module = installedModules[moduleId] = {
/******/ 			exports: {},
/******/ 			id: moduleId,
/******/ 			loaded: false
/******/ 		};
/******/
/******/ 		// Execute the module function
/******/ 		modules[moduleId].call(module.exports, module, module.exports, __webpack_require__);
/******/
/******/ 		// Flag the module as loaded
/******/ 		module.loaded = true;
/******/
/******/ 		// Return the exports of the module
/******/ 		return module.exports;
/******/ 	}
/******/
/******/
/******/ 	// expose the modules object (__webpack_modules__)
/******/ 	__webpack_require__.m = modules;
/******/
/******/ 	// expose the module cache
/******/ 	__webpack_require__.c = installedModules;
/******/
/******/ 	// __webpack_public_path__
/******/ 	__webpack_require__.p = "";
/******/
/******/ 	// Load entry module and return exports
/******/ 	return __webpack_require__(0);
/******/ })
/************************************************************************/
/******/ ([
/* 0 */
/***/ (function(module, exports, __webpack_require__) {

	'use strict';
	
	Object.defineProperty(exports, "__esModule", {
	  value: true
	});
	
	var _react = __webpack_require__(1);
	
	var _react2 = _interopRequireDefault(_react);
	
	var _server = __webpack_require__(2);
	
	var _reactRedux = __webpack_require__(3);
	
	var _reactRouter = __webpack_require__(4);
	
	var _routes = __webpack_require__(5);
	
	var _routes2 = _interopRequireDefault(_routes);
	
	var _configureStore = __webpack_require__(14);
	
	var _configureStore2 = _interopRequireDefault(_configureStore);
	
	function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }
	
	exports.default = function (params) {
	  return new Promise(function (resolve, reject) {
	    (0, _reactRouter.match)({ routes: _routes2.default, location: params.location }, function (error, redirectLocation, renderProps) {
	      if (error) {
	        throw error;
	      }
	
	      // If there's a redirection, just send this information back to the host application
	      if (redirectLocation) {
	        resolve({ redirectUrl: redirectLocation.pathname });
	      }
	
	      // If it didn't match any route, renderProps will be undefined
	      if (!renderProps) {
	        throw new Error('The location \'' + params.url + '\' doesn\'t match any route configured in react-router.');
	      }
	
	      // At this point if we want to initialize the store we need to pass an object with shape
	      // {counter: {count: 10}}
	      var store = (0, _configureStore2.default)();
	      var app = _react2.default.createElement(
	        _reactRedux.Provider,
	        { store: store },
	        _react2.default.createElement(_reactRouter.RouterContext, renderProps)
	      );
	
	      // Perform an initial render that will cause any async tasks (e.g., data access) to begin
	      (0, _server.renderToString)(app);
	
	      // Once the tasks are done, we can perform the final render
	      // We also send the redux store state, so the client can continue execution where the server left off
	      params.domainTasks.then(function () {
	        resolve({
	          html: (0, _server.renderToString)(app),
	          globals: { initialReduxState: store.getState() }
	        });
	      }, reject); // Also propagate any errors back into the host application
	    });
	  });
	};

/***/ }),
/* 1 */
/***/ (function(module, exports) {

	module.exports = require("react");

/***/ }),
/* 2 */
/***/ (function(module, exports) {

	module.exports = require("react-dom/server");

/***/ }),
/* 3 */
/***/ (function(module, exports) {

	module.exports = require("react-redux");

/***/ }),
/* 4 */
/***/ (function(module, exports) {

	module.exports = require("react-router");

/***/ }),
/* 5 */
/***/ (function(module, exports, __webpack_require__) {

	'use strict';
	
	Object.defineProperty(exports, "__esModule", {
	  value: true
	});
	
	var _react = __webpack_require__(1);
	
	var _react2 = _interopRequireDefault(_react);
	
	var _reactRouter = __webpack_require__(4);
	
	var _layout = __webpack_require__(6);
	
	var _layout2 = _interopRequireDefault(_layout);
	
	var _home = __webpack_require__(8);
	
	var _home2 = _interopRequireDefault(_home);
	
	var _fetchData = __webpack_require__(9);
	
	var _fetchData2 = _interopRequireDefault(_fetchData);
	
	var _counter = __webpack_require__(12);
	
	var _counter2 = _interopRequireDefault(_counter);
	
	function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }
	
	exports.default = _react2.default.createElement(
	  _reactRouter.Route,
	  { component: _layout2.default },
	  _react2.default.createElement(_reactRouter.Route, { path: '/', components: { body: _home2.default } }),
	  _react2.default.createElement(_reactRouter.Route, { path: '/counter', components: { body: _counter2.default } }),
	  _react2.default.createElement(
	    _reactRouter.Route,
	    { path: '/fetchdata', components: { body: _fetchData2.default } },
	    _react2.default.createElement(_reactRouter.Route, { path: ':startDateIndex' }),
	    ' '
	  )
	);

/***/ }),
/* 6 */
/***/ (function(module, exports, __webpack_require__) {

	'use strict';
	
	Object.defineProperty(exports, "__esModule", {
	  value: true
	});
	
	var _createClass = function () { function defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ("value" in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } } return function (Constructor, protoProps, staticProps) { if (protoProps) defineProperties(Constructor.prototype, protoProps); if (staticProps) defineProperties(Constructor, staticProps); return Constructor; }; }();
	
	var _react = __webpack_require__(1);
	
	var _react2 = _interopRequireDefault(_react);
	
	var _NavMenu = __webpack_require__(7);
	
	var _NavMenu2 = _interopRequireDefault(_NavMenu);
	
	function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }
	
	function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError("Cannot call a class as a function"); } }
	
	function _possibleConstructorReturn(self, call) { if (!self) { throw new ReferenceError("this hasn't been initialised - super() hasn't been called"); } return call && (typeof call === "object" || typeof call === "function") ? call : self; }
	
	function _inherits(subClass, superClass) { if (typeof superClass !== "function" && superClass !== null) { throw new TypeError("Super expression must either be null or a function, not " + typeof superClass); } subClass.prototype = Object.create(superClass && superClass.prototype, { constructor: { value: subClass, enumerable: false, writable: true, configurable: true } }); if (superClass) Object.setPrototypeOf ? Object.setPrototypeOf(subClass, superClass) : subClass.__proto__ = superClass; }
	
	var Layout = function (_Component) {
	  _inherits(Layout, _Component);
	
	  function Layout() {
	    _classCallCheck(this, Layout);
	
	    return _possibleConstructorReturn(this, (Layout.__proto__ || Object.getPrototypeOf(Layout)).apply(this, arguments));
	  }
	
	  _createClass(Layout, [{
	    key: 'render',
	    value: function render() {
	      return _react2.default.createElement(
	        'div',
	        { className: 'container-fluid' },
	        _react2.default.createElement(
	          'div',
	          { className: 'row' },
	          _react2.default.createElement(
	            'div',
	            { className: 'col-sm-3' },
	            _react2.default.createElement(_NavMenu2.default, null)
	          ),
	          _react2.default.createElement(
	            'div',
	            { className: 'col-sm-9' },
	            this.props.body
	          )
	        )
	      );
	    }
	  }]);
	
	  return Layout;
	}(_react.Component);
	
	Layout.propTypes = {
	  body: _react.PropTypes.element
	};
	
	exports.default = Layout;

/***/ }),
/* 7 */
/***/ (function(module, exports, __webpack_require__) {

	'use strict';
	
	Object.defineProperty(exports, "__esModule", {
	  value: true
	});
	
	var _createClass = function () { function defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ("value" in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } } return function (Constructor, protoProps, staticProps) { if (protoProps) defineProperties(Constructor.prototype, protoProps); if (staticProps) defineProperties(Constructor, staticProps); return Constructor; }; }();
	
	var _react = __webpack_require__(1);
	
	var _react2 = _interopRequireDefault(_react);
	
	var _reactRouter = __webpack_require__(4);
	
	function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }
	
	function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError("Cannot call a class as a function"); } }
	
	function _possibleConstructorReturn(self, call) { if (!self) { throw new ReferenceError("this hasn't been initialised - super() hasn't been called"); } return call && (typeof call === "object" || typeof call === "function") ? call : self; }
	
	function _inherits(subClass, superClass) { if (typeof superClass !== "function" && superClass !== null) { throw new TypeError("Super expression must either be null or a function, not " + typeof superClass); } subClass.prototype = Object.create(superClass && superClass.prototype, { constructor: { value: subClass, enumerable: false, writable: true, configurable: true } }); if (superClass) Object.setPrototypeOf ? Object.setPrototypeOf(subClass, superClass) : subClass.__proto__ = superClass; }
	
	var NavMenu = function (_Component) {
	  _inherits(NavMenu, _Component);
	
	  function NavMenu() {
	    _classCallCheck(this, NavMenu);
	
	    return _possibleConstructorReturn(this, (NavMenu.__proto__ || Object.getPrototypeOf(NavMenu)).apply(this, arguments));
	  }
	
	  _createClass(NavMenu, [{
	    key: 'render',
	    value: function render() {
	      return _react2.default.createElement(
	        'div',
	        { className: 'main-nav' },
	        _react2.default.createElement(
	          'div',
	          { className: 'navbar navbar-inverse' },
	          _react2.default.createElement(
	            'div',
	            { className: 'navbar-header' },
	            _react2.default.createElement(
	              'button',
	              { type: 'button', className: 'navbar-toggle', 'data-toggle': 'collapse', 'data-target': '.navbar-collapse' },
	              _react2.default.createElement(
	                'span',
	                { className: 'sr-only' },
	                'Toggle navigation'
	              ),
	              _react2.default.createElement('span', { className: 'icon-bar' }),
	              _react2.default.createElement('span', { className: 'icon-bar' }),
	              _react2.default.createElement('span', { className: 'icon-bar' })
	            ),
	            _react2.default.createElement(
	              _reactRouter.Link,
	              { className: 'navbar-brand', to: '/' },
	              'WebApplicationBasic'
	            )
	          ),
	          _react2.default.createElement('div', { className: 'clearfix' }),
	          _react2.default.createElement(
	            'div',
	            { className: 'navbar-collapse collapse' },
	            _react2.default.createElement(
	              'ul',
	              { className: 'nav navbar-nav' },
	              _react2.default.createElement(
	                'li',
	                null,
	                _react2.default.createElement(
	                  _reactRouter.Link,
	                  { to: '/', activeClassName: 'active' },
	                  _react2.default.createElement('span', { className: 'glyphicon glyphicon-home' }),
	                  ' Home'
	                )
	              ),
	              _react2.default.createElement(
	                'li',
	                null,
	                _react2.default.createElement(
	                  _reactRouter.Link,
	                  { to: '/counter', activeClassName: 'active' },
	                  _react2.default.createElement('span', { className: 'glyphicon glyphicon-education' }),
	                  ' Counter'
	                )
	              ),
	              _react2.default.createElement(
	                'li',
	                null,
	                _react2.default.createElement(
	                  _reactRouter.Link,
	                  { to: '/fetchdata', activeClassName: 'active' },
	                  _react2.default.createElement('span', { className: 'glyphicon glyphicon-th-list' }),
	                  ' Fetch data'
	                )
	              )
	            )
	          )
	        )
	      );
	    }
	  }]);
	
	  return NavMenu;
	}(_react.Component);
	
	exports.default = NavMenu;

/***/ }),
/* 8 */
/***/ (function(module, exports, __webpack_require__) {

	'use strict';
	
	Object.defineProperty(exports, "__esModule", {
	  value: true
	});
	
	var _createClass = function () { function defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ("value" in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } } return function (Constructor, protoProps, staticProps) { if (protoProps) defineProperties(Constructor.prototype, protoProps); if (staticProps) defineProperties(Constructor, staticProps); return Constructor; }; }();
	
	var _react = __webpack_require__(1);
	
	var _react2 = _interopRequireDefault(_react);
	
	function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }
	
	function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError("Cannot call a class as a function"); } }
	
	function _possibleConstructorReturn(self, call) { if (!self) { throw new ReferenceError("this hasn't been initialised - super() hasn't been called"); } return call && (typeof call === "object" || typeof call === "function") ? call : self; }
	
	function _inherits(subClass, superClass) { if (typeof superClass !== "function" && superClass !== null) { throw new TypeError("Super expression must either be null or a function, not " + typeof superClass); } subClass.prototype = Object.create(superClass && superClass.prototype, { constructor: { value: subClass, enumerable: false, writable: true, configurable: true } }); if (superClass) Object.setPrototypeOf ? Object.setPrototypeOf(subClass, superClass) : subClass.__proto__ = superClass; }
	
	var Home = function (_Component) {
	  _inherits(Home, _Component);
	
	  function Home() {
	    _classCallCheck(this, Home);
	
	    return _possibleConstructorReturn(this, (Home.__proto__ || Object.getPrototypeOf(Home)).apply(this, arguments));
	  }
	
	  _createClass(Home, [{
	    key: 'render',
	    value: function render() {
	      return _react2.default.createElement(
	        'div',
	        null,
	        _react2.default.createElement(
	          'h1',
	          null,
	          'Hello, world!'
	        ),
	        _react2.default.createElement(
	          'p',
	          null,
	          'Welcome to your new single-page application, built with:'
	        ),
	        _react2.default.createElement(
	          'ul',
	          null,
	          _react2.default.createElement(
	            'li',
	            null,
	            _react2.default.createElement(
	              'a',
	              { href: 'https://get.asp.net/' },
	              'ASP.NET Core'
	            ),
	            ' and ',
	            _react2.default.createElement(
	              'a',
	              { href: 'https://msdn.microsoft.com/en-us/library/67ef8sbd.aspx' },
	              'C#'
	            ),
	            ' for cross-platform server-side code'
	          ),
	          _react2.default.createElement(
	            'li',
	            null,
	            _react2.default.createElement(
	              'a',
	              { href: 'https://facebook.github.io/react/' },
	              'React'
	            ),
	            ', ',
	            _react2.default.createElement(
	              'a',
	              { href: 'http://redux.js.org' },
	              'Redux'
	            ),
	            ', and ',
	            _react2.default.createElement(
	              'a',
	              { href: 'http://www.typescriptlang.org/' },
	              'TypeScript'
	            ),
	            ' for client-side code'
	          ),
	          _react2.default.createElement(
	            'li',
	            null,
	            _react2.default.createElement(
	              'a',
	              { href: 'https://webpack.github.io/' },
	              'Webpack'
	            ),
	            ' for building and bundling client-side resources'
	          ),
	          _react2.default.createElement(
	            'li',
	            null,
	            _react2.default.createElement(
	              'a',
	              { href: 'http://getbootstrap.com/' },
	              'Bootstrap'
	            ),
	            ' for layout and styling'
	          )
	        ),
	        _react2.default.createElement(
	          'p',
	          null,
	          'To help you get started, we\'ve also set up:'
	        ),
	        _react2.default.createElement(
	          'ul',
	          null,
	          _react2.default.createElement(
	            'li',
	            null,
	            _react2.default.createElement(
	              'strong',
	              null,
	              'Client-side navigation'
	            ),
	            '. For example, click ',
	            _react2.default.createElement(
	              'em',
	              null,
	              'Counter'
	            ),
	            ' then ',
	            _react2.default.createElement(
	              'em',
	              null,
	              'Back'
	            ),
	            ' to return here.'
	          ),
	          _react2.default.createElement(
	            'li',
	            null,
	            _react2.default.createElement(
	              'strong',
	              null,
	              'Webpack dev middleware'
	            ),
	            '. In development mode, there\'s no need to run the ',
	            _react2.default.createElement(
	              'code',
	              null,
	              'webpack'
	            ),
	            ' build tool. Your client-side resources are dynamically built on demand. Updates are available as soon as you modify any file.'
	          ),
	          _react2.default.createElement(
	            'li',
	            null,
	            _react2.default.createElement(
	              'strong',
	              null,
	              'Hot module replacement'
	            ),
	            '. In development mode, you don\'t even need to reload the page after making most changes. Within seconds of saving changes to files, rebuilt CSS and React components will be injected directly into your running application, preserving its live state.'
	          ),
	          _react2.default.createElement(
	            'li',
	            null,
	            _react2.default.createElement(
	              'strong',
	              null,
	              'Efficient production builds'
	            ),
	            '. In production mode, development-time features are disabled, and the ',
	            _react2.default.createElement(
	              'code',
	              null,
	              'webpack'
	            ),
	            ' build tool produces minified static CSS and JavaScript files.'
	          ),
	          _react2.default.createElement(
	            'li',
	            null,
	            _react2.default.createElement(
	              'strong',
	              null,
	              'Server-side prerendering'
	            ),
	            '. To optimize startup time, your React application is first rendered on the server. The initial HTML and state is then transferred to the browser, where client-side code picks up where the server left off.'
	          )
	        )
	      );
	    }
	  }]);
	
	  return Home;
	}(_react.Component);
	
	exports.default = Home;

/***/ }),
/* 9 */
/***/ (function(module, exports, __webpack_require__) {

	'use strict';
	
	Object.defineProperty(exports, "__esModule", {
	  value: true
	});
	
	var _createClass = function () { function defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ("value" in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } } return function (Constructor, protoProps, staticProps) { if (protoProps) defineProperties(Constructor.prototype, protoProps); if (staticProps) defineProperties(Constructor, staticProps); return Constructor; }; }();
	
	var _react = __webpack_require__(1);
	
	var _react2 = _interopRequireDefault(_react);
	
	var _reactRedux = __webpack_require__(3);
	
	var _reactRouter = __webpack_require__(4);
	
	var _weatherForecasts = __webpack_require__(10);
	
	function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }
	
	function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError("Cannot call a class as a function"); } }
	
	function _possibleConstructorReturn(self, call) { if (!self) { throw new ReferenceError("this hasn't been initialised - super() hasn't been called"); } return call && (typeof call === "object" || typeof call === "function") ? call : self; }
	
	function _inherits(subClass, superClass) { if (typeof superClass !== "function" && superClass !== null) { throw new TypeError("Super expression must either be null or a function, not " + typeof superClass); } subClass.prototype = Object.create(superClass && superClass.prototype, { constructor: { value: subClass, enumerable: false, writable: true, configurable: true } }); if (superClass) Object.setPrototypeOf ? Object.setPrototypeOf(subClass, superClass) : subClass.__proto__ = superClass; }
	
	var FetchData = function (_Component) {
	  _inherits(FetchData, _Component);
	
	  function FetchData() {
	    _classCallCheck(this, FetchData);
	
	    return _possibleConstructorReturn(this, (FetchData.__proto__ || Object.getPrototypeOf(FetchData)).apply(this, arguments));
	  }
	
	  _createClass(FetchData, [{
	    key: 'componentWillMount',
	    value: function componentWillMount() {
	      // This method runs when the component is first added to the page
	      var startDateIndex = parseInt(this.props.params.startDateIndex) || 0;
	      this.props.requestWeatherForecasts(startDateIndex);
	    }
	  }, {
	    key: 'componentWillReceiveProps',
	    value: function componentWillReceiveProps(nextProps) {
	      // This method runs when incoming props (e.g., route params) change
	      var startDateIndex = parseInt(nextProps.params.startDateIndex) || 0;
	      this.props.requestWeatherForecasts(startDateIndex);
	    }
	  }, {
	    key: 'render',
	    value: function render() {
	      var table = _react2.default.createElement(
	        'table',
	        { className: 'table' },
	        _react2.default.createElement(
	          'thead',
	          null,
	          _react2.default.createElement(
	            'tr',
	            null,
	            _react2.default.createElement(
	              'th',
	              null,
	              'Date'
	            ),
	            _react2.default.createElement(
	              'th',
	              null,
	              'Temp. (C)'
	            ),
	            _react2.default.createElement(
	              'th',
	              null,
	              'Temp. (F)'
	            ),
	            _react2.default.createElement(
	              'th',
	              null,
	              'Summary'
	            )
	          )
	        ),
	        _react2.default.createElement(
	          'tbody',
	          null,
	          this.props.forecasts.map(function (forecast) {
	            return _react2.default.createElement(
	              'tr',
	              { key: forecast.dateFormatted },
	              _react2.default.createElement(
	                'td',
	                null,
	                forecast.dateFormatted
	              ),
	              _react2.default.createElement(
	                'td',
	                null,
	                forecast.temperatureC
	              ),
	              _react2.default.createElement(
	                'td',
	                null,
	                forecast.temperatureF
	              ),
	              _react2.default.createElement(
	                'td',
	                null,
	                forecast.summary
	              )
	            );
	          })
	        )
	      );
	
	      var prevStartDateIndex = this.props.startDateIndex - 5;
	      var nextStartDateIndex = this.props.startDateIndex + 5;
	      var pagination = _react2.default.createElement(
	        'p',
	        { className: 'clearfix text-center' },
	        _react2.default.createElement(
	          _reactRouter.Link,
	          { className: 'btn btn-default pull-left', to: '/fetchdata/' + prevStartDateIndex },
	          'Previous'
	        ),
	        _react2.default.createElement(
	          _reactRouter.Link,
	          { className: 'btn btn-default pull-right', to: '/fetchdata/' + nextStartDateIndex },
	          'Next'
	        ),
	        this.props.isLoading ? _react2.default.createElement(
	          'span',
	          null,
	          'Loading...'
	        ) : []
	      );
	
	      return _react2.default.createElement(
	        'div',
	        null,
	        _react2.default.createElement(
	          'h1',
	          null,
	          'Weather forecast'
	        ),
	        _react2.default.createElement(
	          'p',
	          null,
	          'This component demonstrates fetching data from the server and working with URL parameters.'
	        ),
	        table,
	        pagination
	      );
	    }
	  }]);
	
	  return FetchData;
	}(_react.Component);
	
	FetchData.propTypes = {
	  startDateIndex: _react.PropTypes.number,
	  isLoading: _react.PropTypes.bool,
	  requestWeatherForecasts: _react.PropTypes.func.isRequired
	};
	
	var mapStateToProps = function mapStateToProps(state) {
	  return {
	    startDateIndex: state.weatherForecasts.startDateIndex,
	    forecasts: state.weatherForecasts.forecasts,
	    isLoading: state.weatherForecasts.isLoading
	  };
	};
	
	var mapDispatchToProps = function mapDispatchToProps(dispatch) {
	  return {
	    requestWeatherForecasts: function requestWeatherForecasts(startDateIndex) {
	      dispatch((0, _weatherForecasts.requestWeatherForecasts)(startDateIndex));
	    }
	  };
	};
	
	exports.default = (0, _reactRedux.connect)(mapStateToProps, mapDispatchToProps)(FetchData);

/***/ }),
/* 10 */
/***/ (function(module, exports, __webpack_require__) {

	'use strict';
	
	Object.defineProperty(exports, "__esModule", {
	  value: true
	});
	exports.receiveWeatherForecasts = exports.requestWeatherForecasts = undefined;
	
	var _fetch = __webpack_require__(11);
	
	// Constant defining the action type
	var REQUEST_WEATHER_FORECASTS = 'REQUEST_WEATHER_FORECASTS';
	var RECEIVE_WEATHER_FORECASTS = 'RECEIVE_WEATHER_FORECASTS';
	
	// Action creators
	var requestWeatherForecasts = exports.requestWeatherForecasts = function requestWeatherForecasts(startDateIndex) {
	  return function (dispatch, getState) {
	    if (startDateIndex !== getState().weatherForecasts.startDateIndex) {
	      dispatch({ type: REQUEST_WEATHER_FORECASTS, payload: startDateIndex });
	      return (0, _fetch.fetch)('/api/SampleData/WeatherForecasts?startDateIndex=' + startDateIndex).then(function (response) {
	        return response.json();
	      }).then(function (data) {
	        dispatch(receiveWeatherForecasts(startDateIndex, data));
	      });
	    }
	  };
	};
	
	var receiveWeatherForecasts = exports.receiveWeatherForecasts = function receiveWeatherForecasts(startDateIndex, forecasts) {
	  return {
	    type: RECEIVE_WEATHER_FORECASTS,
	    payload: { startDateIndex: startDateIndex, forecasts: forecasts }
	  };
	};
	
	var initialState = {
	  startDateIndex: null,
	  forecasts: [],
	  isLoading: false
	};
	
	// The reducer that changes the state based on the action type
	
	exports.default = function () {
	  var state = arguments.length > 0 && arguments[0] !== undefined ? arguments[0] : initialState;
	  var action = arguments[1];
	
	
	  if (action.type === REQUEST_WEATHER_FORECASTS) {
	    return { startDateIndex: action.payload, isLoading: true, forecasts: state.forecasts };
	  } else if (action.type === RECEIVE_WEATHER_FORECASTS) {
	    // Only accept the incoming data if it matches the most recent request. This ensures we correctly
	    // handle out-of-order responses.
	    if (action.payload.startDateIndex === state.startDateIndex) {
	      return { startDateIndex: action.payload.startDateIndex, forecasts: action.payload.forecasts, isLoading: false };
	    }
	  }
	
	  return state;
	};

/***/ }),
/* 11 */
/***/ (function(module, exports) {

	module.exports = require("domain-task/fetch");

/***/ }),
/* 12 */
/***/ (function(module, exports, __webpack_require__) {

	'use strict';
	
	Object.defineProperty(exports, "__esModule", {
	  value: true
	});
	
	var _createClass = function () { function defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ("value" in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } } return function (Constructor, protoProps, staticProps) { if (protoProps) defineProperties(Constructor.prototype, protoProps); if (staticProps) defineProperties(Constructor, staticProps); return Constructor; }; }();
	
	var _react = __webpack_require__(1);
	
	var _react2 = _interopRequireDefault(_react);
	
	var _reactRedux = __webpack_require__(3);
	
	var _counter = __webpack_require__(13);
	
	function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }
	
	function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError("Cannot call a class as a function"); } }
	
	function _possibleConstructorReturn(self, call) { if (!self) { throw new ReferenceError("this hasn't been initialised - super() hasn't been called"); } return call && (typeof call === "object" || typeof call === "function") ? call : self; }
	
	function _inherits(subClass, superClass) { if (typeof superClass !== "function" && superClass !== null) { throw new TypeError("Super expression must either be null or a function, not " + typeof superClass); } subClass.prototype = Object.create(superClass && superClass.prototype, { constructor: { value: subClass, enumerable: false, writable: true, configurable: true } }); if (superClass) Object.setPrototypeOf ? Object.setPrototypeOf(subClass, superClass) : subClass.__proto__ = superClass; }
	
	var Counter = function (_Component) {
	  _inherits(Counter, _Component);
	
	  function Counter() {
	    _classCallCheck(this, Counter);
	
	    return _possibleConstructorReturn(this, (Counter.__proto__ || Object.getPrototypeOf(Counter)).apply(this, arguments));
	  }
	
	  _createClass(Counter, [{
	    key: 'render',
	    value: function render() {
	      var _this2 = this;
	
	      return _react2.default.createElement(
	        'div',
	        null,
	        _react2.default.createElement(
	          'h1',
	          null,
	          'Counter'
	        ),
	        _react2.default.createElement(
	          'p',
	          null,
	          'This is a simple example of a React component.'
	        ),
	        _react2.default.createElement(
	          'p',
	          null,
	          'Current count: ',
	          _react2.default.createElement(
	            'strong',
	            null,
	            this.props.count
	          )
	        ),
	        _react2.default.createElement(
	          'button',
	          { onClick: function onClick() {
	              _this2.props.increment();
	            } },
	          'Increment'
	        )
	      );
	    }
	  }]);
	
	  return Counter;
	}(_react.Component);
	
	Counter.propTypes = {
	  count: _react.PropTypes.number.isRequired,
	  increment: _react.PropTypes.func.isRequired
	};
	
	var mapStateToProps = function mapStateToProps(state) {
	  return {
	    count: state.counter.count
	  };
	};
	
	var mapDispatchToProps = function mapDispatchToProps(dispatch) {
	  return {
	    increment: function increment() {
	      dispatch((0, _counter.incrementCount)());
	    }
	  };
	};
	
	exports.default = (0, _reactRedux.connect)(mapStateToProps, mapDispatchToProps)(Counter);

/***/ }),
/* 13 */
/***/ (function(module, exports) {

	'use strict';
	
	Object.defineProperty(exports, "__esModule", {
	  value: true
	});
	// Constant defining the action type
	var INCREMENT_COUNT = 'INCREMENT_COUNT';
	
	// Action creator
	var incrementCount = exports.incrementCount = function incrementCount() {
	  return { type: INCREMENT_COUNT };
	};
	
	var initialState = {
	  count: 0
	};
	
	// The reducer that changes the state based on the action type
	
	exports.default = function () {
	  var state = arguments.length > 0 && arguments[0] !== undefined ? arguments[0] : initialState;
	  var action = arguments[1];
	
	  if (action.type === INCREMENT_COUNT) {
	    return { count: state.count + 1 };
	  }
	
	  return state;
	};

/***/ }),
/* 14 */
/***/ (function(module, exports, __webpack_require__) {

	'use strict';
	
	Object.defineProperty(exports, "__esModule", {
	  value: true
	});
	
	var _redux = __webpack_require__(15);
	
	var _reduxThunk = __webpack_require__(16);
	
	var _reduxThunk2 = _interopRequireDefault(_reduxThunk);
	
	var _reducers = __webpack_require__(17);
	
	var _reducers2 = _interopRequireDefault(_reducers);
	
	function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }
	
	exports.default = function (initialState) {
	  var windowIfDefined = typeof window === 'undefined' ? null : window;
	  var devToolsExtension = windowIfDefined && windowIfDefined.devToolsExtension; // If devTools is installed, connect to it
	  var createStoreWithMiddleware = (0, _redux.compose)((0, _redux.applyMiddleware)(_reduxThunk2.default), devToolsExtension ? devToolsExtension() : function (f) {
	    return f;
	  })(_redux.createStore);
	
	  var store = createStoreWithMiddleware(_reducers2.default, initialState);
	  // Enable Webpack hot module replacement for reducers
	  if (false) {
	    module.hot.accept('./reducers', function () {
	      var nextReducer = require('./reducers').default;
	      store.replaceReducer(nextReducer);
	    });
	  }
	
	  return store;
	};

/***/ }),
/* 15 */
/***/ (function(module, exports) {

	module.exports = require("redux");

/***/ }),
/* 16 */
/***/ (function(module, exports) {

	module.exports = require("redux-thunk");

/***/ }),
/* 17 */
/***/ (function(module, exports, __webpack_require__) {

	'use strict';
	
	Object.defineProperty(exports, "__esModule", {
	  value: true
	});
	
	var _redux = __webpack_require__(15);
	
	var _reactRouterRedux = __webpack_require__(18);
	
	var _counter = __webpack_require__(13);
	
	var _counter2 = _interopRequireDefault(_counter);
	
	var _weatherForecasts = __webpack_require__(10);
	
	var _weatherForecasts2 = _interopRequireDefault(_weatherForecasts);
	
	function _interopRequireDefault(obj) { return obj && obj.__esModule ? obj : { default: obj }; }
	
	exports.default = (0, _redux.combineReducers)({
	  routing: _reactRouterRedux.routerReducer,
	  counter: _counter2.default,
	  weatherForecasts: _weatherForecasts2.default
	});

/***/ }),
/* 18 */
/***/ (function(module, exports) {

	module.exports = require("react-router-redux");

/***/ })
/******/ ])));
//# sourceMappingURL=data:application/json;charset=utf-8;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbIndlYnBhY2s6Ly8vd2VicGFjay9ib290c3RyYXAgYzUyMDg2YjQ1ZDE2NDM5YjlmYmMiLCJ3ZWJwYWNrOi8vLy4vQ2xpZW50QXBwL2Jvb3Qtc2VydmVyLmpzIiwid2VicGFjazovLy9leHRlcm5hbCBcInJlYWN0XCIiLCJ3ZWJwYWNrOi8vL2V4dGVybmFsIFwicmVhY3QtZG9tL3NlcnZlclwiIiwid2VicGFjazovLy9leHRlcm5hbCBcInJlYWN0LXJlZHV4XCIiLCJ3ZWJwYWNrOi8vL2V4dGVybmFsIFwicmVhY3Qtcm91dGVyXCIiLCJ3ZWJwYWNrOi8vLy4vQ2xpZW50QXBwL3JvdXRlcy5qcyIsIndlYnBhY2s6Ly8vLi9DbGllbnRBcHAvY29tcG9uZW50cy9sYXlvdXQuanN4Iiwid2VicGFjazovLy8uL0NsaWVudEFwcC9jb21wb25lbnRzL05hdk1lbnUuanN4Iiwid2VicGFjazovLy8uL0NsaWVudEFwcC9jb21wb25lbnRzL2hvbWUuanN4Iiwid2VicGFjazovLy8uL0NsaWVudEFwcC9jb21wb25lbnRzL2ZldGNoRGF0YS5qc3giLCJ3ZWJwYWNrOi8vLy4vQ2xpZW50QXBwL3JlZHVjZXJzL3dlYXRoZXJGb3JlY2FzdHMuanMiLCJ3ZWJwYWNrOi8vL2V4dGVybmFsIFwiZG9tYWluLXRhc2svZmV0Y2hcIiIsIndlYnBhY2s6Ly8vLi9DbGllbnRBcHAvY29tcG9uZW50cy9jb3VudGVyLmpzeCIsIndlYnBhY2s6Ly8vLi9DbGllbnRBcHAvcmVkdWNlcnMvY291bnRlci5qcyIsIndlYnBhY2s6Ly8vLi9DbGllbnRBcHAvY29uZmlndXJlU3RvcmUuanMiLCJ3ZWJwYWNrOi8vL2V4dGVybmFsIFwicmVkdXhcIiIsIndlYnBhY2s6Ly8vZXh0ZXJuYWwgXCJyZWR1eC10aHVua1wiIiwid2VicGFjazovLy8uL0NsaWVudEFwcC9yZWR1Y2Vycy9pbmRleC5qcyIsIndlYnBhY2s6Ly8vZXh0ZXJuYWwgXCJyZWFjdC1yb3V0ZXItcmVkdXhcIiJdLCJuYW1lcyI6WyJwYXJhbXMiLCJQcm9taXNlIiwicmVzb2x2ZSIsInJlamVjdCIsInJvdXRlcyIsImxvY2F0aW9uIiwiZXJyb3IiLCJyZWRpcmVjdExvY2F0aW9uIiwicmVuZGVyUHJvcHMiLCJyZWRpcmVjdFVybCIsInBhdGhuYW1lIiwiRXJyb3IiLCJ1cmwiLCJzdG9yZSIsImFwcCIsImRvbWFpblRhc2tzIiwidGhlbiIsImh0bWwiLCJnbG9iYWxzIiwiaW5pdGlhbFJlZHV4U3RhdGUiLCJnZXRTdGF0ZSIsIkxheW91dCIsImJvZHkiLCJIb21lIiwiQ291bnRlciIsIkZldGNoRGF0YSIsInByb3BzIiwiQ29tcG9uZW50IiwicHJvcFR5cGVzIiwiUHJvcFR5cGVzIiwiZWxlbWVudCIsIk5hdk1lbnUiLCJzdGFydERhdGVJbmRleCIsInBhcnNlSW50IiwicmVxdWVzdFdlYXRoZXJGb3JlY2FzdHMiLCJuZXh0UHJvcHMiLCJ0YWJsZSIsImZvcmVjYXN0cyIsIm1hcCIsImZvcmVjYXN0IiwiZGF0ZUZvcm1hdHRlZCIsInRlbXBlcmF0dXJlQyIsInRlbXBlcmF0dXJlRiIsInN1bW1hcnkiLCJwcmV2U3RhcnREYXRlSW5kZXgiLCJuZXh0U3RhcnREYXRlSW5kZXgiLCJwYWdpbmF0aW9uIiwiaXNMb2FkaW5nIiwibnVtYmVyIiwiYm9vbCIsImZ1bmMiLCJpc1JlcXVpcmVkIiwibWFwU3RhdGVUb1Byb3BzIiwic3RhdGUiLCJ3ZWF0aGVyRm9yZWNhc3RzIiwibWFwRGlzcGF0Y2hUb1Byb3BzIiwiZGlzcGF0Y2giLCJSRVFVRVNUX1dFQVRIRVJfRk9SRUNBU1RTIiwiUkVDRUlWRV9XRUFUSEVSX0ZPUkVDQVNUUyIsInR5cGUiLCJwYXlsb2FkIiwicmVzcG9uc2UiLCJqc29uIiwiZGF0YSIsInJlY2VpdmVXZWF0aGVyRm9yZWNhc3RzIiwiaW5pdGlhbFN0YXRlIiwiYWN0aW9uIiwiY291bnQiLCJpbmNyZW1lbnQiLCJjb3VudGVyIiwiSU5DUkVNRU5UX0NPVU5UIiwiaW5jcmVtZW50Q291bnQiLCJ3aW5kb3dJZkRlZmluZWQiLCJ3aW5kb3ciLCJkZXZUb29sc0V4dGVuc2lvbiIsImNyZWF0ZVN0b3JlV2l0aE1pZGRsZXdhcmUiLCJ0aHVua01pZGRsZXdhcmUiLCJmIiwiY3JlYXRlU3RvcmUiLCJyb290UmVkdWNlciIsIm1vZHVsZSIsImhvdCIsImFjY2VwdCIsIm5leHRSZWR1Y2VyIiwicmVxdWlyZSIsImRlZmF1bHQiLCJyZXBsYWNlUmVkdWNlciIsInJvdXRpbmciLCJyb3V0ZXJSZWR1Y2VyIl0sIm1hcHBpbmdzIjoiO0FBQUE7QUFDQTs7QUFFQTtBQUNBOztBQUVBO0FBQ0E7QUFDQTs7QUFFQTtBQUNBO0FBQ0EsdUJBQWU7QUFDZjtBQUNBO0FBQ0E7O0FBRUE7QUFDQTs7QUFFQTtBQUNBOztBQUVBO0FBQ0E7QUFDQTs7O0FBR0E7QUFDQTs7QUFFQTtBQUNBOztBQUVBO0FBQ0E7O0FBRUE7QUFDQTs7Ozs7Ozs7Ozs7OztBQ3RDQTs7OztBQUNBOztBQUNBOztBQUNBOztBQUVBOzs7O0FBQ0E7Ozs7OzttQkFFZSxVQUFDQSxNQUFELEVBQVk7QUFDekIsVUFBTyxJQUFJQyxPQUFKLENBQVksVUFBQ0MsT0FBRCxFQUFVQyxNQUFWLEVBQXFCO0FBQ3RDLDZCQUFNLEVBQUNDLHdCQUFELEVBQVNDLFVBQVVMLE9BQU9LLFFBQTFCLEVBQU4sRUFBMkMsVUFBQ0MsS0FBRCxFQUFRQyxnQkFBUixFQUEwQkMsV0FBMUIsRUFBMEM7QUFDbkYsV0FBSUYsS0FBSixFQUFXO0FBQ1QsZUFBTUEsS0FBTjtBQUNEOztBQUVEO0FBQ0EsV0FBSUMsZ0JBQUosRUFBc0I7QUFDcEJMLGlCQUFRLEVBQUVPLGFBQWFGLGlCQUFpQkcsUUFBaEMsRUFBUjtBQUNEOztBQUVEO0FBQ0EsV0FBSSxDQUFDRixXQUFMLEVBQWtCO0FBQ2hCLGVBQU0sSUFBSUcsS0FBSixxQkFBNEJYLE9BQU9ZLEdBQW5DLDZEQUFOO0FBQ0Q7O0FBRUQ7QUFDQTtBQUNBLFdBQU1DLFFBQVEsK0JBQWQ7QUFDRSxXQUFNQyxNQUNKO0FBQUMsNkJBQUQ7QUFBQSxXQUFVLE9BQVFELEtBQWxCO0FB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QUVILE9BQU9kLE1BQU1jLEtBQU4sR0FBYyxDQUF2QixFQUFQO0FBQ0Q7O0FBRUQsVUFBT2QsS0FBUDtBQUNELEU7Ozs7Ozs7Ozs7OztBQ2pCRDs7QUFDQTs7OztBQUNBOzs7Ozs7bUJBRWUsVUFBQ1ksWUFBRCxFQUFrQjtBQUMvQixPQUFNTyxrQkFBa0IsT0FBT0MsTUFBUCxLQUFrQixXQUFsQixHQUFnQyxJQUFoQyxHQUF1Q0EsTUFBL0Q7QUFDQSxPQUFNQyxvQkFBb0JGLG1CQUFtQkEsZ0JBQWdCRSxpQkFBN0QsQ0FGK0IsQ0FFaUQ7QUFDaEYsT0FBTUMsNEJBQTRCLG9CQUNoQyw0QkFBZ0JDLG9CQUFoQixDQURnQyxFQUVoQ0Ysb0JBQW9CQSxtQkFBcEIsR0FBMEM7QUFBQSxZQUFLRyxDQUFMO0FBQUEsSUFGVixFQUdoQ0Msa0JBSGdDLENBQWxDOztBQUtBLE9BQU1qRSxRQUFROEQsMEJBQTBCSSxrQkFBMUIsRUFBdUNkLFlBQXZDLENBQWQ7QUFDQTtBQUNBLE9BQUksS0FBSixFQUFnQjtBQUNkZSxZQUFPQyxHQUFQLENBQVdDLE1BQVgsQ0FBa0IsWUFBbEIsRUFBZ0MsWUFBTTtBQUNwQyxXQUFNQyxjQUFjQyxRQUFRLFlBQVIsRUFBc0JDLE9BQTFDO0FBQ0F4RSxhQUFNeUUsY0FBTixDQUFxQkgsV0FBckI7QUFDRCxNQUhEO0FBSUQ7O0FBRUQsVUFBT3RFLEtBQVA7QUFDRCxFOzs7Ozs7QUN0QkQsbUM7Ozs7OztBQ0FBLHlDOzs7Ozs7Ozs7Ozs7QUNBQTs7QUFDQTs7QUFFQTs7OztBQUNBOzs7Ozs7bUJBRWUsNEJBQWdCO0FBQzdCMEUsWUFBU0MsK0JBRG9CO0FBRTdCbkIsNkJBRjZCO0FBRzdCZjtBQUg2QixFQUFoQixDOzs7Ozs7QUNOZixnRCIsImZpbGUiOiJib290LXNlcnZlci5qcyIsInNvdXJjZXNDb250ZW50IjpbIiBcdC8vIFRoZSBtb2R1bGUgY2FjaGVcbiBcdHZhciBpbnN0YWxsZWRNb2R1bGVzID0ge307XG5cbiBcdC8vIFRoZSByZXF1aXJlIGZ1bmN0aW9uXG4gXHRmdW5jdGlvbiBfX3dlYnBhY2tfcmVxdWlyZV9fKG1vZHVsZUlkKSB7XG5cbiBcdFx0Ly8gQ2hlY2sgaWYgbW9kdWxlIGlzIGluIGNhY2hlXG4gXHRcdGlmKGluc3RhbGxlZE1vZHVsZXNbbW9kdWxlSWRdKVxuIFx0XHRcdHJldHVybiBpbnN0YWxsZWRNb2R1bGVzW21vZHVsZUlkXS5leHBvcnRzO1xuXG4gXHRcdC8vIENyZWF0ZSBhIG5ldyBtb2R1bGUgKGFuZCBwdXQgaXQgaW50byB0aGUgY2FjaGUpXG4gXHRcdHZhciBtb2R1bGUgPSBpbnN0YWxsZWRNb2R1bGVzW21vZHVsZUlkXSA9IHtcbiBcdFx0XHRleHBvcnRzOiB7fSxcbiBcdFx0XHRpZDogbW9kdWxlSWQsXG4gXHRcdFx0bG9hZGVkOiBmYWxzZVxuIFx0XHR9O1xuXG4gXHRcdC8vIEV4ZWN1dGUgdGhlIG1vZHVsZSBmdW5jdGlvblxuIFx0XHRtb2R1bGVzW21vZHVsZUlkXS5jYWxsKG1vZHVsZS5leHBvcnRzLCBtb2R1bGUsIG1vZHVsZS5leHBvcnRzLCBfX3dlYnBhY2tfcmVxdWlyZV9fKTtcblxuIFx0XHQvLyBGbGFnIHRoZSBtb2R1bGUgYXMgbG9hZGVkXG4gXHRcdG1vZHVsZS5sb2FkZWQgPSB0cnVlO1xuXG4gXHRcdC8vIFJldHVybiB0aGUgZXhwb3J0cyBvZiB0aGUgbW9kdWxlXG4gXHRcdHJldHVybiBtb2R1bGUuZXhwb3J0cztcbiBcdH1cblxuXG4gXHQvLyBleHBvc2UgdGhlIG1vZHVsZXMgb2JqZWN0IChfX3dlYnBhY2tfbW9kdWxlc19fKVxuIFx0X193ZWJwYWNrX3JlcXVpcmVfXy5tID0gbW9kdWxlcztcblxuIFx0Ly8gZXhwb3NlIHRoZSBtb2R1bGUgY2FjaGVcbiBcdF9fd2VicGFja19yZXF1aXJlX18uYyA9IGluc3RhbGxlZE1vZHVsZXM7XG5cbiBcdC8vIF9fd2VicGFja19wdWJsaWNfcGF0aF9fXG4gXHRfX3dlYnBhY2tfcmVxdWlyZV9fLnAgPSBcIlwiO1xuXG4gXHQvLyBMb2FkIGVudHJ5IG1vZHVsZSBhbmQgcmV0dXJuIGV4cG9ydHNcbiBcdHJldHVybiBfX3dlYnBhY2tfcmVxdWlyZV9fKDApO1xuXG5cblxuLy8gV0VCUEFDSyBGT09URVIgLy9cbi8vIHdlYnBhY2svYm9vdHN0cmFwIGM1MjA4NmI0NWQxNjQzOWI5ZmJjIiwiaW1wb3J0IFJlYWN0IGZyb20gJ3JlYWN0JztcbmltcG9ydCB7IHJlbmRlclRvU3RyaW5nIH0gZnJvbSAncmVhY3QtZG9tL3NlcnZlcic7XG5pbXBvcnQgeyBQcm92aWRlciB9IGZyb20gJ3JlYWN0LXJlZHV4JztcbmltcG9ydCB7IG1hdGNoLCBSb3V0ZXJDb250ZXh0IH0gZnJvbSAncmVhY3Qtcm91dGVyJztcblxuaW1wb3J0IHJvdXRlcyBmcm9tICcuL3JvdXRlcyc7XG5pbXBvcnQgY29uZmlndXJlU3RvcmUgZnJvbSAnLi9jb25maWd1cmVTdG9yZSc7XG5cbmV4cG9ydCBkZWZhdWx0IChwYXJhbXMpID0+IHtcbiAgcmV0dXJuIG5ldyBQcm9taXNlKChyZXNvbHZlLCByZWplY3QpID0+IHtcbiAgICBtYXRjaCh7cm91dGVzLCBsb2NhdGlvbjogcGFyYW1zLmxvY2F0aW9ufSwgKGVycm9yLCByZWRpcmVjdExvY2F0aW9uLCByZW5kZXJQcm9wcykgPT4ge1xuICAgICAgaWYgKGVycm9yKSB7XG4gICAgICAgIHRocm93IGVycm9yO1xuICAgICAgfVxuXG4gICAgICAvLyBJZiB0aGVyZSdzIGEgcmVkaXJlY3Rpb24sIGp1c3Qgc2VuZCB0aGlzIGluZm9ybWF0aW9uIGJhY2sgdG8gdGhlIGhvc3QgYXBwbGljYXRpb25cbiAgICAgIGlmIChyZWRpcmVjdExvY2F0aW9uKSB7XG4gICAgICAgIHJlc29sdmUoeyByZWRpcmVjdFVybDogcmVkaXJlY3RMb2NhdGlvbi5wYXRobmFtZSB9KTtcbiAgICAgIH1cblxuICAgICAgLy8gSWYgaXQgZGlkbid0IG1hdGNoIGFueSByb3V0ZSwgcmVuZGVyUHJvcHMgd2lsbCBiZSB1bmRlZmluZWRcbiAgICAgIGlmICghcmVuZGVyUHJvcHMpIHtcbiAgICAgICAgdGhyb3cgbmV3IEVycm9yKGBUaGUgbG9jYXRpb24gJyR7IHBhcmFtcy51cmwgfScgZG9lc24ndCBtYXRjaCBhbnkgcm91dGUgY29uZmlndXJlZCBpbiByZWFjdC1yb3V0ZXIuYCk7XG4gICAgICB9XG5cbiAgICAgIC8vIEF0IHRoaXMgcG9pbnQgaWYgd2Ugd2FudCB0byBpbml0aWFsaXplIHRoZSBzdG9yZSB3ZSBuZWVkIHRvIHBhc3MgYW4gb2JqZWN0IHdpdGggc2hhcGVcbiAgICAgIC8vIHtjb3VudGVyOiB7Y291bnQ6IDEwfX1cbiAgICAgIGNvbnN0IHN0b3JlID0gY29uZmlndXJlU3RvcmUoKTtcbiAgICAgICAgY29uc3QgYXBwID0gKFxuICAgICAgICAgIDxQcm92aWRlciBzdG9yZT17IHN0b3JlIH0+XG4gICAgICAgICAgICA8Um91dGVyQ29udGV4dCB7Li4ucmVuZGVyUHJvcHN9IC8+XG4gICAgICAgICAgPC9Qcm92aWRlcj5cbiAgICAgICk7XG5cbiAgICAgIC8vIFBlcmZvcm0gYW4gaW5pdGlhbCByZW5kZXIgdGhhdCB3aWxsIGNhdXNlIGFueSBhc3luYyB0YXNrcyAoZS5nLiwgZGF0YSBhY2Nlc3MpIHRvIGJlZ2luXG4gICAgICByZW5kZXJUb1N0cmluZyhhcHApO1xuXG4gICAgICAvLyBPbmNlIHRoZSB0YXNrcyBhcmUgZG9uZSwgd2UgY2FuIHBlcmZvcm0gdGhlIGZpbmFsIHJlbmRlclxuICAgICAgLy8gV2UgYWxzbyBzZW5kIHRoZSByZWR1eCBzdG9yZSBzdGF0ZSwgc28gdGhlIGNsaWVudCBjYW4gY29udGludWUgZXhlY3V0aW9uIHdoZXJlIHRoZSBzZXJ2ZXIgbGVmdCBvZmZcbiAgICAgIHBhcmFtcy5kb21haW5UYXNrcy50aGVuKCgpID0+IHtcbiAgICAgICAgcmVzb2x2ZSh7XG4gICAgICAgICAgICBodG1sOiByZW5kZXJUb1N0cmluZyhhcHApLFxuICAgICAgICAgICAgZ2xvYmFsczogeyBpbml0aWFsUmVkdXhTdGF0ZTogc3RvcmUuZ2V0U3RhdGUoKSB9XG4gICAgICAgIH0pO1xuICAgICAgfSwgcmVqZWN0KTsgLy8gQWxzbyBwcm9wYWdhdGUgYW55IGVycm9ycyBiYWNrIGludG8gdGhlIGhvc3QgYXBwbGljYXRpb25cbiAgICB9KTtcbiAgfSk7XG59O1xuXG5cblxuLy8gV0VCUEFDSyBGT09URVIgLy9cbi8vIC4vQ2xpZW50QXBwL2Jvb3Qtc2VydmVyLmpzIiwibW9kdWxlLmV4cG9ydHMgPSByZXF1aXJlKFwicmVhY3RcIik7XG5cblxuLy8vLy8vLy8vLy8vLy8vLy8vXG4vLyBXRUJQQUNLIEZPT1RFUlxuLy8gZXh0ZXJuYWwgXCJyZWFjdFwiXG4vLyBtb2R1bGUgaWQgPSAxXG4vLyBtb2R1bGUgY2h1bmtzID0gMCIsIm1vZHVsZS5leHBvcnRzID0gcmVxdWlyZShcInJlYWN0LWRvbS9zZXJ2ZXJcIik7XG5cblxuLy8vLy8vLy8vLy8vLy8vLy8vXG4vLyBXRUJQQUNLIEZPT1RFUlxuLy8gZXh0ZXJuYWwgXCJyZWFjdC1kb20vc2VydmVyXCJcbi8vIG1vZHVsZSBpZCA9IDJcbi8vIG1vZHVsZSBjaHVua3MgPSAwIiwibW9kdWxlLmV4cG9ydHMgPSByZXF1aXJlKFwicmVhY3QtcmVkdXhcIik7XG5cblxuLy8vLy8vLy8vLy8vLy8vLy8vXG4vLyBXRUJQQUNLIEZPT1RFUlxuLy8gZXh0ZXJuYWwgXCJyZWFjdC1yZWR1eFwiXG4vLyBtb2R1bGUgaWQgPSAzXG4vLyBtb2R1bGUgY2h1bmtzID0gMCIsIm1vZHVsZS5leHBvcnRzID0gcmVxdWlyZShcInJlYWN0LXJvdXRlclwiKTtcblxuXG4vLy8vLy8vLy8vLy8vLy8vLy9cbi8vIFdFQlBBQ0sgRk9PVEVSXG4vLyBleHRlcm5hbCBcInJlYWN0LXJvdXRlclwiXG4vLyBtb2R1bGUgaWQgPSA0XG4vLyBtb2R1bGUgY2h1bmtzID0gMCIsImltcG9ydCBSZWFjdCBmcm9tICdyZWFjdCc7XG5pbXBvcnQgeyBSb3V0ZSB9IGZyb20gJ3JlYWN0LXJvdXRlcic7XG5pbXBvcnQgTGF5b3V0IGZyb20gJy4vY29tcG9uZW50cy9sYXlvdXQnO1xuaW1wb3J0IEhvbWUgZnJvbSAnLi9jb21wb25lbnRzL2hvbWUnO1xuaW1wb3J0IEZldGNoRGF0YSBmcm9tICcuL2NvbXBvbmVudHMvZmV0Y2hEYXRhJztcbmltcG9ydCBDb3VudGVyIGZyb20gJy4vY29tcG9uZW50cy9jb3VudGVyJztcblxuZXhwb3J0IGRlZmF1bHRcbiAgPFJvdXRlIGNvbXBvbmVudD17TGF5b3V0fT5cbiAgICA8Um91dGUgcGF0aD0nLycgY29tcG9uZW50cz17e2JvZHk6IEhvbWV9fSAvPlxuICAgIDxSb3V0ZSBwYXRoPScvY291bnRlcicgY29tcG9uZW50cz17eyBib2R5OiBDb3VudGVyIH19IC8+XG4gICAgPFJvdXRlIHBhdGg9Jy9mZXRjaGRhdGEnIGNvbXBvbmVudHM9e3sgYm9keTogRmV0Y2hEYXRhIH19PlxuICAgICAgPFJvdXRlIHBhdGg9JzpzdGFydERhdGVJbmRleCcgLz4geyAvKiBPcHRpb25hbCByb3V0ZSBzZWdtZW50IHRoYXQgZG9lcyBub3QgYWZmZWN0IE5hdk1lbnUgaGlnaGxpZ2h0aW5nICovIH1cbiAgICA8L1JvdXRlPlxuICA8L1JvdXRlPjtcblxuXG5cbi8vIFdFQlBBQ0sgRk9PVEVSIC8vXG4vLyAuL0NsaWVudEFwcC9yb3V0ZXMuanMiLCJpbXBvcnQgUmVhY3QsIHtDb21wb25lbnQsIFByb3BUeXBlc30gZnJvbSAncmVhY3QnO1xuaW1wb3J0IE5hdk1lbnUgZnJvbSAnLi9OYXZNZW51JztcblxuY2xhc3MgTGF5b3V0IGV4dGVuZHMgQ29tcG9uZW50IHtcbiAgcmVuZGVyKCkge1xuICAgIHJldHVybiAoXG4gICAgICA8ZGl2IGNsYXNzTmFtZT0nY29udGFpbmVyLWZsdWlkJz5cbiAgICAgICAgPGRpdiBjbGFzc05hbWU9J3Jvdyc+XG4gICAgICAgICAgPGRpdiBjbGFzc05hbWU9J2NvbC1zbS0zJz5cbiAgICAgICAgICAgIDxOYXZNZW51IC8+XG4gICAgICAgICAgPC9kaXY+XG4gICAgICAgICAgPGRpdiBjbGFzc05hbWU9J2NvbC1zbS05Jz5cbiAgICAgICAgICAgIHt0aGlzLnByb3BzLmJvZHl9XG4gICAgICAgICAgPC9kaXY+XG4gICAgICAgIDwvZGl2PlxuICAgICAgPC9kaXY+XG4gICAgKTtcbiAgfVxufVxuXG5MYXlvdXQucHJvcFR5cGVzID0ge1xuICBib2R5OiBQcm9wVHlwZXMuZWxlbWVudCxcbn07XG5cbmV4cG9ydCBkZWZhdWx0IExheW91dDtcblxuXG5cbi8vIFdFQlBBQ0sgRk9PVEVSIC8vXG4vLyAuL0NsaWVudEFwcC9jb21wb25lbnRzL2xheW91dC5qc3giLCJpbXBvcnQgUmVhY3QsIHtDb21wb25lbnR9IGZyb20gJ3JlYWN0JztcbmltcG9ydCB7IExpbmsgfSBmcm9tICdyZWFjdC1yb3V0ZXInO1xuXG5jbGFzcyBOYXZNZW51IGV4dGVuZHMgQ29tcG9uZW50IHtcbiAgcmVuZGVyKCkge1xuICAgIHJldHVybiAoXG4gICAgICA8ZGl2IGNsYXNzTmFtZT0nbWFpbi1uYXYnPlxuICAgICAgICA8ZGl2IGNsYXNzTmFtZT0nbmF2YmFyIG5hdmJhci1pbnZlcnNlJz5cbiAgICAgICAgICA8ZGl2IGNsYXNzTmFtZT0nbmF2YmFyLWhlYWRlcic+XG4gICAgICAgICAgICA8YnV0dG9uIHR5cGU9J2J1dHRvbicgY2xhc3NOYW1lPSduYXZiYXItdG9nZ2xlJyBkYXRhLXRvZ2dsZT0nY29sbGFwc2UnIGRhdGEtdGFyZ2V0PScubmF2YmFyLWNvbGxhcHNlJz5cbiAgICAgICAgICAgICAgPHNwYW4gY2xhc3NOYW1lPSdzci1vbmx5Jz5Ub2dnbGUgbmF2aWdhdGlvbjwvc3Bhbj5cbiAgICAgICAgICAgICAgPHNwYW4gY2xhc3NOYW1lPSdpY29uLWJhcic+PC9zcGFuPlxuICAgICAgICAgICAgICA8c3BhbiBjbGFzc05hbWU9J2ljb24tYmFyJz48L3NwYW4+XG4gICAgICAgICAgICAgIDxzcGFuIGNsYXNzTmFtZT0naWNvbi1iYXInPjwvc3Bhbj5cbiAgICAgICAgICAgIDwvYnV0dG9uPlxuICAgICAgICAgICAgPExpbmsgY2xhc3NOYW1lPSduYXZiYXItYnJhbmQnIHRvPXsgJy8nIH0+V2ViQXBwbGljYXRpb25CYXNpYzwvTGluaz5cbiAgICAgICAgICA8L2Rpdj5cbiAgICAgICAgICA8ZGl2IGNsYXNzTmFtZT0nY2xlYXJmaXgnPjwvZGl2PlxuICAgICAgICAgIDxkaXYgY2xhc3NOYW1lPSduYXZiYXItY29sbGFwc2UgY29sbGFwc2UnPlxuICAgICAgICAgICAgPHVsIGNsYXNzTmFtZT0nbmF2IG5hdmJhci1uYXYnPlxuICAgICAgICAgICAgICA8bGk+XG4gICAgICAgICAgICAgICAgPExpbmsgdG89eyAnLycgfSBhY3RpdmVDbGFzc05hbWU9J2FjdGl2ZSc+XG4gICAgICAgICAgICAgICAgICA8c3BhbiBjbGFzc05hbWU9J2dseXBoaWNvbiBnbHlwaGljb24taG9tZSc+PC9zcGFuPiBIb21lXG4gICAgICAgICAgICAgICAgPC9MaW5rPlxuICAgICAgICAgICAgICA8L2xpPlxuICAgICAgICAgICAgICA8bGk+XG4gICAgICAgICAgICAgICAgPExpbmsgdG89eyAnL2NvdW50ZXInIH0gYWN0aXZlQ2xhc3NOYW1lPSdhY3RpdmUnPlxuICAgICAgICAgICAgICAgICAgPHNwYW4gY2xhc3NOYW1lPSdnbHlwaGljb24gZ2x5cGhpY29uLWVkdWNhdGlvbic+PC9zcGFuPiBDb3VudGVyXG4gICAgICAgICAgICAgICAgPC9MaW5rPlxuICAgICAgICAgICAgICA8L2xpPlxuICAgICAgICAgICAgICA8bGk+XG4gICAgICAgICAgICAgICAgPExpbmsgdG89eyAnL2ZldGNoZGF0YScgfSBhY3RpdmVDbGFzc05hbWU9J2FjdGl2ZSc+XG4gICAgICAgICAgICAgICAgICA8c3BhbiBjbGFzc05hbWU9J2dseXBoaWNvbiBnbHlwaGljb24tdGgtbGlzdCc+PC9zcGFuPiBGZXRjaCBkYXRhXG4gICAgICAgICAgICAgICAgPC9MaW5rPlxuICAgICAgICAgICAgICA8L2xpPlxuICAgICAgICAgICAgPC91bD5cbiAgICAgICAgICA8L2Rpdj5cbiAgICAgICAgPC9kaXY+XG4gICAgICA8L2Rpdj5cbiAgICApO1xuICB9XG59XG5cbmV4cG9ydCBkZWZhdWx0IE5hdk1lbnU7XG5cblxuXG4vLyBXRUJQQUNLIEZPT1RFUiAvL1xuLy8gLi9DbGllbnRBcHAvY29tcG9uZW50cy9OYXZNZW51LmpzeCIsImltcG9ydCBSZWFjdCwge0NvbXBvbmVudH0gZnJvbSAncmVhY3QnO1xuXG5jbGFzcyBIb21lIGV4dGVuZHMgQ29tcG9uZW50IHtcbiAgcmVuZGVyKCkge1xuICAgIHJldHVybiAoXG4gICAgICA8ZGl2PlxuICAgICAgICA8aDE+SGVsbG8sIHdvcmxkITwvaDE+XG4gICAgICAgIDxwPldlbGNvbWUgdG8geW91ciBuZXcgc2luZ2xlLXBhZ2UgYXBwbGljYXRpb24sIGJ1aWx0IHdpdGg6PC9wPlxuICAgICAgICA8dWw+XG4gICAgICAgICAgPGxpPjxhIGhyZWY9J2h0dHBzOi8vZ2V0LmFzcC5uZXQvJz5BU1AuTkVUIENvcmU8L2E+IGFuZCA8YSBocmVmPSdodHRwczovL21zZG4ubWljcm9zb2Z0LmNvbS9lbi11cy9saWJyYXJ5LzY3ZWY4c2JkLmFzcHgnPkMjPC9hPiBmb3IgY3Jvc3MtcGxhdGZvcm0gc2VydmVyLXNpZGUgY29kZTwvbGk+XG4gICAgICAgICAgPGxpPjxhIGhyZWY9J2h0dHBzOi8vZmFjZWJvb2suZ2l0aHViLmlvL3JlYWN0Lyc+UmVhY3Q8L2E+LCA8YSBocmVmPSdodHRwOi8vcmVkdXguanMub3JnJz5SZWR1eDwvYT4sIGFuZCA8YSBocmVmPSdodHRwOi8vd3d3LnR5cGVzY3JpcHRsYW5nLm9yZy8nPlR5cGVTY3JpcHQ8L2E+IGZvciBjbGllbnQtc2lkZSBjb2RlPC9saT5cbiAgICAgICAgICA8bGk+PGEgaHJlZj0naHR0cHM6Ly93ZWJwYWNrLmdpdGh1Yi5pby8nPldlYnBhY2s8L2E+IGZvciBidWlsZGluZyBhbmQgYnVuZGxpbmcgY2xpZW50LXNpZGUgcmVzb3VyY2VzPC9saT5cbiAgICAgICAgICA8bGk+PGEgaHJlZj0naHR0cDovL2dldGJvb3RzdHJhcC5jb20vJz5Cb290c3RyYXA8L2E+IGZvciBsYXlvdXQgYW5kIHN0eWxpbmc8L2xpPlxuICAgICAgICA8L3VsPlxuICAgICAgICA8cD5UbyBoZWxwIHlvdSBnZXQgc3RhcnRlZCwgd2UndmUgYWxzbyBzZXQgdXA6PC9wPlxuICAgICAgICA8dWw+XG4gICAgICAgICAgPGxpPjxzdHJvbmc+Q2xpZW50LXNpZGUgbmF2aWdhdGlvbjwvc3Ryb25nPi4gRm9yIGV4YW1wbGUsIGNsaWNrIDxlbT5Db3VudGVyPC9lbT4gdGhlbiA8ZW0+QmFjazwvZW0+IHRvIHJldHVybiBoZXJlLjwvbGk+XG4gICAgICAgICAgPGxpPjxzdHJvbmc+V2VicGFjayBkZXYgbWlkZGxld2FyZTwvc3Ryb25nPi4gSW4gZGV2ZWxvcG1lbnQgbW9kZSwgdGhlcmUncyBubyBuZWVkIHRvIHJ1biB0aGUgPGNvZGU+d2VicGFjazwvY29kZT4gYnVpbGQgdG9vbC4gWW91ciBjbGllbnQtc2lkZSByZXNvdXJjZXMgYXJlIGR5bmFtaWNhbGx5IGJ1aWx0IG9uIGRlbWFuZC4gVXBkYXRlcyBhcmUgYXZhaWxhYmxlIGFzIHNvb24gYXMgeW91IG1vZGlmeSBhbnkgZmlsZS48L2xpPlxuICAgICAgICAgIDxsaT48c3Ryb25nPkhvdCBtb2R1bGUgcmVwbGFjZW1lbnQ8L3N0cm9uZz4uIEluIGRldmVsb3BtZW50IG1vZGUsIHlvdSBkb24ndCBldmVuIG5lZWQgdG8gcmVsb2FkIHRoZSBwYWdlIGFmdGVyIG1ha2luZyBtb3N0IGNoYW5nZXMuIFdpdGhpbiBzZWNvbmRzIG9mIHNhdmluZyBjaGFuZ2VzIHRvIGZpbGVzLCByZWJ1aWx0IENTUyBhbmQgUmVhY3QgY29tcG9uZW50cyB3aWxsIGJlIGluamVjdGVkIGRpcmVjdGx5IGludG8geW91ciBydW5uaW5nIGFwcGxpY2F0aW9uLCBwcmVzZXJ2aW5nIGl0cyBsaXZlIHN0YXRlLjwvbGk+XG4gICAgICAgICAgPGxpPjxzdHJvbmc+RWZmaWNpZW50IHByb2R1Y3Rpb24gYnVpbGRzPC9zdHJvbmc+LiBJbiBwcm9kdWN0aW9uIG1vZGUsIGRldmVsb3BtZW50LXRpbWUgZmVhdHVyZXMgYXJlIGRpc2FibGVkLCBhbmQgdGhlIDxjb2RlPndlYnBhY2s8L2NvZGU+IGJ1aWxkIHRvb2wgcHJvZHVjZXMgbWluaWZpZWQgc3RhdGljIENTUyBhbmQgSmF2YVNjcmlwdCBmaWxlcy48L2xpPlxuICAgICAgICAgIDxsaT48c3Ryb25nPlNlcnZlci1zaWRlIHByZXJlbmRlcmluZzwvc3Ryb25nPi4gVG8gb3B0aW1pemUgc3RhcnR1cCB0aW1lLCB5b3VyIFJlYWN0IGFwcGxpY2F0aW9uIGlzIGZpcnN0IHJlbmRlcmVkIG9uIHRoZSBzZXJ2ZXIuIFRoZSBpbml0aWFsIEhUTUwgYW5kIHN0YXRlIGlzIHRoZW4gdHJhbnNmZXJyZWQgdG8gdGhlIGJyb3dzZXIsIHdoZXJlIGNsaWVudC1zaWRlIGNvZGUgcGlja3MgdXAgd2hlcmUgdGhlIHNlcnZlciBsZWZ0IG9mZi48L2xpPlxuICAgICAgICA8L3VsPlxuICAgICAgPC9kaXY+XG4gICAgKTtcbiAgfVxufVxuXG5leHBvcnQgZGVmYXVsdCBIb21lO1xuXG5cblxuLy8gV0VCUEFDSyBGT09URVIgLy9cbi8vIC4vQ2xpZW50QXBwL2NvbXBvbmVudHMvaG9tZS5qc3giLCJpbXBvcnQgUmVhY3QsIHtDb21wb25lbnQsIFByb3BUeXBlc30gZnJvbSAncmVhY3QnO1xuaW1wb3J0IHsgY29ubmVjdCB9IGZyb20gJ3JlYWN0LXJlZHV4JztcbmltcG9ydCB7IExpbmsgfSBmcm9tICdyZWFjdC1yb3V0ZXInO1xuaW1wb3J0IHsgcmVxdWVzdFdlYXRoZXJGb3JlY2FzdHMgfSBmcm9tICcuLi9yZWR1Y2Vycy93ZWF0aGVyRm9yZWNhc3RzJ1xuXG5jbGFzcyBGZXRjaERhdGEgZXh0ZW5kcyBDb21wb25lbnQge1xuICBjb21wb25lbnRXaWxsTW91bnQoKSB7XG4gICAgLy8gVGhpcyBtZXRob2QgcnVucyB3aGVuIHRoZSBjb21wb25lbnQgaXMgZmlyc3QgYWRkZWQgdG8gdGhlIHBhZ2VcbiAgICBsZXQgc3RhcnREYXRlSW5kZXggPSBwYXJzZUludCh0aGlzLnByb3BzLnBhcmFtcy5zdGFydERhdGVJbmRleCkgfHwgMDtcbiAgICB0aGlzLnByb3BzLnJlcXVlc3RXZWF0aGVyRm9yZWNhc3RzKHN0YXJ0RGF0ZUluZGV4KTtcbiAgfVxuXG4gIGNvbXBvbmVudFdpbGxSZWNlaXZlUHJvcHMobmV4dFByb3BzKSB7XG4gICAgLy8gVGhpcyBtZXRob2QgcnVucyB3aGVuIGluY29taW5nIHByb3BzIChlLmcuLCByb3V0ZSBwYXJhbXMpIGNoYW5nZVxuICAgIGxldCBzdGFydERhdGVJbmRleCA9IHBhcnNlSW50KG5leHRQcm9wcy5wYXJhbXMuc3RhcnREYXRlSW5kZXgpIHx8IDA7XG4gICAgdGhpcy5wcm9wcy5yZXF1ZXN0V2VhdGhlckZvcmVjYXN0cyhzdGFydERhdGVJbmRleCk7XG4gIH1cblxuICByZW5kZXIoKSB7XG4gICAgY29uc3QgdGFibGUgPSAoXG4gICAgICA8dGFibGUgY2xhc3NOYW1lPSd0YWJsZSc+XG4gICAgICAgIDx0aGVhZD5cbiAgICAgICAgICA8dHI+XG4gICAgICAgICAgICA8dGg+RGF0ZTwvdGg+XG4gICAgICAgICAgICA8dGg+VGVtcC4gKEMpPC90aD5cbiAgICAgICAgICAgIDx0aD5UZW1wLiAoRik8L3RoPlxuICAgICAgICAgICAgPHRoPlN1bW1hcnk8L3RoPlxuICAgICAgICAgIDwvdHI+XG4gICAgICAgIDwvdGhlYWQ+XG4gICAgICAgIDx0Ym9keT5cbiAgICAgICAgICB7dGhpcy5wcm9wcy5mb3JlY2FzdHMubWFwKGZvcmVjYXN0ID0+XG4gICAgICAgICAgICA8dHIga2V5PXsgZm9yZWNhc3QuZGF0ZUZvcm1hdHRlZCB9PlxuICAgICAgICAgICAgICA8dGQ+eyBmb3JlY2FzdC5kYXRlRm9ybWF0dGVkIH08L3RkPlxuICAgICAgICAgICAgICA8dGQ+eyBmb3JlY2FzdC50ZW1wZXJhdHVyZUMgfTwvdGQ+XG4gICAgICAgICAgICAgIDx0ZD57IGZvcmVjYXN0LnRlbXBlcmF0dXJlRiB9PC90ZD5cbiAgICAgICAgICAgICAgPHRkPnsgZm9yZWNhc3Quc3VtbWFyeSB9PC90ZD5cbiAgICAgICAgICAgIDwvdHI+XG4gICAgICAgICAgKX1cbiAgICAgICAgPC90Ym9keT5cbiAgICAgIDwvdGFibGU+XG4gICAgKTtcblxuICAgIGxldCBwcmV2U3RhcnREYXRlSW5kZXggPSB0aGlzLnByb3BzLnN0YXJ0RGF0ZUluZGV4IC0gNTtcbiAgICBsZXQgbmV4dFN0YXJ0RGF0ZUluZGV4ID0gdGhpcy5wcm9wcy5zdGFydERhdGVJbmRleCArIDU7XG4gICAgY29uc3QgcGFnaW5hdGlvbiA9IChcbiAgICAgIDxwIGNsYXNzTmFtZT0nY2xlYXJmaXggdGV4dC1jZW50ZXInPlxuICAgICAgICA8TGluayBjbGFzc05hbWU9J2J0biBidG4tZGVmYXVsdCBwdWxsLWxlZnQnIHRvPXsgYC9mZXRjaGRhdGEvJHsgcHJldlN0YXJ0RGF0ZUluZGV4IH1gIH0+UHJldmlvdXM8L0xpbms+XG4gICAgICAgIDxMaW5rIGNsYXNzTmFtZT0nYnRuIGJ0bi1kZWZhdWx0IHB1bGwtcmlnaHQnIHRvPXsgYC9mZXRjaGRhdGEvJHsgbmV4dFN0YXJ0RGF0ZUluZGV4IH1gIH0+TmV4dDwvTGluaz5cbiAgICAgICAgeyB0aGlzLnByb3BzLmlzTG9hZGluZyA/IDxzcGFuPkxvYWRpbmcuLi48L3NwYW4+IDogW10gfVxuICAgICAgPC9wPlxuICAgICk7XG5cbiAgICByZXR1cm4gKFxuICAgICAgPGRpdj5cbiAgICAgICAgPGgxPldlYXRoZXIgZm9yZWNhc3Q8L2gxPlxuICAgICAgICA8cD5UaGlzIGNvbXBvbmVudCBkZW1vbnN0cmF0ZXMgZmV0Y2hpbmcgZGF0YSBmcm9tIHRoZSBzZXJ2ZXIgYW5kIHdvcmtpbmcgd2l0aCBVUkwgcGFyYW1ldGVycy48L3A+XG4gICAgICAgIHsgdGFibGUgfVxuICAgICAgICB7IHBhZ2luYXRpb24gfVxuICAgICAgPC9kaXY+XG4gICAgKTtcbiAgfVxufVxuXG5GZXRjaERhdGEucHJvcFR5cGVzID0ge1xuICBzdGFydERhdGVJbmRleDogUHJvcFR5cGVzLm51bWJlcixcbiAgaXNMb2FkaW5nOiBQcm9wVHlwZXMuYm9vbCxcbiAgcmVxdWVzdFdlYXRoZXJGb3JlY2FzdHM6IFByb3BUeXBlcy5mdW5jLmlzUmVxdWlyZWRcbn07XG5cbmNvbnN0IG1hcFN0YXRlVG9Qcm9wcyA9IChzdGF0ZSkgPT4gKHtcbiAgc3RhcnREYXRlSW5kZXg6IHN0YXRlLndlYXRoZXJGb3JlY2FzdHMuc3RhcnREYXRlSW5kZXgsXG4gIGZvcmVjYXN0czogc3RhdGUud2VhdGhlckZvcmVjYXN0cy5mb3JlY2FzdHMsXG4gIGlzTG9hZGluZzogc3RhdGUud2VhdGhlckZvcmVjYXN0cy5pc0xvYWRpbmdcbn0pO1xuXG5jb25zdCBtYXBEaXNwYXRjaFRvUHJvcHMgPSAoZGlzcGF0Y2gpID0+ICh7XG4gIHJlcXVlc3RXZWF0aGVyRm9yZWNhc3RzOiAoc3RhcnREYXRlSW5kZXgpID0+IHtcbiAgICBkaXNwYXRjaChyZXF1ZXN0V2VhdGhlckZvcmVjYXN0cyhzdGFydERhdGVJbmRleCkpO1xuICB9XG59KTtcblxuZXhwb3J0IGRlZmF1bHQgY29ubmVjdChcbiAgbWFwU3RhdGVUb1Byb3BzLFxuICBtYXBEaXNwYXRjaFRvUHJvcHNcbikoRmV0Y2hEYXRhKTtcblxuXG5cbi8vIFdFQlBBQ0sgRk9PVEVSIC8vXG4vLyAuL0NsaWVudEFwcC9jb21wb25lbnRzL2ZldGNoRGF0YS5qc3giLCJpbXBvcnQgeyBmZXRjaCB9IGZyb20gJ2RvbWFpbi10YXNrL2ZldGNoJztcblxuLy8gQ29uc3RhbnQgZGVmaW5pbmcgdGhlIGFjdGlvbiB0eXBlXG5jb25zdCBSRVFVRVNUX1dFQVRIRVJfRk9SRUNBU1RTID0gJ1JFUVVFU1RfV0VBVEhFUl9GT1JFQ0FTVFMnO1xuY29uc3QgUkVDRUlWRV9XRUFUSEVSX0ZPUkVDQVNUUyA9ICdSRUNFSVZFX1dFQVRIRVJfRk9SRUNBU1RTJztcblxuLy8gQWN0aW9uIGNyZWF0b3JzXG5leHBvcnQgY29uc3QgcmVxdWVzdFdlYXRoZXJGb3JlY2FzdHMgPSAoc3RhcnREYXRlSW5kZXgpID0+IHtcbiAgcmV0dXJuIChkaXNwYXRjaCwgZ2V0U3RhdGUpID0+IHtcbiAgICBpZiAoc3RhcnREYXRlSW5kZXggIT09IGdldFN0YXRlKCkud2VhdGhlckZvcmVjYXN0cy5zdGFydERhdGVJbmRleCkge1xuICAgICAgZGlzcGF0Y2goe3R5cGU6IFJFUVVFU1RfV0VBVEhFUl9GT1JFQ0FTVFMsIHBheWxvYWQ6IHN0YXJ0RGF0ZUluZGV4fSk7XG4gICAgICByZXR1cm4gZmV0Y2goYC9hcGkvU2FtcGxlRGF0YS9XZWF0aGVyRm9yZWNhc3RzP3N0YXJ0RGF0ZUluZGV4PSR7IHN0YXJ0RGF0ZUluZGV4IH1gKVxuICAgICAgICAudGhlbihyZXNwb25zZSA9PiByZXNwb25zZS5qc29uKCkpXG4gICAgICAgIC50aGVuKChkYXRhKSA9PiB7XG4gICAgICAgICAgZGlzcGF0Y2gocmVjZWl2ZVdlYXRoZXJGb3JlY2FzdHMoc3RhcnREYXRlSW5kZXgsIGRhdGEpKTtcbiAgICAgICAgfSk7XG4gICAgfVxuICB9O1xufTtcblxuZXhwb3J0IGNvbnN0IHJlY2VpdmVXZWF0aGVyRm9yZWNhc3RzID0gKHN0YXJ0RGF0ZUluZGV4LCBmb3JlY2FzdHMpID0+ICh7XG4gIHR5cGU6IFJFQ0VJVkVfV0VBVEhFUl9GT1JFQ0FTVFMsXG4gIHBheWxvYWQ6IHtzdGFydERhdGVJbmRleCwgZm9yZWNhc3RzfSxcbn0pO1xuXG5jb25zdCBpbml0aWFsU3RhdGUgPSB7XG4gIHN0YXJ0RGF0ZUluZGV4OiBudWxsLFxuICBmb3JlY2FzdHM6IFtdLFxuICBpc0xvYWRpbmc6IGZhbHNlXG59O1xuXG4vLyBUaGUgcmVkdWNlciB0aGF0IGNoYW5nZXMgdGhlIHN0YXRlIGJhc2VkIG9uIHRoZSBhY3Rpb24gdHlwZVxuZXhwb3J0IGRlZmF1bHQgKHN0YXRlID0gaW5pdGlhbFN0YXRlLCBhY3Rpb24pID0+IHtcblxuICBpZiAoYWN0aW9uLnR5cGUgPT09IFJFUVVFU1RfV0VBVEhFUl9GT1JFQ0FTVFMpIHtcbiAgICByZXR1cm4geyBzdGFydERhdGVJbmRleDogYWN0aW9uLnBheWxvYWQsIGlzTG9hZGluZzogdHJ1ZSwgZm9yZWNhc3RzOiBzdGF0ZS5mb3JlY2FzdHMgfTtcbiAgfSBlbHNlIGlmIChhY3Rpb24udHlwZSA9PT0gUkVDRUlWRV9XRUFUSEVSX0ZPUkVDQVNUUykge1xuICAgIC8vIE9ubHkgYWNjZXB0IHRoZSBpbmNvbWluZyBkYXRhIGlmIGl0IG1hdGNoZXMgdGhlIG1vc3QgcmVjZW50IHJlcXVlc3QuIFRoaXMgZW5zdXJlcyB3ZSBjb3JyZWN0bHlcbiAgICAvLyBoYW5kbGUgb3V0LW9mLW9yZGVyIHJlc3BvbnNlcy5cbiAgICBpZiAoYWN0aW9uLnBheWxvYWQuc3RhcnREYXRlSW5kZXggPT09IHN0YXRlLnN0YXJ0RGF0ZUluZGV4KSB7XG4gICAgICByZXR1cm4geyBzdGFydERhdGVJbmRleDogYWN0aW9uLnBheWxvYWQuc3RhcnREYXRlSW5kZXgsIGZvcmVjYXN0czogYWN0aW9uLnBheWxvYWQuZm9yZWNhc3RzLCBpc0xvYWRpbmc6IGZhbHNlIH07XG4gICAgfVxuICB9XG5cbiAgcmV0dXJuIHN0YXRlO1xufVxuXG5cblxuLy8gV0VCUEFDSyBGT09URVIgLy9cbi8vIC4vQ2xpZW50QXBwL3JlZHVjZXJzL3dlYXRoZXJGb3JlY2FzdHMuanMiLCJtb2R1bGUuZXhwb3J0cyA9IHJlcXVpcmUoXCJkb21haW4tdGFzay9mZXRjaFwiKTtcblxuXG4vLy8vLy8vLy8vLy8vLy8vLy9cbi8vIFdFQlBBQ0sgRk9PVEVSXG4vLyBleHRlcm5hbCBcImRvbWFpbi10YXNrL2ZldGNoXCJcbi8vIG1vZHVsZSBpZCA9IDExXG4vLyBtb2R1bGUgY2h1bmtzID0gMCIsImltcG9ydCBSZWFjdCwge0NvbXBvbmVudCwgUHJvcFR5cGVzfSBmcm9tICdyZWFjdCc7XG5pbXBvcnQgeyBjb25uZWN0IH0gZnJvbSAncmVhY3QtcmVkdXgnO1xuaW1wb3J0IHsgaW5jcmVtZW50Q291bnQgfSBmcm9tICcuLi9yZWR1Y2Vycy9jb3VudGVyJztcblxuY2xhc3MgQ291bnRlciBleHRlbmRzIENvbXBvbmVudCB7XG4gIHJlbmRlcigpIHtcbiAgICByZXR1cm4gKFxuICAgICAgPGRpdj5cbiAgICAgICAgPGgxPkNvdW50ZXI8L2gxPlxuICAgICAgICA8cD5UaGlzIGlzIGEgc2ltcGxlIGV4YW1wbGUgb2YgYSBSZWFjdCBjb21wb25lbnQuPC9wPlxuICAgICAgICA8cD5DdXJyZW50IGNvdW50OiA8c3Ryb25nPnsgdGhpcy5wcm9wcy5jb3VudCB9PC9zdHJvbmc+PC9wPlxuICAgICAgICA8YnV0dG9uIG9uQ2xpY2s9eyAoKSA9PiB7IHRoaXMucHJvcHMuaW5jcmVtZW50KCkgfSB9PkluY3JlbWVudDwvYnV0dG9uPlxuICAgICA8L2Rpdj5cbiAgICApO1xuICB9XG59XG5cbkNvdW50ZXIucHJvcFR5cGVzID0ge1xuICBjb3VudDogUHJvcFR5cGVzLm51bWJlci5pc1JlcXVpcmVkLFxuICBpbmNyZW1lbnQ6IFByb3BUeXBlcy5mdW5jLmlzUmVxdWlyZWQsXG59O1xuXG5jb25zdCBtYXBTdGF0ZVRvUHJvcHMgPSAoc3RhdGUpID0+ICh7XG4gIGNvdW50OiBzdGF0ZS5jb3VudGVyLmNvdW50XG59KTtcblxuY29uc3QgbWFwRGlzcGF0Y2hUb1Byb3BzID0gKGRpc3BhdGNoKSA9PiAoe1xuICBpbmNyZW1lbnQ6ICgpID0+IHtcbiAgICBkaXNwYXRjaChpbmNyZW1lbnRDb3VudCgpKTtcbiAgfVxufSk7XG5cbmV4cG9ydCBkZWZhdWx0IGNvbm5lY3QoXG4gIG1hcFN0YXRlVG9Qcm9wcyxcbiAgbWFwRGlzcGF0Y2hUb1Byb3BzXG4pKENvdW50ZXIpO1xuXG5cblxuLy8gV0VCUEFDSyBGT09URVIgLy9cbi8vIC4vQ2xpZW50QXBwL2NvbXBvbmVudHMvY291bnRlci5qc3giLCIvLyBDb25zdGFudCBkZWZpbmluZyB0aGUgYWN0aW9uIHR5cGVcbmNvbnN0IElOQ1JFTUVOVF9DT1VOVCA9ICdJTkNSRU1FTlRfQ09VTlQnO1xuXG4vLyBBY3Rpb24gY3JlYXRvclxuZXhwb3J0IGNvbnN0IGluY3JlbWVudENvdW50ID0gKCkgPT4gKHt0eXBlOiBJTkNSRU1FTlRfQ09VTlR9KTtcblxuY29uc3QgaW5pdGlhbFN0YXRlID0ge1xuICBjb3VudDogMFxufTtcblxuLy8gVGhlIHJlZHVjZXIgdGhhdCBjaGFuZ2VzIHRoZSBzdGF0ZSBiYXNlZCBvbiB0aGUgYWN0aW9uIHR5cGVcbmV4cG9ydCBkZWZhdWx0IChzdGF0ZSA9IGluaXRpYWxTdGF0ZSwgYWN0aW9uKSA9PiB7XG4gIGlmIChhY3Rpb24udHlwZSA9PT0gSU5DUkVNRU5UX0NPVU5UKSB7XG4gICAgcmV0dXJuIHsgY291bnQ6IHN0YXRlLmNvdW50ICsgMSB9O1xuICB9XG5cbiAgcmV0dXJuIHN0YXRlO1xufVxuXG5cblxuLy8gV0VCUEFDSyBGT09URVIgLy9cbi8vIC4vQ2xpZW50QXBwL3JlZHVjZXJzL2NvdW50ZXIuanMiLCJpbXBvcnQgeyBjcmVhdGVTdG9yZSwgY29tcG9zZSwgYXBwbHlNaWRkbGV3YXJlIH0gZnJvbSAncmVkdXgnO1xuaW1wb3J0IHRodW5rTWlkZGxld2FyZSBmcm9tICdyZWR1eC10aHVuayc7XG5pbXBvcnQgcm9vdFJlZHVjZXIgZnJvbSAnLi9yZWR1Y2VycydcblxuZXhwb3J0IGRlZmF1bHQgKGluaXRpYWxTdGF0ZSkgPT4ge1xuICBjb25zdCB3aW5kb3dJZkRlZmluZWQgPSB0eXBlb2Ygd2luZG93ID09PSAndW5kZWZpbmVkJyA/IG51bGwgOiB3aW5kb3c7XG4gIGNvbnN0IGRldlRvb2xzRXh0ZW5zaW9uID0gd2luZG93SWZEZWZpbmVkICYmIHdpbmRvd0lmRGVmaW5lZC5kZXZUb29sc0V4dGVuc2lvbjsgLy8gSWYgZGV2VG9vbHMgaXMgaW5zdGFsbGVkLCBjb25uZWN0IHRvIGl0XG4gIGNvbnN0IGNyZWF0ZVN0b3JlV2l0aE1pZGRsZXdhcmUgPSBjb21wb3NlKFxuICAgIGFwcGx5TWlkZGxld2FyZSh0aHVua01pZGRsZXdhcmUpLFxuICAgIGRldlRvb2xzRXh0ZW5zaW9uID8gZGV2VG9vbHNFeHRlbnNpb24oKSA6IGYgPT4gZlxuICApKGNyZWF0ZVN0b3JlKTtcblxuICBjb25zdCBzdG9yZSA9IGNyZWF0ZVN0b3JlV2l0aE1pZGRsZXdhcmUocm9vdFJlZHVjZXIsIGluaXRpYWxTdGF0ZSk7XG4gIC8vIEVuYWJsZSBXZWJwYWNrIGhvdCBtb2R1bGUgcmVwbGFjZW1lbnQgZm9yIHJlZHVjZXJzXG4gIGlmIChtb2R1bGUuaG90KSB7XG4gICAgbW9kdWxlLmhvdC5hY2NlcHQoJy4vcmVkdWNlcnMnLCAoKSA9PiB7XG4gICAgICBjb25zdCBuZXh0UmVkdWNlciA9IHJlcXVpcmUoJy4vcmVkdWNlcnMnKS5kZWZhdWx0XG4gICAgICBzdG9yZS5yZXBsYWNlUmVkdWNlcihuZXh0UmVkdWNlcik7XG4gICAgfSk7XG4gIH1cblxuICByZXR1cm4gc3RvcmU7XG59O1xuXG5cblxuLy8gV0VCUEFDSyBGT09URVIgLy9cbi8vIC4vQ2xpZW50QXBwL2NvbmZpZ3VyZVN0b3JlLmpzIiwibW9kdWxlLmV4cG9ydHMgPSByZXF1aXJlKFwicmVkdXhcIik7XG5cblxuLy8vLy8vLy8vLy8vLy8vLy8vXG4vLyBXRUJQQUNLIEZPT1RFUlxuLy8gZXh0ZXJuYWwgXCJyZWR1eFwiXG4vLyBtb2R1bGUgaWQgPSAxNVxuLy8gbW9kdWxlIGNodW5rcyA9IDAiLCJtb2R1bGUuZXhwb3J0cyA9IHJlcXVpcmUoXCJyZWR1eC10aHVua1wiKTtcblxuXG4vLy8vLy8vLy8vLy8vLy8vLy9cbi8vIFdFQlBBQ0sgRk9PVEVSXG4vLyBleHRlcm5hbCBcInJlZHV4LXRodW5rXCJcbi8vIG1vZHVsZSBpZCA9IDE2XG4vLyBtb2R1bGUgY2h1bmtzID0gMCIsImltcG9ydCB7IGNvbWJpbmVSZWR1Y2VycyB9IGZyb20gJ3JlZHV4JztcbmltcG9ydCB7IHJvdXRlclJlZHVjZXIgfSBmcm9tICdyZWFjdC1yb3V0ZXItcmVkdXgnXG5cbmltcG9ydCBjb3VudGVyIGZyb20gJy4vY291bnRlcic7XG5pbXBvcnQgd2VhdGhlckZvcmVjYXN0cyBmcm9tICcuL3dlYXRoZXJGb3JlY2FzdHMnO1xuXG5leHBvcnQgZGVmYXVsdCBjb21iaW5lUmVkdWNlcnMoe1xuICByb3V0aW5nOiByb3V0ZXJSZWR1Y2VyLFxuICBjb3VudGVyLFxuICB3ZWF0aGVyRm9yZWNhc3RzXG59KTtcblxuXG5cbi8vIFdFQlBBQ0sgRk9PVEVSIC8vXG4vLyAuL0NsaWVudEFwcC9yZWR1Y2Vycy9pbmRleC5qcyIsIm1vZHVsZS5leHBvcnRzID0gcmVxdWlyZShcInJlYWN0LXJvdXRlci1yZWR1eFwiKTtcblxuXG4vLy8vLy8vLy8vLy8vLy8vLy9cbi8vIFdFQlBBQ0sgRk9PVEVSXG4vLyBleHRlcm5hbCBcInJlYWN0LXJvdXRlci1yZWR1eFwiXG4vLyBtb2R1bGUgaWQgPSAxOFxuLy8gbW9kdWxlIGNodW5rcyA9IDAiXSwic291cmNlUm9vdCI6IiJ9