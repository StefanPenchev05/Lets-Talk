import React from 'react'
import { Divider } from '@mui/material'

function DividerField() {
  return (
    <Divider 
        className='w-full text-gray-400 dark:text-white mb-4'
        sx={{
          '.dark &:before': {
            backgroundColor: 'white',
            height: '1px !important'
          },
          '.dark &:after': {
            backgroundColor: 'white',
            height: '1px !important'
          }
        }}
      >
        Login with social media
      </Divider>
  )
}

export default DividerField