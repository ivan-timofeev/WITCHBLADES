import React, { Component } from 'react';
import '../styles/artist.css';
import { NavLink } from 'react-router-dom';

export default class ArtistCard extends Component {

    constructor(props) {
        super(props);

        this.state = {
            artistId: props.artistId,
            artistName: props.artistName ?? "Artist name",
            artistImage: props.artistImage ?? ""
        };
    }
     
    render() {
        let style={ 
            backgroundImage: "url(" + this.state.artistImage + ")",
            backgroundPosition: 'center',
            backgroundSize: 'cover',
            backgroundRepeat: 'no-repeat'
          };

        let artistImage = this.state.artistImage !== ""
            ? <div style={style} className="artist-img"></div>
            : <div className="image-placeholder">Artist Image Must Be Here</div>

        return (
            <NavLink className="navLink" to={'/artist/' + this.state.artistId}>
                <div className="artist-card">
                    <div className="artist-img">{artistImage}</div>
                    <div className="artist-name">{this.state.artistName}</div>
                </div>
            </NavLink>
        );
    }
}
