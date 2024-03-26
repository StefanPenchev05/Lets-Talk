import React, { useState } from "react";
import { useWindowResize } from "../../hooks/useWindowResize.hook";
import { useFormSubmit } from "../../hooks/login/useFormSubmit.hook";

import Subtitle from "@components/login/Subtitle";
import SubmitButton from "@components/shared/SubmitButton";
import EmailInput from "@components/shared/EmailInput";
import PasswordInput from "@components/shared/PasswordInput";
import DividerField from "@components/shared/DividerFiled";
import SocialButton from "@components/login/SocialButton";

import ManImg from "../../assets/icons/man.png";
import Wallpaper from "../../assets/wallpaper/LoginWallpaper.png";
import Loader from "../Loader";

const RESOLUTION_THRESHOLD = 1022;

const Login: React.FC = () => {
  const [email, setEmail] = useState<string>("");
  const [password, setPassword] = useState<string>("");
  const windowWidth = useWindowResize();
  const { isLoading, emailError, passwordError, handleFormSubmit } = useFormSubmit(
    email,
    password
  );

  if(isLoading){
    return <Loader/>
  }

  return (
    <div className="flex flex-col md:flex-row items-center justify-center h-screen md:h-[100dvh] w-full">
      <div className="hidden lg:block w-3/4 h-full bg-white dark:bg-[#150f38]">
        <img
          src={Wallpaper}
          alt="Login Wallpaper"
          className="w-full h-full object-cover"
        />
      </div>
      <div className="w-full h-screen lg:w-1/4 flex flex-col items-center justify-center max-md:px-8 md:p-8 loginBox dark:bg-[#040622]">
        <img src={ManImg} alt="Avatar" className="w-1/3 mb-5" />
        {windowWidth <= RESOLUTION_THRESHOLD ? (
          <Subtitle align="center" gutterBottom />
        ) : (
          <Subtitle align="left" gutterBottom />
        )}
        <form
          className="flex flex-col space-y-4 w-full mb-9"
          onSubmit={handleFormSubmit}
          method="post"          
        >
          <EmailInput email={email} setEmail={setEmail} error={emailError} />
          <PasswordInput
            password={password}
            setPassword={setPassword}
            error={passwordError}
          />
          <p className="text-base w-full text-end text-[#6F58C1] dark:text-[#8B71DD] underline">
            Forgot Password?
          </p>
          <SubmitButton helperText="Sign in" />
        </form>
        <DividerField helperText="Login with social media" />
        <SocialButton />
        <p className="text-base w-full text-center text-black dark:text-white">
          Don't have an account?&nbsp;
          <a href="/register">
            <span className="underline text-[#6F58C1] dark:text-[#8B71DD]">
              Sign Up
            </span>
          </a>
        </p>
      </div>
    </div>
  );
};

export default Login;
