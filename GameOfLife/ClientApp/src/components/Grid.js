import React, { Component } from 'react';
import '../index.css';
import Box from './Box';

class Grid extends Component {
    render() {
        const width = (this.props.cols * 14);
        var rowsArr = [];

        var boxClass = "";
        for (var i = 0; i < this.props.cols; i++) {
            for (var j = 0; j < this.props.rows; j++) {
                let boxId = i + "_" + j;

                boxClass = this.props.gridFull[i][j] ? "box on" : "box off";
                rowsArr.push(
                    <Box
                        boxClass={boxClass}
                        key={boxId}
                        boxId={boxId}
                        row={i}
                        col={j}
                        selectBox={this.props.handleLifeformCreation}
                    />
                );
            }
        }

        return (
            <div className="grid" style={{ width: width }}>
                {rowsArr}
            </div>
        );
    }
}

export default Grid;