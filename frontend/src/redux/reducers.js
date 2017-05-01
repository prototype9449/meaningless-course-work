import { combineReducers } from 'redux'

import {reducer} from './transition'

export const makeRootReducer = () => {
  return combineReducers({
    a: reducer
  })
}

export default makeRootReducer
