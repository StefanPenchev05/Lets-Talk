function StepBack() {
  return (
    <>
      <svg
        data-slot="icon"
        fill="none"
        strokeWidth="1.5"
        stroke="currentColor"
        className={"w-5 h-5 inline-block dark:stroke-white"}
        viewBox="0 0 24 24"
        xmlns="http://www.w3.org/2000/svg"
        aria-hidden="true"
      >
        <path
          strokeLinecap="round"
          strokeLinejoin="round"
          d="M10.5 19.5 3 12m0 0 7.5-7.5M3 12h18"
        ></path>
      </svg>
    </>
  );
}

export default StepBack;
