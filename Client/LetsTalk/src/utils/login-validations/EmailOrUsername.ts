import { CustomError } from "../CustomError";

const validateEmail = (email: string): boolean => {
  const emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
  if (!emailPattern.test(email)) {
    throw new CustomError("Invalid email format", { type: "email" });
  }
  return true;
};

const validateUsername = (username: string): boolean => {
  const usernamePattern = /^[a-zA-Z0-9]{3,20}$/;
  if (!usernamePattern.test(username)) {
    throw new CustomError("Invalid username format", { type: "username" });
  }
  return true;
};

// Main function
export const validateEmailOrUsername = (emailOrUsername: string): boolean => {
  if (emailOrUsername.includes("@")) {
    return validateEmail(emailOrUsername);
  } else {
    return validateUsername(emailOrUsername);
  }
};
