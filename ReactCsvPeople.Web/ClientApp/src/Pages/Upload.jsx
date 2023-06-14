import React, { useState, useEffect, useRef } from 'react';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';



const Upload = () => {

    const fileRef = useRef(null);
    const navigate = useNavigate();

    const toBase64 = file => {
        return new Promise((resolve, reject) => {
            const reader = new FileReader();
            reader.readAsDataURL(file);
            reader.onload = () => resolve(reader.result);
            reader.onerror = error => reject(error);
        });
    }

    const onUploadClick = async () => {
        const file = fileRef.current.files[0];
        const base64 = await toBase64(file);
        console.log(file.name);
        await axios.post('/api/file/upload', { base64, name: file.name });
        navigate('/');
    }

    return (

        <div className="d-flex w-100 justify-content-center align-self-center" style={{ marginTop: '400px' }}>
            <div className="row">
                <div className="col-md-10">
                    <input ref={fileRef} type="file" className="form-control" />
                </div>
                <div className="col-md-2">
                    <button className="btn btn-primary" onClick={onUploadClick}>Upload</button>
                </div>
            </div>
        </div>
    )

}
export default Upload;