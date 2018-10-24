import React, { Component } from 'react';
import '../index.css';

class Box extends Component {
    selectBox = () => {
        this.props.selectBox(this.props.row, this.props.col);

    }

    render() {
        return (
            <div
                className="box"
                id={this.props.id}
                onClick={this.selectBox}
                style={{ backgroundColor: this.props.color ? this.props.color : 'lightgray'}}
            />
        );
    }
}

export default Box;