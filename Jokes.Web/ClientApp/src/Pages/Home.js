import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useAuthContext } from '../AuthContext';
import { Link } from 'react-router-dom';
import GetAxios from '../AuthAxios';

const Home = () => {
    const { user } = useAuthContext();

    const [joke, setJoke] = useState({ id: '', setup: '', punchline: '' })
    const [loading, setIsLoading] = useState(true);


    const [userLikedJoke, setUserLikedJoke] = useState(true);
    const [likedOrDisliked, setLikedOrDisliked] = useState(true);

    const [likes, setLikes] = useState(0);
    const [dislikes, setDislikes] = useState(0);

    //const [timeLikedPlusFive, setTimeLikedPlusFive] = useState();


    useEffect(() => {
        const getJoke = async () => {

            const { data } = await axios.get('/api/home/getjoke');
            setJoke(data);
            await getLikes(data.id);
            if (user) {
                await getUserLikedJoke(data.id);
                const promise = await GetAxios().get(`/api/home/likedordisliked?jokeId=${data.id}`);
                setLikedOrDisliked(promise.data);

            }

            setIsLoading(false);
        }
        getJoke();
    }, [])

    setInterval(() => {
        getLikes(joke.id);

    }, 500)



    const getLikes = async (id) => {
        const promise = await axios.get(`/api/home/getlikesanddislikes?jokeId=${id}`);
        const { likes } = promise.data;
        const { dislikes } = promise.data;
        setLikes(likes);
        setDislikes(dislikes);
    }
    const getUserLikedJoke = async (id) => {
        const { data } = await GetAxios().get(`/api/home/getuserlikedjoke?jokeId=${id}`)
        setUserLikedJoke(data.liked);
    }


    const onLikeClick = async () => {
        const { data } = await GetAxios().post('/api/home/like', joke);
        //setTimeLikedPlusFive(data);
        await getLikes(joke.id);
        setUserLikedJoke(true);
        setLikedOrDisliked(true);


    }
    const onDislikeClick = async () => {
        const { data } = await GetAxios().post('/api/home/dislike', joke);
        //setTimeLikedPlusFive(data);
        await getLikes(joke.id);
        setUserLikedJoke(false);
        setLikedOrDisliked(true);

    }

    return (
        <div className="col-md-6 offset-md-3 card card-body bg-light">
            <div>
                {loading ? <h1>Loading...</h1> :
                    <div>
                        <h4>{joke.setup}</h4>
                        <h4>{joke.punchline}</h4>
                        <div>
                            {!user && <Link to='/login'>Login to your account to like/dislike the joke</Link>}
                        </div>
                        <h4>Likes: {likes}</h4>
                        <h4>Dislikes: {dislikes}</h4>
                        <h4><button className='btn btn-link' onClick={() => window.location.reload()}>refresh</button></h4>
                        {user &&
                            <div><button className='btn btn-primary' onClick={onLikeClick} disabled={userLikedJoke && likedOrDisliked}>Like</button>
                                <button className='btn btn-danger' onClick={onDislikeClick} disabled={!userLikedJoke && likedOrDisliked}>Dislike</button>
                            </div>}
                    </div>
                }

            </div>
        </div>
    )

}
export default Home;