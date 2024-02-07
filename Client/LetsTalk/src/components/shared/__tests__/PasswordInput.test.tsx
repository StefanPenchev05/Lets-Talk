import "@testing-library/jest-dom";
import { render, screen } from "@testing-library/react";
import PasswordInput from "../PasswordInput";

test("render EmailInput and checks helperText for password", () => {
  const mockPassword = jest.fn();
  render(<PasswordInput password="" setPassword={mockPassword}/>);
  const helperText = screen.getByText("Please enter your password")
  expect(helperText).toBeInTheDocument();
})
