import { Outlet, Navigate } from "react-router-dom";
import { useAuthentication } from '@hooks/useAuthenticate.hook.ts';
import Loader from './pages/Loader/index';


const ProtectedRoute: React.FC = () => {
    const { isAuth, type, message, isLoading } = useAuthentication();

    if(isLoading){
      return <Loader/>
    }
    return <div>{isAuth ? <Outlet/> : <Navigate to={'/'}/>}</div>
};

export default ProtectedRoute;
