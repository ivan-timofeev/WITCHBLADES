import React, { Component } from 'react';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import Header from './components/header';
import HomePage from './pages/home';
import { AboutPage } from './pages/about';

import './App.css';
import ArtistPage from './pages/artistPage'

export default class App extends Component {
    static displayName = App.name;

    constructor(props) {
        super(props);
    }

    render() {
        return (
            <BrowserRouter>
                <Header></Header>
                <div>
                    <Routes>
                        <Route path={'/'} exact element={<HomePage/>}/>
                        <Route path="/artist/:id" element={<ArtistPage/>}/>
                        <Route path={'/about'} element={<AboutPage/>}/>
                    </Routes>
                </div>
            </BrowserRouter>
        );
    }
}
