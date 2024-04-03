import { Icon } from "@types";

function NotificationIcon({isActive}: Icon) {
  return (
    <>
      <svg
        data-slot="icon"
        fill="none"
        strokeWidth="1.5"
        className={`w-5 h-5 inline-block dark:stroke-white ${isActive ? " stroke-blue-500" : null}`}
        stroke="black"
        viewBox="0 0 24 24"
        xmlns="http://www.w3.org/2000/svg"
        aria-hidden="true"
      >
        <path
          strokeLinecap="round"
          strokeLinejoin="round"
          d="M14.857 17.082a23.848 23.848 0 0 0 5.454-1.31A8.967 8.967 0 0 1 18 9.75V9A6 6 0 0 0 6 9v.75a8.967 8.967 0 0 1-2.312 6.022c1.733.64 3.56 1.085 5.455 1.31m5.714 0a24.255 24.255 0 0 1-5.714 0m5.714 0a3 3 0 1 1-5.714 0"
        ></path>
      </svg>
    </>
  );
}

export default NotificationIcon;
