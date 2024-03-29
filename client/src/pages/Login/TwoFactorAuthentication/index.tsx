import React, { useState, createRef } from "react";
import Loader from "../../Loader";
import { api } from "@services/api";
import { useNavigate } from "react-router-dom";

const TwoFactorAuthentication: React.FC = () => {

  const [input1, setInput1] = useState("");
  const [input2, setInput2] = useState("");
  const [input3, setInput3] = useState("");
  const [input4, setInput4] = useState("");
  const [input5, setInput5] = useState("");

  const input2Ref = createRef<HTMLInputElement>();
  const input3Ref = createRef<HTMLInputElement>();
  const input4Ref = createRef<HTMLInputElement>();
  const input5Ref = createRef<HTMLInputElement>();

  const [isLoading, setIsLoading] = useState<boolean>(false);

  const navigate = useNavigate();

  const handleOnChange = (e: React.ChangeEvent<HTMLInputElement>, nextInput: React.RefObject<HTMLInputElement>) => {
    if (e.target.value) {
      nextInput.current?.focus();
    }
  }

  const handleOnSubmit = async(e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    setIsLoading(true);
    const code = input1 + input2 + input3 + input4 + input5;
    try{
      await api("/auth/login/verify/", {
        method: "POST", 
        data: JSON.stringify({TwoFactorAuthCode: code})
      })

      navigate("/");
    }catch(err){

    }
  }

  if(isLoading){
    return <Loader/>
  }

  return (
    <div className="container flex bg-transparent items-center justify-center min-h-screen mx-auto">
      <form onSubmit={handleOnSubmit}>
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
              value={input1}
              onChange={(e) => {setInput1(e.target.value); handleOnChange(e, input2Ref)}}
              maxLength={1}
              pattern="[\d]*"
              tabIndex={1}
              autoComplete="off"
            />
            <input
              ref={input2Ref}
              className="w-14 h-24 bg-transparent border-b-4 text-center text-black dark:text-white"
              placeholder="."
              type="tel"
              value={input2}
              onChange={(e) => {setInput2(e.target.value); handleOnChange(e, input3Ref)}}
              maxLength={1}
              pattern="[\d]*"
              tabIndex={2}
              autoComplete="off"
            />
            <input
              ref={input3Ref}
              className="w-14 h-24 bg-transparent border-b-4 text-center text-black dark:text-white"
              placeholder="."
              type="tel"
              value={input3}
              onChange={(e) => {setInput3(e.target.value); handleOnChange(e, input4Ref)}}
              maxLength={1}
              pattern="[\d]*"
              tabIndex={3}
              autoComplete="off"
            />
            <input
              ref={input4Ref}
              className="w-14 h-24 bg-transparent border-b-4 text-center text-black dark:text-white"
              placeholder="."
              type="tel"
              value={input4}
              onChange={(e) => {setInput4(e.target.value); handleOnChange(e, input5Ref)}}
              maxLength={1}
              pattern="[\d]*"
              tabIndex={4}
              autoComplete="off"
            />
            <input
              ref={input5Ref}
              className="w-14 h-24 bg-transparent border-b-4 text-center text-black dark:text-white"
              placeholder="."
              type="tel"
              value={input5}
              onChange={(e) => setInput5(e.target.value)}
              maxLength={1}
              pattern="[\d]*"
              tabIndex={5}
              autoComplete="off"
            />
          </div>
          <button className="btn">Continue</button>
        </div>
      </form>
    </div>
  );
};

export default TwoFactorAuthentication;
