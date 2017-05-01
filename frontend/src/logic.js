import isEqual from 'lodash/isEqual'

export default function dnaToDfa({states, symbols, transitions, start, end}) {
  const filteredTransitions = transitions
    .filter(x => symbols.find(y => y === x.symbol))
    .filter(x => states.find(y => y === x.first))
    .filter(x => states.find(y => y === x.second))

  const newStates = [[start]]
  const mentionatedStates = [[start]]
  const newTransitions = []
  while (newStates.length !== 0) {
    const newState = newStates.pop()
    symbols.forEach(symbol => {
      const currentStates = newState.reduce((prev, curr) => {
        const toStates = filteredTransitions.filter(x => x.symbol === symbol && x.first === curr).map(x => x.second)

        return [...new Set([...prev, ...toStates])]
      }, [])
      newTransitions.push({
        first: newState,
        second: currentStates,
        symbol
      })
      if (!mentionatedStates.find(x => isEqual(x, currentStates))) {
        newStates.push(currentStates)
        mentionatedStates.push(currentStates)
      }
    })
  }

  const aliases = []
  newTransitions.forEach(({first, second}) => {
    if (!aliases.find(y => isEqual(first, y))) {
      aliases.push(first)
    }
    if (!aliases.find(y => isEqual(second, y))) {
      aliases.push(second)
    }
  })
  const result = newTransitions.map(x => ({
    first: aliases.findIndex(y => isEqual(x.first, y)),
    second: aliases.findIndex(y => isEqual(x.second, y)),
    symbol: x.symbol
  })).reduce((prev, curr) => {
    const index = prev.findIndex(x => x.first === curr.first && x.second === curr.second)
    if(index !== -1){
      prev[index].symbol = `${prev[index].symbol} + ${curr.symbol}`
      return [...prev]
    }
    return [...prev, curr]
  }, [])

  const newStart = aliases.findIndex(x => isEqual(x, [start]))
  const newEnd = aliases.filter(x => x.includes(end))

  return {
    transitions: result,
    states: aliases.map((x, i) => i),
    start: newStart,
    end: newEnd.map(x => aliases.findIndex(y => isEqual(x,y)))
  }
}
