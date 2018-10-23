import React, { Component, Fragment } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { HubConnectionBuilder } from '@aspnet/signalr';
import RingLoader from 'react-spinners/RingLoader';

import { actionCreators } from '../store/GameOfLifePage';
import Grid from './Grid';
import Toolbar from './Toolbar';
import Block from '../images/block.svg';
import Blinker from '../images/blinker.gif';

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

        if (this.props.gameStatus && this.props.gameStatus.board) {
            return (
                <div className="container-fluid">

                    <h1 className="text-center text-primary pb-2">Generation {this.props.gameStatus.generation}</h1>

                    <Grid
                        rows={this.props.gameStatus.rows}
                        cols={this.props.gameStatus.columns}
                        gridFull={this.props.gameStatus.board}
                    />
                    <Toolbar />

                </div>
            );
        }
        else {
            return (
                <div id="loading">
                    <RingLoader
                        sizeUnit={"px"}
                        size={150}
                        color={'#123abc'}
                        loading={true}
                    />
                </div>
            );
        }
    }


}

export default connect(
    state => state.gameOfLifePage,
    dispatch => bindActionCreators(actionCreators, dispatch)
)(GameOfLifePage);
