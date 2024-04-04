// ResetPasswordComponents.ts
export { default as Subtitle } from "@components/login/Subtitle";
export { default as SubmitButton } from "@components/shared/SubmitButton";
export { default as ConfirmPassword } from "@components/shared/ConfirmPasswordInput";
export { default as PasswordInput } from "@components/shared/PasswordInput";
export { default as Loader } from "../Loader";

// Assets
export { default as ManImg } from "../../assets/icons/man.png";
export { default as Wallpaper } from "../../assets/wallpaper/LoginWallpaper.png";

// React
export { useState, useRef, useEffect } from "react";
export { useParams, useNavigate } from "react-router-dom";


//utils
export { CustomError } from "@utils/CustomError";

//types
export type { IResetPasswordResponse } from "@types"
