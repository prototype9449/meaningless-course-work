import React, {Component} from 'react'
import {DataSet,Network} from 'vis'
import {connect} from 'react-redux'

import dnaToDfa from '../logic'

class Graph extends Component {
  handleClick = () => {
    const {states, symbols, transitions,start,  end} = this.props

    const dfaData = dnaToDfa({states, symbols, transitions, start, end})

    const getColor = (x) => {
      if (x === dfaData.start) {
        return 'blue'
      }
      if (dfaData.end.includes(x)) {
        return 'gold'
      }

      return 'white'
    }

    const nodes = dfaData.states.map(x => ({
      id: x,
      label: x,
      shape: 'circle',
      color: getColor(x)
    }))
    const edges = dfaData.transitions.map(({first, second, symbol}) => ({
      from: first,
      to: second,
      label: symbol,
      arrows: 'to'
    }))
    debugger

    this.graphic = new Network(this.graph, {nodes, edges}, {});
  }

  render() {
    return <div id="kek">
      <div onClick={this.handleClick} className="graph_command_panel">
        <div className="button">Convert</div>
      </div>
      <div ref={x => this.graph = x} className="graph"/>
    </div>
  }
}


function mapStateToProps({a}) {
  return {
    states: a.states,
    symbols: a.symbols,
    transitions: a.transitions,
    start: a.startLine.trim().split(' ')[0],
    end: a.endLine.trim().split(' ')[0]
  }
}

export default connect(mapStateToProps)(Graph)
