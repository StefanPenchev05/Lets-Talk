import React from 'react'
//import { useDispatch } from 'react-redux';
import { Visibility, VisibilityOff } from '@mui/icons-material'
import { TextField, InputAdornment, IconButton } from '@mui/material'

function PasswordInput() {
    const [showPassword, setShowPassword] = React.useState<boolean>(false);
    //const dispatch = useDispatch();

    return (
        <TextField
            label='Password'
            variant='outlined'
            color='primary'
           //onChange={(e) => dispatch(updatePassword(e.target.value))}
            type={showPassword ? 'text' : 'password'}
            InputProps={{
            className: 'bg-white dark:bg-[#040622] text-black dark:text-white rounded-lg',
            endAdornment: (
                <InputAdornment position='end'>
                    <IconButton
                        onClick={() => setShowPassword(!showPassword)}
                    >
                        {showPassword ? 
                            <VisibilityOff 
                                className='dark:text-white'
                            /> :
                            <Visibility 
                                className='dark:text-white'
                            />
                        }
                    </IconButton>
                </InputAdornment>
            )
            }}
            InputLabelProps={{
            className: 'dark:text-white'
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

export default PasswordInput