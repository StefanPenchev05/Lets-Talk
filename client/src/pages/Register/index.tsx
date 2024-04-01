import React, { useState, useEffect, useRef } from "react";

import { RotateLoader } from "react-spinners";
import { FaCheckCircle } from "react-icons/fa";

import * as RegisterImports from "./imports";
import * as GlobalImports from "@globalImports"


const RESOLUTION_THRESHOLD = 1022;
const connection = new RegisterImports.SignalRConnection("/RegisterHub");

const Register: React.FC = () => {
  const [email, setEmail] = useState<string>("");
  const [password, setPassword] = useState<string>("");
  const [confirmPassword, setConfirmPassword] = useState<string>("");
  const [username, setUsername] = useState<string>("");
  const [firstName, setFirstName] = useState<string>("");
  const [lastName, setLastName] = useState<string>("");
  const [image, setImage] = useState<File | null>(null);
  const [isTwoFactor, setIsTwoFactor] = useState<boolean>(false);
  const [verifyLoading, setVerifyLoading] = useState<boolean>(true);

  const refDialog = useRef<HTMLDialogElement>(null);

  const { roomId } = RegisterImports.useAppSelector((state) => state.auth);
  const dispatch = RegisterImports.useAppDispatch();

  const windowWidth = GlobalImports.useWindowResize();

  const {
    usernameError,
    firstNameError,
    lastNameError,
    emailError,
    passwordError,
    handleFormSubmit,
  } = RegisterImports.useFormSubmit(
    email,
    password,
    username,
    firstName,
    lastName,
    image,
    isTwoFactor
  );

  useEffect(() => {
    const connectToRoom = async () => {
      if (roomId) {
        await connection.start();
        connection.JoinRoom(roomId);
        connection.onMessage("JoinedRoom", (data) => {
          refDialog.current?.showModal();
          console.log(data)
        });

        connection.onMessage(
          "VerifiedEmail",
          async (data: RegisterImports.VerifiedEmailSignalRResponse) => {
            console.log(data);
            console.log(data.message);
            if (data.verifiedEmail) {
              setVerifyLoading(false);
              await GlobalImports.API(`/auth/register/getSession?token=${data.encryptUserId}`,{ method: "GET" })
                .then(() => {
                  dispatch(RegisterImports.setIsAuth(true));
                });
            }
          }
        );
      }
    };

    connectToRoom();
  }, [roomId]);

  return (
    <div className="flex flex-col md:flex-row items-center justify-center h-screen md:h-[100dvh] w-full">
      <div className="hidden lg:block w-3/4 h-full bg-white dark:bg-[#150f38]">
        <img
          src={RegisterImports.Wallpaper}
          alt="Register Wallpaper"
          className="w-full h-full object-cover"
        />
      </div>
      <div className="w-full h-screen lg:w-1/4 flex flex-col items-center justify-center max-md:px-8 md:p-8 loginBox dark:bg-[#040622]">
        {image ? (
          <div className="avatar relative">
            <div className="w-24 h-24 md:w-26 md:h-26 lg:w-40 lg:h-40 rounded-full mb-5">
              <img src={image ? URL.createObjectURL(image) : ""} />
              <button
                className="absolute top-0 right-0 lg:top-1 lg:right-2 bg-red-500 w-7 rounded-full text-center text-xl text-white"
                onClick={() => setImage(null)}
              >
                x
              </button>
            </div>
          </div>
        ) : (
          <label
            htmlFor="image"
            className="relative w-24 h-24 md:w-26 md:h-26 lg:w-36 lg:h-40 rounded-full border-2 border-dashed mb-5 cursor-pointer flex items-center justify-center"
          >
            <input
              type="file"
              id="image"
              className="hidden"
              placeholder="Please Upload Image"
              onChange={(e) => setImage(e.target.files![0])}
            />
            <span className="text-gray-500 w-2/3 text-center">
              Upload Your Avatar
            </span>
          </label>
        )}
        {windowWidth <= RESOLUTION_THRESHOLD ? (
          <RegisterImports.Subtitle align="center" gutterBottom />
        ) : (
          <RegisterImports.Subtitle align="left" gutterBottom />
        )}
        <form
          className="flex flex-col space-y-4 w-full mb-9"
          onSubmit={handleFormSubmit}
        >
          <RegisterImports.EmailInput email={email} setEmail={setEmail} error={emailError} />
          <RegisterImports.FirstAndLastName
            firstName={firstName}
            setFirstName={setFirstName}
            firstNameError={firstNameError}
            lastName={lastName}
            setLastName={setLastName}
            lastNameError={lastNameError}
          />
          <RegisterImports.Username
            Username={username}
            setUsername={setUsername}
            error={usernameError}
          />
          <RegisterImports.PasswordInput
            password={password}
            setPassword={setPassword}
            error={passwordError}
          />
          <RegisterImports.ConfirmPasswordInput
            confirmPassword={confirmPassword}
            setConfirmPassword={setConfirmPassword}
            error={passwordError}
          />

          <RegisterImports.TwoFactorAuthenticationButton
            twoFactorAuthentication={isTwoFactor}
            setTwoFactorAuthentication={setIsTwoFactor}
          />
          <RegisterImports.SubmitButton helperText="Sign in" />
        </form>
        <dialog className="modal modal-bottom sm:modal-middle" ref={refDialog}>
          <div className="modal-box">
            {verifyLoading ? (
              <div className="flex flex-col items-center justify-center space-y-9 p-6">
                <RotateLoader color="#36d7b7" />
                <p>
                  Check your email. Your Verifican Link{" "}
                  <span className="text-red-500 font-bold">
                    expirese after 15 minutes
                  </span>
                </p>
              </div>
            ) : (
              <FaCheckCircle color="green" size="1em" />
            )}
          </div>
        </dialog>
      </div>
    </div>
  );
};

export default Register;
