import { TextField } from '@mui/material'


function EmailInput() {

    return (
        <TextField
            label='Username'
            variant='outlined'
            color='primary'
            helperText='Please enter your username or email'
            InputProps={{
                className: 'bg-white dark:bg-[#040622] text-black dark:text-white rounded-lg'
            }}
            InputLabelProps={{
            className: 'text-black dark:text-white'
            }}
            sx={{
                '.dark & .MuiOutlinedInput-root': {
                    '& fieldset': {
                        borderColor: 'white',
                    },
                    '&:hover fieldset': {
                        borderColor: 'green',
                    },
                    '&.Mui-focused fieldset': {
                        borderColor: 'yellow',
                    },
                },
            }}
        />
    )
}

export default EmailInput