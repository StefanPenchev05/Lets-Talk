import "@testing-library/jest-dom";
import { fireEvent, render, screen } from "@testing-library/react";
import PasswordInput from "../PasswordInput";

const setPassword = jest.fn();

beforeEach(() => {
  setPassword.mockClear();
});

test("render EmailInput and checks helperText for password", () => {
  render(<PasswordInput password="" setPassword={setPassword} />);
  const helperText = screen.getByText("Please enter your password");
  expect(helperText).toBeInTheDocument();
});

test("updates value when typed into", () => {
  const { getByLabelText } = render(
    <PasswordInput password="" setPassword={setPassword} />
  );
  fireEvent.change(getByLabelText("Password"), {
    target: { value: "Pass1234!" },
  });

  expect(setPassword).toHaveBeenCalledWith("Pass1234!")
});
