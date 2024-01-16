import { Routes, Route } from 'react-router-dom';
import { Suspense } from 'react';
import './App.css';

import Loader from './components/Loader';

function App() {

  return (
    <div className='App'>
      <Suspense
        fallback= {<Loader />}
      >
        <Routes>
          <Route path="/" element={<h1>Home</h1>} />
          <Route path="/about" element={<h1>About</h1>} />
        </Routes>
      </Suspense>
    </div>
  );
}

export default App;
