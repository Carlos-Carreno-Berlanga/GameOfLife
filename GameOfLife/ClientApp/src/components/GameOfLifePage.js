import React, { Component, Fragment } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import { HubConnectionBuilder } from '@aspnet/signalr';
import RingLoader from 'react-spinners/RingLoader';

import { actionCreators } from '../store/GameOfLifePage';
import Grid from './Grid';
import LifeformToolbar from './LifeformToolbar';

let hubConnection = null;

class GameOfLifePage extends Component {
    constructor() {
        super();
        this.handleNotification();
        this.handleLifeformCreation = this.handleLifeformCreation.bind(this);
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
            //console.log("Notify", data);
            this.props.receiveGameStatus(data);
            //this.props.setLifeformName("HOLA");
        });
        hubConnection.start();
    }

    handleLifeformCreation(row, col) {
        console.log("row", row, "col", col);
        this.props.createLifeForm(
            {
                name: this.props.lifeformName,
                col: col,
                row: row,
                Color: this.props.userColor
            }
        );
    }

    render() {
        if (this.props.gameStatus && this.props.gameStatus.board) {
            return (
                <div className="container-fluid">
                    <div className="row">
                        <div className="col-4 offset-3">
                            <h1 className="text-center  pb-2">
                                <span className="text-primary">Generation {this.props.gameStatus.generation}  </span>
                                <span className="w-10 text-secondary" style={{ backgroundColor: this.props.userColor }} > User  </span>
                            </h1>
                        </div>
                        <Grid
                            rows={this.props.gameStatus.rows}
                            cols={this.props.gameStatus.columns}
                            gridFull={this.props.gameStatus.board}
                            handleLifeformCreation={this.handleLifeformCreation}
                        />
                        <div className="col-5">
                            <LifeformToolbar
                                selectLifeform={this.props.setLifeformName}
                            />
                        </div>
                    </div>
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
                        loading
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
