import React, {Component} from 'react'
import {connect} from 'react-redux'

import {changeStateLine, changeSymbolLine, changeStartLine, changeEndLine} from '../redux/transition'

class StateLine extends Component {
  handleStateChange = (e) => {
    this.props.onStateLineChange(e.target.value)
  }

  handleSymbolChange = (e) => {
    this.props.onSymbolLineChange(e.target.value)
  }

  handleStartStateChange = (e) => {
    this.props.onStartStateChange(e.target.value)
  }

  handleEndStateChange = (e) => {
    this.props.onEndStateChange(e.target.value)
  }

  render() {
    const {stateLine, symbolLine} = this.props

    return <div className="values">
      <div className="values-item"><div className="values-item-text">States:</div> <input type="text" className="kek" value={stateLine} onChange={this.handleStateChange}/></div>
      <div className="values-item"><div className="values-item-text">Start state:</div> <input type="text" onChange={this.handleStartStateChange}/></div>
      <div className="values-item"><div className="values-item-text">End state:</div> <input type="text" onChange={this.handleEndStateChange}/></div>
      <div className="values-item"><div className="values-item-text">Symbols:</div> <input type="text" value={symbolLine} onChange={this.handleSymbolChange}/></div>
    </div>
  }
}

function mapStateToProps({a}) {
  return {
    stateLine: a.stateLine,
    symbolLine: a.symbolLine
  }
}

function mapDispatchToProps(dispatch) {
  return {
    onStateLineChange: (x) => dispatch(changeStateLine(x)),
    onSymbolLineChange: (x) => dispatch(changeSymbolLine(x)),
    onStartStateChange: (x) => dispatch(changeStartLine(x)),
    onEndStateChange: (x) => dispatch(changeEndLine(x))
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(StateLine)
