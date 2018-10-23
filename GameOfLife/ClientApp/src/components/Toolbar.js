import React, { Component } from 'react';
import Block from '../images/block.svg';
import Blinker from '../images/blinker.gif';
import Glider from '../images/glider.gif';
import Lwss from '../images/lwss.gif';
class Toolbar extends Component {
    constructor() {
        super();
    }

    render() {
        return (
            <div className="btn-group d-flex justify-content-center pt-3" role="group" >

                <button type="button" className="btn btn-secondary btn-lg">
                    Block
                    <p />
                    <img className="rounded mx-auto d-block" src={Block} />
                </button>
                <button type="button" className="btn btn-secondary">
                    Blinker
                    <p />
                    <img className="rounded mx-auto d-block" src={Blinker} />
                </button>
                <button type="button" className="btn btn-secondary">Glider
                    <p />
                    <img className="rounded mx-auto d-block" src={Glider} />
                </button>
                <button type="button" className="btn btn-secondary">
                    Lightweight spaceship
                    <p />
                    <img className="rounded mx-auto d-block" src={Lwss} />
                </button>
            </div >
        );
    }
}

export default Toolbar;