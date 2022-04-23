import React, { useEffect, useState } from 'react';
import { useParams} from 'react-router-dom';
import "../styles/artistpage.css";

const state = {
    artistName: "Artist Name",
    artistImage: "Artist Image URL",
    albums: [{
        id: 1,
        albumName: "",
        releaseDate: "",
        albumImage: "",
        tracks: [{
            id: 1,
            inAlbumNumber: 1,
            trackName: ""
        }]
    }]
}

const ArtistPage = (props) => {
    const [data, setData] = useState({ artistName: "TEST"  });
    const { id } = useParams();

    useEffect(() => {
        async function fetchData() {
            var result = await fetch(`http://witchblades.com/api/v1/Artists/${id}`);
            var data = await result.json();
            
            setData(data);
        };
        fetchData();
    }, [id]);

    let test = data?.albums?.map(album => (
        <>
            <div className="ArtistPage_table_row">
                <div className="ArtistPage_table_columnId">
                    <img className="ArtistPage_table_column_albumImg" width="50px" src={album?.albumImage}></img>
                </div>
                <div className="ArtistPage_table_column">
                    <div className="ArtistPage_table_column_albumName">{album?.albumName}</div>
                </div>
                <div className="ArtistPage_table_column"></div>
                <div className="ArtistPage_table_column"></div>
            </div>
            {
                album?.tracks.map(track => (
                    <div className="ArtistPage_table_row">
                        <div className="ArtistPage_table_columnId">{track?.inAlbumNumber}</div>
                        <div className="ArtistPage_table_column">{track?.trackName}</div>
                        <div className="ArtistPage_table_column">{track?.duration}</div>
                        <div className="ArtistPage_table_column">{track?.collaboration?.map(artist => <span class="ArtistPage_artist">{artist.artistName}</span>)}</div>
                    </div>
                ))
            }
        </>
        
    ));

    let style={ 
        backgroundImage: `url(${data.artistImage})`,
        backgroundPosition: 'center',
        backgroundSize: 'cover',
        backgroundRepeat: 'no-repeat'
    };

    return (
        <div className="ArtistPage_flex">
            <div className="ArtistPage_artist-info">
                <div className="ArtistPage_artist-image" style={style}></div>
                <div className="ArtistPage_artist-name">{data.artistName}</div>
            </div>
            <div className="ArtistPage_popular">
                <h1>Популярные треки</h1>

                <div className="ArtistPage_popularTracks">
                    <div className="ArtistPage_table">
                        <div className="ArtistPage_table_row">
                            <div className="ArtistPage_table_columnId"></div>
                            <div className="ArtistPage_table_column"><b>Название</b></div>
                            <div className="ArtistPage_table_column"><b>Длительность</b></div>
                            <div className="ArtistPage_table_column"><b>На фите</b></div>
                        </div>
                        {test}
                    </div>
                </div>
            </div>
        </div>
    )
}

export default ArtistPage;