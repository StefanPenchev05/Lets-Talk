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
    dispatch(checkAuth());
  }, [location])

  useEffect(() => {
      if(!location.pathname.startsWith("/register/verify/")){
        if(isAuth){
          navigate("/");
        }else if(isAwaitTwoFactor){
          navigate("/login/verify");
        }else if(isAwaitEmailVerifiaction){
          navigate("/register");
        }
      }
  }, [isAuth, isAwaitTwoFactor, isAwaitEmailVerifiaction]);

  return { isAwaitEmailVerifiaction, roomId, isLoading };
};
