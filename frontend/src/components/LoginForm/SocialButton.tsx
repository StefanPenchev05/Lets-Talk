import React from 'react'
import { IconButton } from '@mui/material'
import { Google, FacebookOutlined } from '@mui/icons-material'

function SocialButton() {
  return (
    <div>
        <div className='flex flex-row space-x-4 justify-center w-full'>
            <IconButton>
                <Google color='error' className='text-4xl'/>
            </IconButton>
            <IconButton>
                <FacebookOutlined color='primary' className='text-4xl'/>
            </IconButton>
            {/* <button className='bg-[#3B5998] dark:bg-[#3B5998] rounded-lg w-full px-5 py-2 text-center no-underline inline-block text-xl font-bold text-white'>Facebook</button>
            <button className='bg-[#DB4437] dark:bg-[#DB4437] rounded-lg w-full px-5 py-2 text-center no-underline inline-block text-xl font-bold text-white'>Google</button> */}
        </div>
    </div>
  )
}

export default SocialButton