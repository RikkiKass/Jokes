import React, { useState, useEffect } from 'react';
import axios from 'axios';

const Joke = ({ joke }) => {

    const [likes, setLikes] = useState(0);
    const [dislikes, setDislikes] = useState(0);

    const { setup, punchline, id } = joke;

    const getLikes = async () => {
        const promise = await axios.get(`/api/home/getlikes?jokeId=${id}`);
        const { likes } = promise.data;
        const { dislikes } = promise.data;
        setLikes(likes);
        setDislikes(dislikes);
    }

    useEffect(() => {
        getLikes();
    }, []
    )
    setInterval(() => {
        getLikes();
    }, 200)
    return (
        <div className="col-md-6 offset-md-3">
            <div className="card card-body bg-light mb-3">
                <h5>{setup}</h5>
                <h5>{punchline}</h5>
                <span>Likes: {likes}</span>
                <span>Dislikes: {dislikes}</span>
            </div>
        </div>
    )
}
export default Joke;