import React from "react";
import { TextField } from "@mui/material";

type ConfirmPasswordInputProps = {
  confirmPassword: string;
  setConfirmPassword: (password: string) => void;
  showConfirmPassword?: boolean;
  error?: string;
};

const ConfirmPasswordInput: React.FC<ConfirmPasswordInputProps> = ({
  confirmPassword,
  setConfirmPassword,
  showConfirmPassword,
  error,
}) => {

  return (
    <TextField
      data-testid="confirm-password-input"
      label="Confirm Password"
      variant="outlined"
      color="primary"
      value={String(confirmPassword)}
      onChange={(e) => setConfirmPassword(e.target.value)}
      error={!!error}
      helperText={error ? String(error) : "Please confirm your password"}
      type={showConfirmPassword ? "text" : "password"}
      InputProps={{
        className:
          "bg-white dark:bg-[#040622] text-black dark:text-white rounded-lg",
      }}
      InputLabelProps={{
        className: "dark:text-white",
      }}
      sx={{
        ".dark & .MuiOutlinedInput-root": {
          "& fieldset": {
            borderColor: "white",
          },
          "&:hover fieldset": {
            borderColor: "green",
          },
          "&.Mui-focused fieldset": {
            borderColor: "yellow",
          },
        },
        ".dark & .MuiFormHelperText-root": {
          color: "white",
          "&.Mui-error": {
            color: "red",
          },
        },
      }}
    />
  );
};

export default ConfirmPasswordInput;
