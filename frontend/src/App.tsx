import { Routes, Route } from 'react-router-dom';
import { Suspense, lazy, useEffect } from 'react';

import Loader from './Page/Loader';

const Login = lazy(() => import('./Page/Login/index'));

function App() {

  useEffect(() => {
    const setTheme = () => {
      const currentHour = new Date().getHours();
      const isDayTime = currentHour > 6 && currentHour < 7;
      if(isDayTime){
        if(document.documentElement.classList.contains('dark')){
          document.documentElement.classList.remove('dark');
        }
      }else{
        if(!document.documentElement.classList.contains('dark')){
          document.documentElement.classList.add('dark');
        }
      }
    }

    setTheme();

    const intervalId = setInterval(setTheme, 1000)

    return () => {
      clearInterval(intervalId);
    };
  },[])

  return (
    <div className='bg-[#F5F5F5] dark:bg-[#272640]'>
      <Suspense fallback= {<Loader />}>
        <Routes>
          <Route path="/" element={<Login/>} />
          <Route path="/about" element={<h1>About</h1>} />
          <Route path="/loader" element={<Loader/>}/>
        </Routes>
      </Suspense>
    </div>
  );
}

export default App;
