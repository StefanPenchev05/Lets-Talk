import { Suspense, lazy } from 'react';
import useDayNightTheme from './hooks/useDayNightTheme.hook'
import { Routes, Route } from 'react-router-dom';

import Loader from './pages/Loader/index';
import ProtectedPage from './ProtectedPage';

const Login = lazy(() => import('./pages/Login/index'));

function App() {
  useDayNightTheme();

  return (
    <div className='bg-[#F5F5F5] dark:bg-[#272640]'>
      <Suspense fallback= {<Loader />}>
        <Routes>
          <Route path="/" element={<Login/>} />
          <Route path="/loader" element={<Loader/>}/>
          <Route element={<ProtectedPage/>}>
            <Route path='/dashboard' element={<h1>Hello World</h1>}/>
          </Route>
        </Routes>
      </Suspense>
    </div>
  );
}

export default App;
