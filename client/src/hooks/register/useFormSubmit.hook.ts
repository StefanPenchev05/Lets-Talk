import { useState, FormEvent } from "react";
import { api } from "@services/api";
import { validateRegsiterForm } from "@utils/validations";
import { CustomError } from "@utils/CustomError";
import { RegisterResponse } from "@types";

import { useDispatch } from "react-redux";
import { AppDispatch } from "src/store/app";

export default function useFormSubmit(
  email: string,
  password: string,
  username: string,
  firstName: string,
  lastName: string,
  image: File | null,
  isTwoFactor: boolean,
) {
  const [emailError, setEmailError] = useState<string>("");
  const [passwordError, setPasswordError] = useState<string>("");
  const [usernameError, setUsernameError] = useState<string>("");
  const [firstNameError, setFirstNameError] = useState<string>("");
  const [lastNameError, setLastNameError] = useState<string>("");
  const [iamgeError, setImageNameError] = useState<string>("");

  const dispatch: AppDispatch = useDispatch();

  const formData = new FormData();

  const handleFormSubmit = async (e: FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    setEmailError("");
    setPasswordError("");
    setUsernameError("");
    setFirstNameError("");
    setLastNameError("");
    setImageNameError("");

    try {
      await validateRegsiterForm(
        email,
        password,
        username,
        firstName,
        lastName
      );
      formData.append("Email", email);
      formData.append("Username", username);
      formData.append("FirstName", firstName);
      formData.append("LastName", lastName);
      formData.append("Password", password);
      formData.append("TwoFactorAuth", JSON.stringify(isTwoFactor));
      formData.append("ProfilePicture", image ? image : "");

      await api("/auth/register/", {
        method: "POST",
        data: formData,
        headers: {
          "Content-Type": "multipart/form-data",
        },
      })
        .then(async (response: any) => {
          const data = response.data as RegisterResponse;
          if (data.awaitForEmailVerification && data.roomId) {
            
          }
        })
        .catch((err) => {
          console.log(err);
          const data = err.response.data as RegisterResponse;
          if (data.emailExists) {
            setEmailError(data.message);
          } else if (data.usernameExists) {
            setUsernameError(data.message);
          } else {
            // snackBar
            console.log(data.message);
          }
        });
    } catch (err) {
      if (err instanceof CustomError) {
        if (Object.values(err.details)[0] === "email") {
          return setEmailError(err.message);
        } else if (Object.values(err.details)[0] === "password") {
          return setPasswordError(err.message);
        } else if (Object.values(err.details)[0] === "username") {
          return setUsernameError(err.message);
        } else if (Object.values(err.details)[0] === "firstName") {
          return setFirstNameError(err.message);
        } else if (Object.values(err.details)[0] === "lastName") {
          return setLastNameError(err.message);
        } else {
          console.log("Hello from useForm errr");
          return setImageNameError(err.message);
        }
      } else {
        console.log("An unexpected error occurred");
        console.log(err);
      }
    }
  };

  return {
    usernameError,
    firstNameError,
    lastNameError,
    iamgeError,
    emailError,
    passwordError,
    handleFormSubmit,
  };
};
