import { CustomError } from "../CustomError";

export const validatePassword = (password: string): boolean => {
  if (password.length < 8) {
    throw new CustomError(
      "Password is too short. It should be at least 8 characters long.",
      { type: "password" }
    );
  }
  if (!/[a-z]/.test(password)) {
    throw new CustomError(
      "Password should contain at least one lowercase letter.",
      { type: "password" }
    );
  }
  if (!/[A-Z]/.test(password)) {
    throw new CustomError(
      "Password should contain at least one uppercase letter.",
      { type: "password" }
    );
  }
  if (!/\d/.test(password)) {
    throw new CustomError("Password should contain at least one number.", {
      type: "password",
    });
  }
  if (!/[@$!%*?&]/.test(password)) {
    throw new CustomError(
      "Password should contain at least one special character.",
      { type: "password" }
    );
  }
  return true;
};
