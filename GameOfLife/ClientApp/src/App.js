import React from 'react';
import { Route } from 'react-router';
import GameOfLifePage from './components/GameOfLifePage';

export default () => (

    <Route exact path='/' component={GameOfLifePage} />
);
