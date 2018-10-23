import * as chroma from 'chroma-js';

const receiveGameStatusType = 'RECEIVE_GAME_STATUS';
const setLifeformNameType = 'SET_LIFEFORM_NAME';
const initialState = { gameStatus: null, lifeformName: 'pixel', userColor: chroma.random().hex() };

export const actionCreators = {
    receiveGameStatus: (gameStatus) => ({ type: receiveGameStatusType, gameStatus }),
    setLifeformName: (name) => ({ type: setLifeformNameType, name })
};

export const reducer = (state, action) => {
    state = state || initialState;

    if (action.type === receiveGameStatusType) {
        return {
            ...state,
            gameStatus: action.gameStatus
        };
    }
    if (action.type === setLifeformNameType) {
        return {
            ...state,
            lifeformName: action.name
        };
    }
    return state;
};
