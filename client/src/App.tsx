import { Suspense, lazy, useEffect } from 'react';
import useDayNightTheme from './hooks/useDayNightTheme.hook'
import { Routes, Route, useNavigate } from 'react-router-dom';
import { useAuthentication } from '@hooks/useAuthenticate.hook.ts';



import Loader from './pages/Loader/index';
import ProtectedPage from './ProtectedPage';;

const Login = lazy(() => import('./pages/Login/index'));
const TwoFactorAuthentication = lazy(() => import("./pages/TwoFactorAuthentication/index"))

function App() {
  useDayNightTheme();

  const { isAuth, isAwaitTwoFactor, isLoading } = useAuthentication();
  const navigate = useNavigate();


  useEffect(() => {
    if(isAwaitTwoFactor){
      navigate('/login/verify');
    }
  },[isAwaitTwoFactor, isLoading])

  if (isLoading) {
    return <Loader />;
  }

  return (
    <div className='bg-[#F5F5F5] dark:bg-base-100'>
      <Suspense fallback= {<Loader />}>
        <Routes>
          <Route path="/login" element={<Login/>} />
          <Route element={<ProtectedPage isAuth={isAuth} isAwaitTwoFactor= {isAwaitTwoFactor}/>}>
            <Route path='/' element={<h1>Home</h1>}/>
            <Route path='/login/verify' element={<TwoFactorAuthentication/>}/>
          </Route>
        </Routes>
      </Suspense>
    </div>
  );
}

export default App;
