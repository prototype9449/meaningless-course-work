import React from 'react'

require('normalize.css/normalize.css')
import '../styles/main'

import StateLine from './state-line'
import Transitions from './transitions'
import Graph from './graph'

class AppComponent extends React.Component {
  render() {
    return (
      <div>
        <StateLine/>
        <Transitions/>
        <Graph/>
      </div>
    )
  }
}

AppComponent.defaultProps = {
}

export default AppComponent
