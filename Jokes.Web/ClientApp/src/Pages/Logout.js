import React, { useEffect } from 'react';
import axios from 'axios';
import { useHistory } from 'react-router-dom';
import { useAuthContext } from '../AuthContext';

const Logout = () => {

    const history = useHistory();
    const { setUser } = useAuthContext();

    useEffect(() => {
        const logout = () => {

            setUser(null);
            localStorage.removeItem('auth-token');

        }
        logout();
        history.push('/');
    }, [])
    return (
        <></>
    )


}
export default Logout;
