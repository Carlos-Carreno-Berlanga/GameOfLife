const receiveGameStatusType = 'RECEIVE_GAME_STATUS';
const initialState = { gameStatus: null };

export const actionCreators = {
    receiveGameStatus: (gameStatus) => ({ type: receiveGameStatusType, gameStatus })
};

export const reducer = (state, action) => {
    state = state || initialState;

    if (action.type === receiveGameStatusType) {
        return {
            ...state,
            gameStatus: action.gameStatus
        };
    }

    return state;
};
