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

  useEffect(() => {
    console.log(window.location.href);
    const isAuthenticated = async (): Promise<void> => {
      if(isAuth){
        navigate("/");
      }else if(isAwaitTwoFactor){
        navigate("/login/verify");
      }else if(isAwaitEmailVerifiaction){
        navigate("/register");
      }else{
        await dispatch(checkAuth());
      }
    };

    isAuthenticated();
  }, [location]);

  return { isAwaitEmailVerifiaction, roomId, isLoading };
};
