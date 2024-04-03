function ExitIcon() {
  return (
    <>
      <svg
        data-slot="icon"
        fill="none"
        strokeWidth="1.5"
        className="w-5 h-5 inline-block dark:stroke-white"
        stroke="black"
        viewBox="0 0 24 24"
        xmlns="http://www.w3.org/2000/svg"
        aria-hidden="true"
      >
        <path
          strokeLinecap="round"
          strokeLinejoin="round"
          d="M15.75 9V5.25A2.25 2.25 0 0 0 13.5 3h-6a2.25 2.25 0 0 0-2.25 2.25v13.5A2.25 2.25 0 0 0 7.5 21h6a2.25 2.25 0 0 0 2.25-2.25V15M12 9l-3 3m0 0 3 3m-3-3h12.75"
        ></path>
      </svg>
    </>
  );
}

export default ExitIcon;