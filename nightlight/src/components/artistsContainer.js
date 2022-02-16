import React, { Component } from 'react';
import '../styles/home.css';
import ArtistCard from './artist';
import { useNavigate } from "react-router-dom";

export default class ArtistsContainer extends Component {

    constructor(props) {
        super(props);
        this.state = { artists: <div>Nothing loaded</div> }
    }

    async componentDidMount() {
        var result;
        
        try {
            result = await fetch("https://localhost:5001/api/Artists");
        } catch (ex) {
            result = { status: -1, message: ex }
            console.log(ex);
        }

        if (result.status === 200) {
            let data = await result.json();
            console.log(data);
            let artists = data.map((artist) => 
                <div className="collection-item">
                    <ArtistCard artistId={artist.id}
                                artistName={artist.artistName}
                                artistImage={artist.artistImage}/>
                </div>
            );

            this.state = { artists: artists };
            this.setState(this.state);
        } else {
            this.state = { artists: this.generateTamplates() }
            this.setState(this.state);
        }
    }

    generateTamplates() {
        return  [...Array(8)].map((item, index) => <div className="collection-item"><ArtistCard /></div>  );
    }

    render() {
        return (
            <div className="collection-container">
                {this.state.artists}
            </div>
        );
    }
}

