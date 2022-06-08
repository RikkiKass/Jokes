import React, { useEffect, useState } from "react";
import axios from "axios";
import Joke from '../Components/Joke';

const ViewAll = () => {

    const [jokes, setJokes] = useState([]);

    useEffect(() => {
        const getJokes = async () => {
            const { data } = await axios.get('/api/home/viewall');
            setJokes(data);
        }
        getJokes();

    }, [])



    return (
        jokes.map(j => <Joke key={j.id} joke={j} />)

    )
}
export default ViewAll;