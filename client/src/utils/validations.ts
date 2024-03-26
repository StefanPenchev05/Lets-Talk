import { validateEmailOrUsername } from "./login-validations/EmailOrUsername";
import { validatePassword } from "./login-validations/Password";
import validateFirstName from "./register-validations/FirstName";
import validateLastName from "./register-validations/LastName";
import validateImageName from "./register-validations/ImageName";
import validateEmail from "./register-validations/Email";

export const validateLogInForm = async (email: string, password: string) => {
  try {
    validateEmailOrUsername(email);
    validatePassword(password);
  } catch (err) {
    throw err;
  }
};

export const validateRegsiterForm = async(email: string, password: string, username: string, firstName: string, LastName: string, image: File | null, isTwoFactor: boolean) => {
  validateFirstName(firstName);
  validateLastName(LastName);
  validateEmail(email);
  validateImageName(image);
}
