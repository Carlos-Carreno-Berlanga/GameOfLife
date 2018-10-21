import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { HubConnectionBuilder } from '@aspnet/signalr';

import { actionCreators } from '../store/GameOfLifePage';
import Grid from './Grid';
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
        console.log("this.props.gameStatus", this.props.gameStatus);
        if (this.props.gameStatus && this.props.gameStatus.board) {
            return (
                <Grid
                    rows={70}
                    cols={70}
                    gridFull={this.props.gameStatus.board}
                />
            );
        }
        else {
            return (
                <span>LOADING</span>
                );
        }
    }


}

export default connect(
    state => state.gameOfLifePage,
    dispatch => bindActionCreators(actionCreators, dispatch)
)(GameOfLifePage);
