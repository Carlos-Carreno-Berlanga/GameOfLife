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
            <div className="d-flex justify-content-center flex-column align-items-stretch" role="group" >
                <button type="button" className="btn btn-secondary my-2">
                    Block
                    <p />
                    <img className="rounded mx-auto" src={Block} />
                </button>
                <button type="button" className="btn btn-secondary my-2">
                    Blinker
                    <p />
                    <img className="rounded mx-auto d-block" src={Blinker} />
                </button>
                <button type="button" className="btn btn-secondary my-2">
                    Glider
                    <p />
                    <img className="rounded mx-auto d-block" src={Glider} />
                </button>
                <button type="button" className="btn btn-secondary my-2">
                    Lightweight spaceship
                    <p />
                    <img className="rounded mx-auto d-block" src={Lwss} />
                </button>
            </div >
        );
    }
}

export default Toolbar;