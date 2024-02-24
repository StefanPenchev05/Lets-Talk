import { validateEmailOrUsername } from "../../login-validations/EmailOrUsername";
import { CustomError } from "../../CustomError";

describe("validateEmailOrUsername", () => {
  it.each([
    "test.test@test.test",
    "another.valid@example.com",
    "user.name+tag+sorting@example.com",
    "user@localhost.com",
  ])("should return true if email is valid", (validEmail) => {
    expect(validateEmailOrUsername(validEmail)).toBe(true);
  });

  it.each([
    "test@",
    "user.name@example.com (Joe Smith)",
    "user.name@example.com<",
    "user.name@example.com>",
    "user@192.0.2.1",
  ])(
    "should throw a CustomError with message 'Invalid email format' and details { type: 'email' } when an invalid email is provided",
    async (invalidEmail) => {
      try {
        validateEmailOrUsername(invalidEmail);
      } catch (err) {
        const error = err as CustomError;
        expect(error.message).toBe("Invalid email format");
        expect(error.details).toEqual({ type: "email" });
      }
    }
  );

  it.each([
    "user123",
    "username",
    "user_name",
    "User.Name",
    "user-name",
    "user123name",
  ])("should return true if username is valid ", (validUsername) => {
    expect(validateEmailOrUsername(validUsername)).toBeTruthy();
  });

  it("should throw a CustomError with message 'Invalid username format' and details {type: 'usernama'} when and invalid username is provided", async () => {
    const invalidUsername = "Stef32!";

    try {
      validateEmailOrUsername(invalidUsername);
    } catch (err) {
      const error = err as CustomError;
      expect(error.message).toBe("Invalid username format");
      expect(error.details).toEqual({ type: "username" });
    }
  });
});
