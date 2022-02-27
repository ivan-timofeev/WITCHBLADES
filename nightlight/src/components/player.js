import React, { useState, useEffect, useRef } from 'react';
import "../styles/audioPlayer.css";

const Player = () => {
    const track = {
        title: "мои друзья не должны умирать",
        artist: "aikko",
        audioSrc: "https://localhost:5001/music/15.mp3",
        image: "https://localhost:5001/images/albums/1.png",
        color: "black",
      };

	// State
    const [currentTrack, setCurrentTrack] = useState(track);
    const [isPlaying, setIsPlaying] = useState(false);
    const [volume, setVolume] = useState(0.1);
    const [maxTime, setMaxTime] = useState(1);
    const [currentTime, setCurrentTime] = useState(0);

	// Refs
    const audioRef = useRef(new Audio(currentTrack.audioSrc));
    const intervalRef = useRef();
    const isReady = useRef(false);

    useEffect(() => {
        if (isPlaying) {
          audioRef.current.play();
        } else {
          audioRef.current.pause();
        }
    }, [isPlaying]);

    useEffect(() => {
        setMaxTime(audioRef.current.duration);
        audioRef.current.ontimeupdate = () => {
            setCurrentTime(audioRef.current.currentTime);
            
            if (audioRef.current.ended) {
                setIsPlaying(false);
            }
        }
    }, [audioRef, isPlaying])

    useEffect(() => {
        audioRef.current.volume = volume;
    }, [volume]);

    

	// Destructure for conciseness
	const { duration } = audioRef.current;

    const onPlayPause = () => {
        if (isPlaying)
            setIsPlaying(false);
        else
            setIsPlaying(true);
    }

    let buttonImg = isPlaying
        ? "https://localhost:5001/images/pause-button.png"
        : "https://localhost:5001/images/play-button.png";

      return (
        <div className="audio-player-container">
            <div className="audio-player">
                <div className="audio-player-left">
                    <div className="audio-player-sound">
                        <img className="audio-player-volume-icon" src="https://localhost:5001/images/sound.svg"></img>
                        <div className="audio-player-volume">
                            <input type="range" min="0" value={volume} onChange={event => setVolume(event.target.value)} step="0.01" max="1"></input>
                        </div>
                    </div>
                    <div className="audio-player-buttons">
                        <img width="33px" className="click" src="https://localhost:5001/images/rewind.svg"></img>
                        <img width="40px" className="click click-center" src={buttonImg} onClick={onPlayPause}></img>
                        <img width="33px" className="click rewind" src="https://localhost:5001/images/rewind.svg"></img>
                    </div>
                </div>
                <div className="audio-player-right">
                    <div className="audio-player-duration">
                        <input className type="range" value={currentTime / maxTime} min="0" max="1" step="0.01"></input>
                    </div>
                    <div className="audio-player-right-info">
                        <img width="70px"
                            className="audio-player-trackinfo-artwork"
                            src={currentTrack.image}
                            alt={`track artwork for ${currentTrack.title} by ${currentTrack.artist}`}/>
                        <div className="audio-player-trackinfo">
                            <div className="audio-player-trackinfo-title">{currentTrack.title}</div>
                            <div className="artist"><b>{currentTrack.artist}</b></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
	);
}

export default Player;