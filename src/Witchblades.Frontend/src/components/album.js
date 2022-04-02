import React, { Component } from 'react';
import '../styles/album.css';
import { NavLink } from 'react-router-dom';

export default class AlbumCard extends Component {

    constructor(props) {
        super(props);

        this.state = {
            albumName: props.albumName ?? "Album name",
            albumAuthor: props.albumAuthor ?? "Album author",
            albumImage: props.albumImage ?? "",
            releaseDate: props.releaseDate ?? "1945"
        };
    }
     
    render() {
        let albumImg = this.state.albumImage !== ""
            ? <img className="album-img" src={this.state.albumImage}></img>
            : <div className="image-placeholder">Album Image Must Be Here</div>

        return (
            <div className="album-card">
                <div className="album-img">{albumImg}</div>
                <div className="album-name">{this.state.albumName}</div>
                <div className="album-author">{this.state.albumAuthor}, <span className="album-release-date">{this.state.releaseDate.substring(0, 4)}</span></div>
            </div>
        );
    }
}
