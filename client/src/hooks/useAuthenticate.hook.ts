import { useState, useEffect } from "react";
import { api } from "@services/api";
import { AuthSessionResponse } from "@types";

export const useAuthentication = () => {
    const [type, setType] = useState<"success" | "error" | "warning">("warning");
    const [message, setMessage] = useState<string>("");
    const [isAuth, setIsAuth] = useState<boolean>(false);
    const [isLoading, setIsLoading] = useState(true);

    const isAuthenticated = async(): Promise<void> => {
        try{
            const response = await api("http://localhost:5295/auth/session", {method: "GET"}) as AuthSessionResponse;
            console.log(response);
            setType("success");
            setMessage(response.message);
            setIsAuth(true);
        }catch(err: any){
            const data = err.response.data as AuthSessionResponse
            setType("error");
            setMessage(data.message);
            setIsAuth(false);
        } finally {
            setIsLoading(false);
        }
    }

    useEffect(() => {
        const checkAuth = async () => {
            await isAuthenticated();
          };
      
          checkAuth();
    }, []);

    return {isAuth,type, message, isLoading}
};
