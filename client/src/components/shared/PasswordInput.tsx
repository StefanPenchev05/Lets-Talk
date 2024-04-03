import React from "react";
import { Visibility, VisibilityOff } from "@mui/icons-material";
import { TextField, InputAdornment, IconButton } from "@mui/material";

type PasswordInputProps = {
  password: string;
  setPassword: (password: string) => void;
  error?: string;
  showPassword: boolean;
  setShowPassword: (showPassword:boolean) => void;
};

const PasswordInput: React.FC<PasswordInputProps> = ({
  password,
  setPassword,
  error,
  showPassword,
  setShowPassword
}) => {
  return (
    <TextField
      data-testid="password-input"
      label="Password"
      variant="outlined"
      color="primary"
      value={String(password)}
      onChange={(e) => setPassword(e.target.value)}
      error={!!error}
      helperText={error ? String(error) : "Please enter your password"}
      type={showPassword ? "text" : "password"}
      InputProps={{
        className:
          "bg-white dark:bg-[#040622] text-black dark:text-white rounded-lg",
        endAdornment: (
          <InputAdornment position="end">
            <IconButton onClick={() => setShowPassword(!showPassword)}>
              {showPassword ? (
                <VisibilityOff className="dark:text-white" />
              ) : (
                <Visibility className="dark:text-white" />
              )}
            </IconButton>
          </InputAdornment>
        ),
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

export default PasswordInput;
