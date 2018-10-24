import React, { Component } from 'react';
import '../index.css';
import Box from './Box';

class Grid extends Component {
    render() {
        const width = (this.props.cols * 14);
        var rowsArr = [];

        var boxClass = "box";
        for (var i = 0; i < this.props.cols; i++) {
            for (var j = 0; j < this.props.rows; j++) {
                let boxId = i + "_" + j;

                //boxClass = this.props.gridFull[i][j] ? "box on" : "box off";
                rowsArr.push(
                    <Box
                        boxClass={boxClass}
                        key={boxId}
                        boxId={boxId}
                        row={i}
                        col={j}
                        color={this.props.gridFull[i][j]}
                        selectBox={this.props.handleLifeformCreation}
                    />
                );
            }
        }

        return (
            <div >
                <table>
                    <tbody>
                        {this.props.gridFull.map((variable, indexCol) =>
                            <tr key={indexCol}>
                                {variable.map((value, indexRow) =>
                                    <td key={indexCol + "_" + indexRow} className="cell"
                                        onClick={() => this.props.handleLifeformCreation(indexRow, indexCol)}
                                        style={{ backgroundColor: value ? value : 'lightgray', width: 15, height: 15, border: '1px solid black' }}
                                    />
                                )
                                }
                            </tr>
                        )
                        }
                    </tbody>
                </table>
            </div>
        );
    }
}

export default Grid;