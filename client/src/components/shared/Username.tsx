import { TextField } from "@mui/material";

interface UsernameProps {
  Username: string;
  setUsername: (Username: string) => void;
  error?: string;
}

const Username: React.FC<UsernameProps> = ({ Username, setUsername, error }) => {
  return (
    <TextField
      data-testid="Username-input"
      type="username"
      label="Username"
      variant="outlined"
      color="primary"
      value={Username}
      error={!!error}
      onChange={(e) => setUsername(e.target.value)}
      helperText={error ? String(error) : "Please enter your username"}
      InputProps={{
        className:
          "bg-white dark:bg-[#040622] text-black dark:text-white rounded-lg",
      }}
      InputLabelProps={{
        className: "text-black dark:text-white",
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

export default Username;
