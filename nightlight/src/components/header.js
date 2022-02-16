import React, { Component } from 'react';
import '../styles/header.css';
import { NavLink } from 'react-router-dom';

export default class Header extends Component {

    constructor(props) {
        super(props);
    }
     
    render() {
        
        return (
            <div>
                <div className="header">
                    <div className="logo">WITCHBLADES</div>
                </div>
                <div className="menu">
                    <div><NavLink className="menu-item" to="/" exact>Главная</NavLink></div>
                    <div><NavLink className="menu-item" to="/about" >Артисты</NavLink></div>
                </div> 
            </div>
            
        );
    }

}
