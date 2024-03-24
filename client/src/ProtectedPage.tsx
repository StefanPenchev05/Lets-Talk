import { Outlet, Navigate } from "react-router-dom";
import { ProtectedPages } from "@types";

const ProtectedRoute: React.FC<ProtectedPages> = ({ isAuth, isAwaitTwoFactor }) => {

  console.log(isAwaitTwoFactor);

  return <div>{isAuth ? <Outlet /> : (isAwaitTwoFactor ? <Outlet/> : <Navigate to={"/login"} /> )}</div>;
};

export default ProtectedRoute;
