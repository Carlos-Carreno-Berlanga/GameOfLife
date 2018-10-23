import * as chroma from 'chroma-js';

const receiveGameStatusType = 'RECEIVE_GAME_STATUS';
const setLifeformNameType = 'SET_LIFEFORM_NAME';
const createLifeformType = 'CREATE_LIFEFORM';
const initialState = { gameStatus: null, lifeformName: 'pixel', userColor: chroma.random().hex() };

export const actionCreators = {
    receiveGameStatus: (gameStatus) => ({ type: receiveGameStatusType, gameStatus }),
    setLifeformName: (name) => ({ type: setLifeformNameType, name }),
    createLifeForm: (createLifeFormRequest) => async (dispatch) => {

        dispatch({ type: createLifeformType, createLifeFormRequest });

        const url = `api/GameStatus/`;
        const response = await fetch(url, {
            method: 'PUT',
            credentials: 'include',
            headers: {
                Pragma: 'default',
                'Content-type': 'application/json'
            },
            body: JSON.stringify(createLifeFormRequest)
        });
    }
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
    if (action.type === createLifeformType) {
        return {
            ...state,
            lastLifeform: action.createLifeFormRequest
        };
    }
    return state;
};
