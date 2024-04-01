import { Outlet, Navigate } from "react-router-dom";
import { useSelector } from "react-redux";
import { RootState } from "./store/app";

const ProtectedRoute: React.FC = () => {

  const { isAuth, isAwaitTwoFactor } = useSelector((state: RootState) => state.auth);

  return <div>{isAuth ? <Outlet /> : (isAwaitTwoFactor ? <Outlet/> : <Navigate to={"/login"} /> )}</div>;
};

export default ProtectedRoute;
