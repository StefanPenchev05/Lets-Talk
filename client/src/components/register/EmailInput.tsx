import { TextField } from "@mui/material";

interface EmailInputProps {
    email: string;
    setEmail: (email: string) => void;
    error?: string;
}

const EmailInput: React.FC<EmailInputProps> = ({ email, setEmail, error }) => {
    return (
        <TextField
            data-testid="email-input"
            type="email"
            label="Email"
            variant="outlined"
            color="primary"
            value={email}
            error={!!error}
            onChange={(e) => setEmail(e.target.value)}
            helperText={error ? String(error) : "Please enter your email"}
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

export default EmailInput;