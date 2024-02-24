import { useState, FormEvent } from "react";
import { api } from "../../services/api";
import { validateLogInForm } from "../../utils/validations";
import { CustomError } from "../../utils/CustomError";

export const useFormSubmit = (email: string, password: string) => {
  const [emailError, setEmailError] = useState<string>("");
  const [passwordError, setPasswordError] = useState<string>("");

  const handleFormSubmit = async (e: FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    setEmailError("");
    setPasswordError("");
    try {
      await validateLogInForm(email, password);
      const response = await api("api/login", {
        method: "POST",
        data: JSON.stringify({ EmailOrUsername: email, password: password }),
      });
      console.log(response);
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
