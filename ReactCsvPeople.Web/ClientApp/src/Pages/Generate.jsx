import React, { useState, useEffect, useRef } from 'react';
import axios from 'axios';

const Generate = () => {

    const generateNum = useRef();

    const onGenerateClick = async () => {
        const amount = generateNum.current.value;
        const response = await axios.get(`/api/file/generate/${amount}`);

        const contentDispositionHeader = response.headers['content-disposition'];
        const matches = contentDispositionHeader.match(/filename\*=(?<encoding>\S*)'(?<name>.*)/i);
        const fileName = decodeURIComponent(matches.groups.name);
        console.log(fileName);

        window.location.href = `/api/file/view?name=${fileName}`;
    };

    return (
        <div className="d-flex w-100 justify-content-center align-self-center" style={{ marginTop: '400px' }}>
            <div className="row">
                <input type="text" ref={generateNum} className="form-control-lg" placeholder="Amount" />
            </div>
            <div className="row">
                <div className="col-md-4 offset-md-2">
                    <button className="btn btn-primary btn-lg" onClick={onGenerateClick}>Generate</button>
                </div>
            </div>
        </div>
    )

}
export default Generate;