import "@testing-library/jest-dom";
import { render, screen, fireEvent } from "@testing-library/react";
import EmailInput from "../EmailInput";

const setEmail = jest.fn();

beforeEach(() => {
  setEmail.mockClear();
});

test("renders EmailImput and checks helperText for email", () => {
  render(<EmailInput email="" setEmail={setEmail} />);
  const helperText = screen.getByText("Please enter your username or email");
  expect(helperText).toBeInTheDocument();
});

test("updates value when typed into", () => {
  const { getByLabelText } = render(
    <EmailInput email="" setEmail={setEmail} />
  );

  fireEvent.change(getByLabelText("Email or Username"), {
    target: { value: "test@example.com" },
  });

  expect(setEmail).toHaveBeenCalledWith("test@example.com");
});
