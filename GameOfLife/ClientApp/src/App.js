import React from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout';
import Counter from './components/Counter';
import FetchData from './components/FetchData';
import GameOfLifePage from './components/GameOfLifePage';

export default () => (
    <Layout>
        <Route exact path='/' component={GameOfLifePage} />
        <Route path='/counter' component={Counter} />
        <Route path='/fetchdata/:startDateIndex?' component={FetchData} />
    </Layout>
);
