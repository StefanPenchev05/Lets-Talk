import { TextField } from "@mui/material"

interface FirstAndLastNameProps {
    firstName: string;
    setFirstName: (firstName: string) => void;
    firstNameError?: string;
    lastName: string;
    setLastName: (lastName: string) => void;
    lastNameError?: string;
}

const FirstAndLastName : React.FC<FirstAndLastNameProps> = ({...rest}) => {
  return (
    <div className='container flex space-x-2'>
        <div className='w-1/2'>
            <TextField
                label="First Name"
                variant="outlined"
                className="w-full"
                color="primary"
                error={!!rest.firstNameError}
                value={rest.firstName}
                onChange={(e) => rest.setFirstName(e.target.value)}
                helperText={rest.firstNameError ? String(rest.firstNameError) : "Please enter your first name"}
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
        </div>
        <div className='w-1/2'>
            <TextField
                label="Last Name"
                variant="outlined"
                color="primary"
                className="w-full"
                error={!!rest.lastNameError}
                value={rest.lastName}
                onChange={(e) => rest.setLastName(e.target.value)}
                helperText={rest.lastNameError ? String(rest.lastNameError) : "Please enter your last name"}
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
        </div>
    </div>
  )
}

export default FirstAndLastName