import { useState, FormEvent } from "react";
import { api } from "@services/api";
import { validateRegsiterForm } from "@utils/validations";
import { CustomError } from "@utils/CustomError";
import { useNavigate } from "react-router-dom";


export const useFormSubmit = (email: string, password: string, username: string, firstName: string, lastName: string, image: File | null, isTwoFactor: boolean) => {
  const [emailError, setEmailError] = useState<string>("");
  const [passwordError, setPasswordError] = useState<string>("");
  const [usernameError, setUsernameError] = useState<string>("");
  const [firstNameError, setFirstNameError] = useState<string>("");
  const [lastNameError, setLastNameError] = useState<string>("");
  const [iamgeError, setImageNameError] = useState<string>("");
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

    try {
      await validateRegsiterForm(email, password, username, firstName, lastName, image);
      await api("/auth/register/", {
        method: "POST",
        data: JSON.stringify({
          Email: email, Username: username,
          FirstName: firstName, LastName: lastName,
          Password: password, ProfilePicture: image,
          TwoFactorAuth: isTwoFactor
        })
      });
    } catch (err) {
        if(err instanceof CustomError){
          if(Object.values(err.details)[0] === "email"){
            return setEmailError(err.message);
          }else if(Object.values(err.details)[0] === "password"){
            return setPasswordError(err.message);
          }else if(Object.values(err.details)[0] === "username"){
            return setUsernameError(err.message);
          }else if(Object.values(err.details)[0] === "firstName"){
            return setFirstNameError(err.message);
          }else if(Object.values(err.details)[0] === "lastName"){
            return setLastNameError(err.message);
          }else{
            return setImageNameError(err.message);
          }
      }else {
        console.log("An unexpected error occurred");
      }
    }
  };

  return { isLoading, usernameError, firstNameError, lastNameError, iamgeError, emailError, passwordError, handleFormSubmit };
};
