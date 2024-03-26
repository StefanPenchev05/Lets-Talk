import React, { useState } from "react";
import { useWindowResize } from "../../hooks/useWindowResize.hook";
import { useFormSubmit } from "../../hooks/register/useFormSubmit.hook";

import Subtitle from "@components/login/Subtitle";
import SubmitButton from "@components/shared/SubmitButton";
import EmailInput from "@components/register/EmailInput";
import Username from "@components/shared/Username";
import PasswordInput from "@components/shared/PasswordInput";
import ConfirmPasswordInput from "@components/shared/ConfirmPasswordInput";
import FirstAndLastName from "@components/shared/FirstAndLastName";
import TwoFactorAuthenticationButton from "@components/register/TwoFactorAuthenticationButton";

import Wallpaper from "../../assets/wallpaper/LoginWallpaper.png";
import Loader from "../Loader";

const RESOLUTION_THRESHOLD = 1022;

const Register: React.FC = () => {
  const [email, setEmail] = useState<string>("");
  const [password, setPassword] = useState<string>("");
  const [confirmPassword, setConfirmPassword] = useState<string>("");
  const [username, setUsername] = useState<string>("");
  const [firstName, setFirstName] = useState<string>("");
  const [lastName, setLastName] = useState<string>("");
  const [image, setImage] = useState<File | null>(null);
  const [isTwoFactor, setIsTwoFactor] = useState<boolean>(false);

  const windowWidth = useWindowResize();
  const { isLoading, emailError, passwordError, handleFormSubmit } =
    useFormSubmit(
      email,
      password,
      username,
      firstName,
      lastName,
      image,
      isTwoFactor
    );

  if (isLoading) {
    return <Loader />;
  }

  return (
    <div className="flex flex-col md:flex-row items-center justify-center h-screen md:h-[100dvh] w-full">
      <div className="hidden lg:block w-3/4 h-full bg-white dark:bg-[#150f38]">
        <img
          src={Wallpaper}
          alt="Register Wallpaper"
          className="w-full h-full object-cover"
        />
      </div>
      <div className="w-full h-screen lg:w-1/4 flex flex-col items-center justify-center max-md:px-8 md:p-8 loginBox dark:bg-[#040622]">
        {image ? (
          <div className="avatar relative">
            <div className="w-24 h-24 md:w-26 md:h-26 lg:w-40 lg:h-40 rounded-full mb-5">
              <img
                src={image ? URL.createObjectURL(image) : ""}
              />
              <button
                className="absolute top-0 right-0 lg:top-1 lg:right-2 bg-red-500 w-7 rounded-full text-center text-xl text-white"
                onClick={() => setImage(null)}
              >
                x
              </button>
            </div>
          </div>
        ):(
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
          <Subtitle align="center" gutterBottom />
        ) : (
          <Subtitle align="left" gutterBottom />
        )}
        <form
          className="flex flex-col space-y-4 w-full mb-9"
          onSubmit={handleFormSubmit}
        >
          <EmailInput email={email} setEmail={setEmail} error={emailError} />
          <FirstAndLastName
            firstName={firstName}
            setFirstName={setFirstName}
            lastName={lastName}
            setLastName={setLastName}
          />
          <Username Username={username} setUsername={setUsername} />
          <PasswordInput
            password={password}
            setPassword={setPassword}
            error={passwordError}
          />
          <ConfirmPasswordInput
            confirmPassword={confirmPassword}
            setConfirmPassword={setConfirmPassword}
            error={passwordError}
          />

          <TwoFactorAuthenticationButton
            twoFactorAuthentication={isTwoFactor}
            setTwoFactorAuthentication={setIsTwoFactor}
          />
          <SubmitButton helperText="Sign in" />
        </form>
      </div>
    </div>
  );
};

export default Register;
