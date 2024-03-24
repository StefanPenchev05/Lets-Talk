import { useState, useEffect } from "react";
import { api } from "@services/api";
import { AuthResponse } from "@types";

export const useAuthentication = () => {
  const [isAuth, setIsAuth] = useState<boolean>(false);
  const [isAwaitTwoFactor, setIsAwaitTwoFactor] = useState<boolean>(false);
  const [isLoading, setIsLoading] = useState(true);

  const isAuthenticated = async (): Promise<void> => {
    api("http://localhost:5295/auth/", { method: "GET" })
      .then((reponse: any) => {
        const data = reponse.data as AuthResponse;
        console.log(data);
        if (data.awaitTwoFactorAuth) {
          setIsAwaitTwoFactor(true);
        } else {
          setIsAuth(true);
        }
        setIsLoading(false);
      })
      .catch((err: any) => {
        console.error(err);
        setIsAuth(false);
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
  }, []);

  return { isAuth, isAwaitTwoFactor, isLoading };
};
