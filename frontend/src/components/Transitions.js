import React, {Component} from 'react'
import {connect} from 'react-redux'

import {changeTransitionLine, addTransition} from '../redux/transition'

class Transitions extends Component {
  handleChange = (index) => (e) => {
    this.props.onChange(e.target.value, index)
  }

  handleClick = () => {
    this.props.onAdd()
  }

  render() {
    const {transitions} = this.props

    return <div className="transitions">
      <div className="transitions_theme">Transitions <div onClick={this.handleClick} className="button" id="addButton">+</div></div>
      {transitions.map((x,i) => {
        return <div key={i} className="transition_item">
          <span>{`${(i+1)}`}</span><input type="text" value={x.value} onChange={this.handleChange(i)}/>
        </div>
      })}
      <div>
      </div>

    </div>
  }
}

function mapStateToProps({a}) {
  return {transitions: a.transitions}
}

function mapDispatchToProps(dispatch) {
  return {
    onChange: (value, index) => dispatch(changeTransitionLine({index, value})),
    onAdd: () => dispatch(addTransition(''))
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(Transitions)
