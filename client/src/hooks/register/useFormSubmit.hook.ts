import { useState, FormEvent } from "react";
import { api } from "@services/api";
import { validateRegsiterForm } from "@utils/validations";
import { CustomError } from "@utils/CustomError";
import { LoginResponse } from "@types";
import { useNavigate } from "react-router-dom";


export const useFormSubmit = (email: string, password: string, username: string, firstName: string, lastName: string, image: File | null, isTwoFactor: boolean) => {
  const [emailError, setEmailError] = useState<string>("");
  const [passwordError, setPasswordError] = useState<string>("");
  const [usernameError, setUsernameError] = useState<string>("");
  const [firstNameError, setFirstNameError] = useState<string>("");
  const [lastNameError, setLastNameError] = useState<string>("");
  const [iamgeError, setImageNameError] = useState<string>("");
  const [isTwoFactorError, setIsTwoFactorError] = useState<string>("");
  const [isLoading, setIsLoading] = useState<boolean>(false);

  const navigate = useNavigate();

  const handleFormSubmit = async (e: FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    setEmailError("");
    setPasswordError("");
    setUsernameError("");
    setFirstNameError("");
    setLastNameError("");
    setImageNameError("");
    setIsTwoFactorError("");


    try {
      //await validateRegsiterForm(email, password, username, firstName, lastName,image)
    } catch (err) {
    
    }
  };

  return { isLoading, emailError, passwordError, handleFormSubmit };
};
