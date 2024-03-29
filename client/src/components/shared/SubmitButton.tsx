import React, { ReactNode } from "react";
import "../index.css";

type SubmitButtonProp<ItemType extends ReactNode> = {
  helperText: ItemType;
};

const SubmitButton: React.FC<SubmitButtonProp<ReactNode>> = <ItemType extends ReactNode,>({ helperText }:SubmitButtonProp<ItemType>) => {
  return (
    <div>
      <button
        type="submit"
        className="submitButton 
            bg-[#6F58C1] dark:bg-[#5341A5] 
            border-none rounded-lg text-white w-full 
            px-5 py-2 text-center no-underline inline-block text-xl font-bold"
      >
        {helperText}
      </button>
    </div>
  );
};

export default SubmitButton;
