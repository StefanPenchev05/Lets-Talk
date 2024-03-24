import React, { useState } from "react";

const TwoFactorAuthentication: React.FC = () => {

  const [code, SetCode] = useState<string | undefined>(undefined);

  const handleOnSubmit = async() => {

  }

  return (
    <div className="container flex bg-transparent items-center justify-center min-h-screen mx-auto">
      <form action="">
        <div className="card w-96 bg-transparent shadow-xl p-8">
          <div className="text-center text-4xl text-black dark:text-white font-thin">
            Two-Factor <br /> Authentication
          </div>
          <div className="divider divider-neutral dark:divider mx-8"/>
          <p className="text-center text-black dark:text-white">
            Enter 6-digit code from <br /> your authenticator application
          </p>
          <div className="card-body justify-center flex-row space-x-2">
            <input
              className="w-14 h-24 bg-transparent border-b-4 text-center text-black dark:text-white"
              placeholder="."
              type="tel"
              maxLength={1}
              pattern="[\d]*"
              tabIndex={1}
              autoComplete="off"
            />
            <input
              className="w-14 h-24 bg-transparent border-b-4 text-center text-black dark:text-white"
              placeholder="."
              type="tel"
              maxLength={1}
              pattern="[\d]*"
              tabIndex={2}
              autoComplete="off"
            />
            <input
              className="w-14 h-24 bg-transparent border-b-4 text-center text-black dark:text-white"
              placeholder="."
              type="tel"
              maxLength={1}
              pattern="[\d]*"
              tabIndex={3}
              autoComplete="off"
            />
            <input
              className="w-14 h-24 bg-transparent border-b-4 text-center text-black dark:text-white"
              placeholder="."
              type="tel"
              maxLength={1}
              pattern="[\d]*"
              tabIndex={4}
              autoComplete="off"
            />
            <input
              className="w-14 h-24 bg-transparent border-b-4 text-center text-black dark:text-white"
              placeholder="."
              type="tel"
              maxLength={1}
              pattern="[\d]*"
              tabIndex={5}
              autoComplete="off"
            />
          </div>
          <button className="btn"> Continue</button>
        </div>
      </form>
    </div>
  );
};

export default TwoFactorAuthentication;
