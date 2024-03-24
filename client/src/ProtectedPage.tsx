import { Outlet, Navigate } from "react-router-dom";
import { ProtectedPage } from "@types";
import Loader from "./pages/Loader";

const ProtectedRoute: React.FC<ProtectedPage> = ({ isAuth, isAwaitTwoFactor, isLoading }) => {

  if (isLoading) {
    return <Loader />;
  }

  return <div>{isAuth ? <Outlet /> : (isAwaitTwoFactor ? <Outlet/> : <Navigate to={"/login"} /> )}</div>;
};

export default ProtectedRoute;
