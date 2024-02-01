import React from 'react';

import Subtitle from './Subtitle';
import SubmitButton from '../../components/SubmitButton';
import EmailInput from '../../components/EmailInput';
import PasswordInput from '../../components/PasswordInput'
import DividerField from '../../components/DividerFiled';
import SocialButton from './SocialButton';

import ManImg from '../../assets/icons/man.png';
import Wallpaper from '../../assets/wallpaper/LoginWallpaper.png';

// import { useDispatch } from 'react-redux';
// import { fetchUserData } from '../../feature/LoginForm/loginSlice';

function Login() {

  const [resolution, setResolution] = React.useState<number>(window.innerWidth);
  //const dispatch = useDispatch();

  React.useEffect(() => {
    const handleResize = () => {
      setResolution(window.innerWidth);
    }

    window.addEventListener('resize', handleResize);

    return () => {
      window.removeEventListener('resize', handleResize);
    }
  },[])

  const handleOnSubmit = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
  }

  const renderForm = () => (
    <form className='flex flex-col space-y-4 w-full mb-9' onSubmit={handleOnSubmit}>
      <EmailInput/>
      <PasswordInput/>
      <p className='text-base w-full text-end text-[#6F58C1] dark:text-[#8B71DD] underline'>Forgot Password?</p>
      <SubmitButton helperText='Sign in'/>
    </form>
  )

  return (
    <div className='flex flex-col md:flex-row items-center justify-center h-screen w-full'>
      <div className='hidden lg:block w-3/4 h-full bg-white dark:bg-[#150f38]'>
        <img src={Wallpaper} alt='Login Wallpaper' className='w-full h-full object-cover' />
      </div>
      <div className='w-full h-screen lg:w-1/4 flex flex-col items-center justify-center max-md:px-8 md:p-8 loginBox dark:bg-[#040622]'>
        <img src={ManImg} alt='Avatar' className='w-1/3 mb-5' />
        {resolution <= 1022 ? <Subtitle align='center' gutterBottom /> : <Subtitle align='left' gutterBottom/>}
        {renderForm()}
        <DividerField helperText = "Login with social media"/>
        <SocialButton />
        <p className='text-base w-full text-center text-black dark:text-white'>
          Don't have an account?&nbsp;
          <span className='underline text-[#6F58C1] dark:text-[#8B71DD]'>Sign Up</span>
        </p>
      </div>
    </div>
  );
}

export default Login;