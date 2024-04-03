import { Suspense, lazy } from 'react';
import useDayNightTheme from './hooks/useDayNightTheme.hook'
import { Routes, Route } from 'react-router-dom';
import { useAuthentication } from '@hooks/useAuthenticate.hook.ts';

import Loader from './pages/Loader/index';
import ProtectedPage from './ProtectedPage';

const Login = lazy(() => import('./pages/Login/index'));
const Register = lazy(() => import('./pages/Register/index'));
const VerifyEmail = lazy(() => import('./pages/Register/Verify/index'))
const TwoFactorAuthentication = lazy(() => import("./pages/Login/TwoFactorAuthentication/index"));
const Home = lazy(() => import('./pages/Home/index'));
const ResetPassword = lazy(() => import('./pages/ResetPassword/index'))


function App() {
  useDayNightTheme();

  const { isLoading } = useAuthentication();

  if (isLoading) {
    return <Loader />;
  }

  return (
    <div className='bg-[#F5F5F5] dark:bg-base-100'>
      <Suspense fallback= {<Loader />}>
        <Routes>
          <Route path='/login' element={<Login/>} />
          <Route path='/register' element={<Register/>}/>
          <Route path ='/register/verify/:token' element={<VerifyEmail/>}/>
          <Route path='/password/reset/:token' element={<ResetPassword/>}/>
          <Route path='/home' element={<Home/>}/>
          <Route element={<ProtectedPage />}>
            <Route path='/' element={<Home/>}/>
            <Route path='/login/verify' element={<TwoFactorAuthentication/>}/>
          </Route>
        </Routes>
      </Suspense>
    </div>
  );
}

export default App;
