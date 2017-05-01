import 'core-js/fn/object/assign'
import ReactDOM from 'react-dom'
import React from 'react'
import {Provider} from 'react-redux'

import App from './components/main'
import createStore from './redux/createStore'

const initialState = window.__INITIAL_STATE__
const store = createStore(initialState)

ReactDOM.render(
  <Provider store={store} key="provider">
    <App/>
  </Provider>,
  document.getElementById('root')
)
