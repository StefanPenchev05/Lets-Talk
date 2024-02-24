import { validateEmailOrUsername } from "./login-validations/EmailOrUsername";
import { validatePassword } from "./login-validations/Password";

export const validateLogInForm = async (email: string, password: string) => {
  try {
    validateEmailOrUsername(email);
    validatePassword(password);
  } catch (err) {
    throw err;
  }
};
