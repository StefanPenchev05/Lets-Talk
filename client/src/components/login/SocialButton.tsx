import React from 'react'
import { IconButton } from '@mui/material'
import { Google, FacebookOutlined, GitHub } from '@mui/icons-material'

const SocialButton: React.FC = ()  => {
  return (
    <div>
        <div className='flex flex-row space-x-4 justify-center w-full mb-4'>
            <IconButton>
                <Google color='error' className='text-4xl'/>
            </IconButton>
            <IconButton>
                <FacebookOutlined color='primary' className='text-4xl'/>
            </IconButton>
            <IconButton>
              <GitHub className='text-4xl text-black dark:text-white'/>
            </IconButton>
        </div>
    </div>
  )
}

export default SocialButton