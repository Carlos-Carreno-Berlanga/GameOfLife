import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { HubConnectionBuilder } from '@aspnet/signalr';

import { actionCreators } from '../store/GameOfLifePage';
let hubConnection = null;
class GameOfLifePage extends Component {
    constructor() {
        super();
        this.handleNotification();
    }

    handleNotification() {
        if (hubConnection) {
            console.log("unsuscribe");
            hubConnection.off("Notify");
            hubConnection = null;
        }

        hubConnection = new HubConnectionBuilder()
            .withUrl('/GameNotifier')
            .build();

        hubConnection.on("Notify", (data) => {
            console.log("Notify", data);
            this.props.receiveGameStatus(data);

        });
        hubConnection.start();
    }

    render() {
        return (
            <div>HOLA</div>
        );
    }


}

export default connect(
    state => state.gameOfLife,
    dispatch => bindActionCreators(actionCreators, dispatch)
)(GameOfLifePage);
