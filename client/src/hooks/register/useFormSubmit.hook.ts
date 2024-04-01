import { useState, FormEvent, createRef } from "react";
import { api } from "@services/api";
import { validateRegsiterForm } from "@utils/validations";
import { CustomError } from "@utils/CustomError";
import { RegisterResponse, VerifiedEmailSignalRResponse } from "@types";
import SignalRConnection from "@services/signalR";

import { useNavigate } from "react-router-dom";

export const useFormSubmit = (
  email: string,
  password: string,
  username: string,
  firstName: string,
  lastName: string,
  image: File | null,
  isTwoFactor: boolean,
  connection: SignalRConnection
) => {
  const [emailError, setEmailError] = useState<string>("");
  const [passwordError, setPasswordError] = useState<string>("");
  const [usernameError, setUsernameError] = useState<string>("");
  const [firstNameError, setFirstNameError] = useState<string>("");
  const [lastNameError, setLastNameError] = useState<string>("");
  const [iamgeError, setImageNameError] = useState<string>("");
  const [verifyLoading, setVerifyLoading] = useState<boolean>(true);

  const refDialog = createRef<HTMLDialogElement>();
  const naviagte = useNavigate();

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
            await connection.start();
            connection.JoinRoom(data.roomId);
            connection.onMessage("JoinedRoom", () => {
              refDialog.current?.showModal();
            });
            connection.onMessage("VerifiedEmail", async(data: VerifiedEmailSignalRResponse ) => {
              if(data.verifiedEmail){
                setVerifyLoading(false);
                await api(`/auth/register/getSession?token=${data.encryptUserId}`, {method: "GET",})
                  .then(() => {
                    naviagte("/");
                  })
                  .catch(() => {
                    //snackBar
                  })
              }
            })
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
    verifyLoading,
    refDialog,
    handleFormSubmit,
  };
};
