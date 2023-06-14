import React from 'react';
import { Route, Routes } from 'react-router';
import Layout from './components/Layout';
import Home from './pages/Home';
import Generate from './pages/Generate';
import Upload from './pages/Upload';

const App = () => {
    return (
            <Layout>
                <Routes>
                    <Route exact path='/' element={<Home />} />
                    <Route exact path='/generate' element={<Generate />} />
                    <Route exact path='/upload' element={<Upload />} />
                </Routes>
            </Layout>

    );
}

export default App;