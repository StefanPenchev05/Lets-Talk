import { Comment } from 'react-loader-spinner';
import React, { useEffect, useState } from 'react';

interface LoaderProps {
  helpText?: string;
}

const Loader: React.FC<LoaderProps> = ({ helpText = 'true' }) => {
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
      <div className='w-[280px] flex items-center flex-col'>
        <Comment
          visible={true}
          height="200"
          width="200"
          ariaLabel="comment-loading"
          color="#fff"
          backgroundColor="#F4442E"
        />
        {helpText === 'true' && (
          <>
            <div className={isBlinking ? 'text-typingUpperCase text-3xl text-left font-mono text-orange-500' : 'text-3xl text-left font-mono text-orange-500'}>
              Please wait,
            </div>
            {showSecondLine && (
              <div className='text-typingDownCase text-2xl text-left font-mono text-orange-500'>
                Your time is <span className="text-purple-600 underline font-bold">Essential</span> to us!
              </div>
            )}
          </>
        )}
      </div>
  );
};

export default Loader;