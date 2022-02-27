import React, { Component } from 'react';
import '../styles/home.css';
import AlbumCard from '../components/album';
import ArtistCard from '../components/artist';
import AlbumContainer from '../components/albumContainer';
import ArtistsContainer from '../components/artistsContainer';
import Player from '../components/player';

export default class HomePage extends Component {

    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div className="container">
                <div className="label-container">
                    <h1 className="label">Добрый день</h1>
                </div>

                <Player></Player>

                <h1 className="label2">Свежие альбомы</h1>
                <AlbumContainer/>

                <h1 className="label2">Артисты</h1>
                <ArtistsContainer/>
                
            </div>
        );
    }
}

