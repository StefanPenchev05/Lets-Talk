import { useEffect } from "react";
import { useNavigate, useLocation } from "react-router-dom";

import { checkAuth } from "src/features/authSlice";
import useAppSelector from "./useAppSelector.hook";
import useAppDispatch  from "./useAppDispatch.hook";

export const useAuthentication = () => {

  const navigate = useNavigate();
  const location = useLocation();
  const dispatch = useAppDispatch();
  const { isAuth, isAwaitTwoFactor, isAwaitEmailVerifiaction, roomId, isLoading } = useAppSelector((state) => state.auth);

  const isAuthenticated = async (): Promise<void> => {
    dispatch(checkAuth());
    if(isAuth){
      navigate("/");
    }else if(isAwaitTwoFactor){
      navigate("/login/verify");
    }else if(isAwaitEmailVerifiaction){
      navigate("/register");
    }
  };

  useEffect(() => {
    const checkAuth = async () => {
      await isAuthenticated();
    };

    checkAuth();
  }, [location, isAuth]);

  return { isAwaitEmailVerifiaction, roomId, isLoading };
};
