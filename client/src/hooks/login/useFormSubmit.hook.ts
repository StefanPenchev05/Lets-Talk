import { useState, FormEvent } from "react";
import { api } from "@services/api";
import { validateLogInForm } from "@utils/validations";
import { CustomError } from "@utils/CustomError";
import { LoginResponse } from "@types";
import { useNavigate } from "react-router-dom";


export const useFormSubmit = (email: string, password: string) => {
  const [emailError, setEmailError] = useState<string>("");
  const [passwordError, setPasswordError] = useState<string>("");
  const navigate = useNavigate();

  const handleFormSubmit = async (e: FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    setEmailError("");
    setPasswordError("");
    try {
      //await validateLogInForm(email, password);
      await api("http://localhost:5295/" + "auth/login", {
        method: "POST",
        data: JSON.stringify({ usernameOrEmail: email, Password: password }),
      })
        .then((response: any) => {
          console.log(response);
          const data = response.data as LoginResponse;
          if(data.twoFactorAwait === true){
            navigate("/")
          }
        })
        .catch((err) => {
          const response = err.response.data as LoginResponse;
          console.log(response.existingUser);
          if (response.existingUser === false) {
            setEmailError(response.message);
          }else if(response.incorrectPassword === false){
            setPasswordError(response.message);
          }
        });
    } catch (err) {
      if (err instanceof CustomError) {
        if (
          Object.values(err.details)[0] === "email" ||
          Object.values(err.details)[0] === "username"
        ) {
          return setEmailError(err.message);
        } else {
          setPasswordError(err.message);
        }
      } else {
        console.log("An unexpected error occurred");
      }
    }
  };

  return { emailError, passwordError, handleFormSubmit };
};
