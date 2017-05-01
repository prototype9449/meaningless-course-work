export const CHANGE_STATE_LINE = 'CHANGE_STATE_LINE'
export const CHANGE_SYMBOL_LINE = 'CHANGE_SYMBOL_LINE'
export const CHANGE_TRANSITIONS_LINE = 'CHANGE_TRANSITIONS_LINE'
export const CHANGE_START_LINE = 'CHANGE_START_LINE'
export const CHANGE_END_LINE = 'CHANGE_END_LINE'
export const ADD_TRANSITION = 'ADD_TRANSITION'

export const changeStateLine = (value) => ({
  type: CHANGE_STATE_LINE,
  value
})

export const changeSymbolLine = (value) => ({
  type: CHANGE_SYMBOL_LINE,
  value
})

export const addTransition = (value) => ({
  type: ADD_TRANSITION,
  value
})

export const changeTransitionLine = ({value, index}) => ({
  type: CHANGE_TRANSITIONS_LINE,
  value,
  index
})

export const changeStartLine = (value) => ({
  type: CHANGE_START_LINE,
  value
})

export const changeEndLine = (value) => ({
  type: CHANGE_END_LINE,
  value
})

const initialState = {
  stateLine: '',
  symbolLine: '',
  startLine: '',
  endLine: '',
  states: [],
  symbols: [],
  transitions: []
};

export function reducer(state = initialState, action = {}) {
  switch (action.type) {
    case CHANGE_STATE_LINE:
    {
      return {
        ...state,
        states: action.value.split(' ').filter(x => !!x),
        stateLine: action.value
      }
    }
    case CHANGE_SYMBOL_LINE:
    {
      return {
        ...state,
        symbols: action.value.split(' ').filter(x => !!x),
        symbolLine: action.value
      }
    }
    case CHANGE_START_LINE:
    {
      return {
        ...state,
        startLine: action.value
      }
    }
    case CHANGE_END_LINE:
    {
      return {
        ...state,
        endLine: action.value
      }
    }
    case CHANGE_TRANSITIONS_LINE:
    {
      const [first, symbol, second] = action.value.split(' ')

      return {
        ...state,
        transitions: [
          ...state.transitions.slice(0, action.index),
          {
            value: action.value,
            first,
            symbol,
            second
          },
          ...state.transitions.slice(action.index + 1)
        ]
      }
    }
    case ADD_TRANSITION:
    {
      return {
        ...state,
        transitions: [
          ...state.transitions,
          action.value
        ]
      }
    }
    default:
      return state
  }
}
