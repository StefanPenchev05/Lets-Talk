function ContactIcon() {
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
          d="M15.75 6a3.75 3.75 0 1 1-7.5 0 3.75 3.75 0 0 1 7.5 0ZM4.501 20.118a7.5 7.5 0 0 1 14.998 0A17.933 17.933 0 0 1 12 21.75c-2.676 0-5.216-.584-7.499-1.632Z"
        ></path>
      </svg>
    </>
  );
}

export default ContactIcon;
