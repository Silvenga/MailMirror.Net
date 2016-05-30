import { bootstrap } from 'aurelia-bootstrapper-webpack';

import 'bootstrap';

import '../node_modules/bootstrap/dist/css/bootstrap.css';
import '../node_modules/font-awesome/css/font-awesome.css';
import '../styles/styles.css';

var Promise = require('bluebird').config({longStackTraces: false, warnings: false}); // Promise polyfill for IE11

bootstrap(function(aurelia) {
  aurelia.use
    .standardConfiguration()
    .developmentLogging();

  aurelia.start().then(() => aurelia.setRoot('app', document.body));
});