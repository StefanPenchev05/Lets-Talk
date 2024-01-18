import React from 'react'
import { Visibility, VisibilityOff } from '@mui/icons-material'
import { TextField, InputAdornment, IconButton } from '@mui/material'

function InputField() {

    const [showPassword, setShowPassword] = React.useState<boolean>(false);

  return (
    <div className='flex flex-col space-y-6'>
         <TextField
        label='Username'
        variant='outlined'
        color='primary'
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
                    borderColor: '#040622',
                },
                '&.Mui-focused fieldset': {
                    borderColor: 'yellow',
                },
            },
        }}
      />

      <TextField
        label='Password'
        variant='outlined'
        color='primary'
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
                    borderColor: '#040622',
                },
                '&.Mui-focused fieldset': {
                    borderColor: 'yellow',
                },
            },
        }}
      />
    </div>
  )
}

export default InputField