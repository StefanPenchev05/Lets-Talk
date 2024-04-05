import React, { FormEvent, useRef, useState } from "react";

import * as GlobalImports from "@globalImports";
import * as LoginImports from "./imports";
import { api } from "@services/api";

const RESOLUTION_THRESHOLD = 1022;

const Login: React.FC = () => {
  const [email, setEmail] = useState<string>("");
  const [password, setPassword] = useState<string>("");
  const [showPassword, setShowPassword] = useState<boolean>(false);
  const refDialog = useRef<HTMLDialogElement>(null);

  const windowWidth = GlobalImports.useWindowResize();
  const { isLoading, emailError, passwordError, handleFormSubmit } =
    LoginImports.useFormSubmit(email, password);

  const handleSubmitFromResetPassword = async (
    e: FormEvent<HTMLFormElement>,
    emailOrUsername: string
  ) => {
    e.preventDefault();
    try {
      await api("/password/reset/", {
        method: "POST",
        data: JSON.stringify(emailOrUsername),
      });

      refDialog.current?.close();
    } catch (err: any) {
      console.log(err);
    }
  };
  
  if (isLoading) {
    return <LoginImports.Loader />;
  }

  return (
    <div className="flex flex-col md:flex-row items-center justify-center h-screen md:h-[100dvh] w-full">
      <div className="hidden lg:block w-3/4 h-full bg-white dark:bg-[#150f38]">
        <img
          src={LoginImports.Wallpaper}
          alt="Login Wallpaper"
          className="w-full h-full object-cover"
        />
      </div>
      <div className="w-full h-screen lg:w-1/4 flex flex-col items-center justify-center max-md:px-8 md:p-8 loginBox dark:bg-[#040622]">
        <img src={LoginImports.ManImg} alt="Avatar" className="w-1/3 mb-5" />
        {windowWidth <= RESOLUTION_THRESHOLD ? (
          <LoginImports.Subtitle align="center" gutterBottom />
        ) : (
          <LoginImports.Subtitle align="left" gutterBottom />
        )}
        <form
          className="flex flex-col space-y-4 w-full mb-9"
          onSubmit={handleFormSubmit}
          method="post"
        >
          <LoginImports.EmailInput
            email={email}
            setEmail={setEmail}
            error={emailError}
          />
          <LoginImports.PasswordInput
            password={password}
            showPassword={showPassword}
            setShowPassword={setShowPassword}
            setPassword={setPassword}
            error={passwordError}
          />
          <p
            className="text-base w-full hover:cursor-pointer text-end text-[#6F58C1] dark:text-[#8B71DD] underline"
            onClick={() => refDialog.current?.showModal()}
          >
            Forgot Password?
          </p>
          <LoginImports.SubmitButton helperText="Sign in" />
        </form>
        <LoginImports.DividerField helperText="Login with social media" />
        <LoginImports.SocialButton />
        <p className="text-base w-full text-center text-black dark:text-white">
          Don't have an account?&nbsp;
          <a href="/register">
            <span className="underline text-[#6F58C1] dark:text-[#8B71DD]">
              Sign Up
            </span>
          </a>
        </p>
      </div>
      <dialog ref={refDialog} className="modal">
        <div className="modal-box space-y-4 bg-white dark:bg-[#150f38]">
          <h3 className="font-bold text-lg text-black dark:text-white">
            Reset Password Function
          </h3>
          <form onSubmit={(e) => handleSubmitFromResetPassword(e, email)}>
            <LoginImports.EmailInput
              email={email}
              setEmail={setEmail}
              error={emailError}
            />
            <LoginImports.SubmitButton helperText="Send Email" />
          </form>
        </div>
        <form method="dialog" className="modal-backdrop">
          <button>close</button>
        </form>
      </dialog>
    </div>
  );
};

export default Login;
