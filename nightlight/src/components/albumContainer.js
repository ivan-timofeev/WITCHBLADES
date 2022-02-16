import React, { Component } from 'react';
import '../styles/home.css';
import AlbumCard from './album';

export default class AlbumContainer extends Component {

    constructor(props) {
        super(props);
        this.state = { albums: <div>Nothing loaded</div> }
    }

    async componentDidMount() {
        var result;
        
        try {
            result = await fetch("https://localhost:5001/api/Albums");
        } catch (ex) {
            result = { status: -1, message: ex };
            console.log(ex);
        }

        if (result.status === 200) {
            let data = await result.json();
            console.log(data);
            let albums = data.map((album) => 
                <div className="collection-item">
                    <AlbumCard albumName={album.albumName}
                               albumImage={album.albumImage}
                               albumAuthor={album.artist.artistName}
                               releaseDate={album.releaseDate}/>
                </div>
            );

            this.state = { albums: albums };
            this.setState(this.state);
        } else {
            this.state = { albums: this.generateTamplates() };
            this.setState(this.state);
        }
    }

    generateTamplates() {
        return  [...Array(8)].map((item, index) => <div className="collection-item"><AlbumCard /></div>  );
    }

    render() {
        return (
            <div className="collection-container">
                {this.state.albums}
            </div>
        );
    }
}

