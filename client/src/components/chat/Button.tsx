interface ChatButtonProps {
  onClick: () => void;
  disabled?: boolean;
  placeholder: string;
}

function Button({ onClick, disabled, placeholder }: ChatButtonProps) {
  return (
    <button
      className={`w-1/2 p-4 bg-blue-500 text-white font-mono rounded shadow-xl hover:bg-blue-600 active:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:ring-opacity-50 transition duration-150 ease-in-out ${disabled ? "disabled:bg-gray-400" : null}`}
      onClick={onClick}
      disabled={disabled ? true : false}
    >
      {placeholder}
    </button>
  );
}

export default Button;
