import { validateEmailOrUsername } from "./login-validations/EmailOrUsername";
import { validatePassword } from "./login-validations/Password";
import validateFirstName from "./register-validations/FirstName";
import validateLastName from "./register-validations/LastName";
import validateImageName from "./register-validations/ImageName";
import validateEmail from "./register-validations/Email";
import validateUsername from "./register-validations/Username";

export const validateLogInForm = async (email: string, password: string) => {
  try {
    validateEmailOrUsername(email);
    validatePassword(password);
  } catch (err) {
    throw err;
  }
};

export const validateRegsiterForm = async (
  email: string,
  password: string,
  username: string,
  firstName: string,
  LastName: string,
  image: File | null
) => {
  try {
    validateEmail(email);
    validateFirstName(firstName);
    validateLastName(LastName);
    validatePassword(password);
    validateUsername(username);
    validateImageName(image);
  } catch (err) {
    throw err;
  }
};
