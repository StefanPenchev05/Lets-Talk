import "@testing-library/jest-dom";
import { render, screen, fireEvent } from "@testing-library/react";
import EmailInput from "../EmailInput";

test("renders EmailImput and checks helperText for email", () => {
  const mockEmail = jest.fn();
  render(<EmailInput email="" setEmail={mockEmail} />);
  const helperText = screen.getByText("Please enter your username or email");
  expect(helperText).toBeInTheDocument();
});

test('updates value when typed into', () => {
    const setEmail = jest.fn();
    const { getByLabelText } = render(<EmailInput email="" setEmail={setEmail} />);
  
    fireEvent.change(getByLabelText('Username'), { target: { value: 'test@example.com' } });
  
    expect(setEmail).toHaveBeenCalledWith('test@example.com');
  });
