import { useState, useEffect } from "react";
import { api } from "@services/api";
import { AuthResponse } from "@types";
import { useNavigate, useLocation } from "react-router-dom";

export const useAuthentication = () => {
  const [isAuth, setIsAuth] = useState<boolean>(false);
  const [isAwaitTwoFactor, setIsAwaitTwoFactor] = useState<boolean>(false);
  const [isLoading, setIsLoading] = useState(true);

  const navigate = useNavigate();
  const location = useLocation();

  const isAuthenticated = async (): Promise<void> => {
    api("/auth/", { method: "GET" })
      .then((reponse: any) => {
        const data = reponse.data as AuthResponse;
        if (data.awaitTwoFactorAuth) {
          navigate('/login/verify')
          setIsAwaitTwoFactor(true);
        } else if(data.AwaitForEmailVerification){
          navigate('/register', {state : { roomId: data.roomId }});
        }else {
          navigate("/");
          setIsAuth(true);
        }
        setIsLoading(false);
      })
      .catch((err: any) => {
        console.error(err);
        setIsAuth(false);
        setIsAwaitTwoFactor(false);
        setIsLoading(false);
      }).finally(() => {
        setIsLoading(false);
      })
  };

  useEffect(() => {
    const checkAuth = async () => {
      await isAuthenticated();
    };

    checkAuth();
  }, [location]);

  return { isAuth, isAwaitTwoFactor, isLoading };
};
