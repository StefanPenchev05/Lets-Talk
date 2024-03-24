import { Outlet, Navigate } from "react-router-dom";
import { ProtectedPage } from "@types";

const ProtectedRoute: React.FC<ProtectedPage> = ({ isAuth, isAwaitTwoFactor }) => {

  return <div>{isAuth ? <Outlet /> : <Navigate to={isAwaitTwoFactor ? "/login/verify" : "/login"} />}</div>;
};

export default ProtectedRoute;
