import { Outlet, Navigate } from "react-router-dom";
import { ProtectedPage } from "@types";

const ProtectedRoute: React.FC<ProtectedPage> = ({ isAuth }) => {

  return <div>{isAuth ? <Outlet /> : <Navigate to={"/login"} />}</div>;
};

export default ProtectedRoute;
