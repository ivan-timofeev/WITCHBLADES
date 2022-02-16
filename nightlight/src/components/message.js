import React, { Component } from 'react';
import '../styles/message.css'

export default class Message extends Component {

    constructor(props) {
        super(props);

        this.dislike = this.dislike.bind(this);
        this.like = this.like.bind(this);

        this.state = {
            messageId: props.messageId,
            avatarUrl: props.avatarUrl ?? '/images/defaultAvatar.png',
            messageAuthor: props.messageAuthor ?? 'No data',
            messageBody: props.messageBody ?? 'No data',
            likesCount: props.likesCount ?? 0,
            errorClassname: "error hidden"
        };
    }

    async dislike() {
        let newLikeCount = --this.state.likesCount;
        this.setState(this.state);

        const response = await fetch(`api/Messages/ReduceLikesToMessage/${this.state.messageId}`, { method: 'POST' });

        if (response.status !== 200) {
            this.state.likesCount = newLikeCount + 1;
            this.state.errorClassname = "error";
            this.setState(this.state);
        }

        const data = await response.json();

        if (newLikeCount !== data.likesCount) {
            this.state.likesCount = data.likesCount;
            this.setState(this.state);
        }
    }

    async like() {
        let newLikeCount = ++this.state.likesCount;
        this.setState(this.state);

        const response = await fetch(`api/Messages/IncreaseLikesToMessage/${this.state.messageId}`, { method: 'POST' });

        if (response.status !== 200) {
            this.state.likesCount = newLikeCount - 1;
            this.state.errorClassname = "error";
            this.setState(this.state);
        }

        const data = await response.json();

        if (newLikeCount !== data.likesCount) {
            this.state.likesCount = data.likesCount;
            this.setState(this.state);
        }
    }

    
    render() {
        
        return (
            <div className="message">
                <div className="message-content">
                    <div>
                        <img
                            className="author-avatar"
                            alt="Author Avatar"
                            src={this.state.avatarUrl}
                            width="40px"
                            height="40px"/>
                    </div>
                    <div className="message-right">
                        <div className="message-author">{this.state.messageAuthor}</div>
                        <div className="message-body">{this.state.messageBody}</div>
                        <div className="message-reactions">
                            <div className="reaction" onClick={this.like}>👍🏻</div>
                            <div className="likes-count">{this.state.likesCount}</div>
                            <div className="reaction" onClick={this.dislike}>👎🏿</div>
                        </div>
                    </div>
                </div>
                
                <div className={this.state.errorClassname}>
                    Не удалось установить реакцию. Сервис недоступен
                </div>
            </div>
        );
    }

}
