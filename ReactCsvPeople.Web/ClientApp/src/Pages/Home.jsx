﻿import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import axios from 'axios';

const Home = () => {

    const [people, setPeople] = useState([]);

    useEffect(() => {
        const getData = async () => {
            const { data } = await axios.get('/api/file/getpeople');
            setPeople(data);
            console.log(data);
        }
        getData();
    }, [])

    const onDeleteClick = async() => {
        await axios.post('/api/file/deletepeople');
        setPeople([''])
    }

    return (
        <>
            <div className="row">
                <div className="col-md-6 offset-md-3 mt-5">
                    <button className="btn btn-danger btn-lg w-100" onClick={onDeleteClick}>Delete All</button>
                </div>
            </div>

            <table className="table table-hover table-striped table-bordered mt-5">
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>First Name</th>
                        <th>Last Name</th>
                        <th>Age</th>
                        <th>Address</th>
                        <th>Email</th>
                    </tr>
                </thead>
                <tbody>
                    {people.map(p => (
                        <tr key={p.id}>
                            <td>{p.id}</td>
                            <td>{p.firstName}</td>
                            <td>{p.lastName}</td>
                            <td>{p.age}</td>
                            <td>{p.address}</td>
                            <td>{p.email}</td>
                        </tr>
                        ))}
                </tbody>
            </table>
        </>)

}
export default Home;