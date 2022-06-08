import axios from 'axios';


const GetAxios = () => {
    const headers = {};
    const token = localStorage.getItem('auth-token');
    headers['Authorization'] = `Bearer ${token}`;
    return axios.create({
        headers
    });
}
export default GetAxios;