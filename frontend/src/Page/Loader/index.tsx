import { Comment } from 'react-loader-spinner';
import React, { useEffect, useState } from 'react';
import './Loader.css'

interface LoaderProps {
  helpText?: boolean;
}

const sameStyle = "text-3xl max-sm:text-2xl font-mono font-bold text-black dark:text-orange-500";

const Loader: React.FC<LoaderProps> = ({ helpText = true }) => {
  const [showSecondLine, setShowSecondLine] = useState(false);
  const [isBlinking, setIsBlinking] = useState(true);

  useEffect(() => {
    const timer = setTimeout(() => {
      setShowSecondLine(true);
      setIsBlinking(false);
    }, 1500);

    return () => {
      clearTimeout(timer);
    };
  }, []);

  return (
      <div className='flex items-center justify-center h-screen'>
        <div className='w-[280px] max-sm:w-[190px] flex items-center flex-col'>
          <Comment
            visible={true}
            height="200"
            width="200"
            ariaLabel="comment-loading"
            color="#fff"
            backgroundColor="#F4442E"
          />
          {helpText && (
            <>
              <div className={isBlinking ? 'text-typingUpperCase ' + sameStyle : sameStyle}>
                Please wait,
              </div>
              {showSecondLine && (
                <div className='text-typingDownCase text-2xl max-sm:text-lg font-mono font-semibold text-black dark:text-orange-500'>
                  Your time is <span className="text-orange-500 dark:text-purple-600 underline font-extrabold">Essential</span> to us!
                </div>
              )}
            </>
          )}
        </div>
      </div>
  );
};

export default Loader;