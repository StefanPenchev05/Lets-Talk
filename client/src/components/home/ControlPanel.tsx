import ChatIcon from "@assets/svg/ChatIcon";
import ContactIcon from "@assets/svg/ContactIcon";
import ExitIcon from "@assets/svg/ExitIcon";
import NotificationIcon from "@assets/svg/NotificationIcon";
import SettingsIcon from "@assets/svg/SettingsIcon";

const ICONS = [
  {component: ContactIcon, label: "contact"},
  {component: ChatIcon, label: "chat"},
  {component: NotificationIcon, label: "notifications"},
  {component: SettingsIcon, label: "settings"}
];

function ControlPanel() {
  return (
    <div className="container flex flex-col rounded-xl items-center justify-between min-h-[100dvh] bg-white dark:bg-base-100 w-full lg:w-60 px-8 py-14 pb-32">
      <div className="flex flex-col items-center space-y-2">
        <div className="avatar">
          <div className="w-24 h-24 rounded-full">
            <img src="https://daisyui.com/images/stock/photo-1534528741775-53994a69daeb.jpg" />
          </div>
        </div>
        <span className="text-black dark:text-white text-lg font-medium">
          Stefan Penchev
        </span>
      </div>
      <div className="flex flex-col justify-center items-center md:items-start space-y-8 w-full">
        {ICONS.map(({ component: Icon, label }) => (
          <div key={label} className="space-x-2">
            <Icon />
            <span className="text-black dark:text-white uppercase text-sm">
              {label}
            </span>
          </div>
        ))}
      </div>
      <div className="space-x-2 w-full m-40 flex items-center md:items-start">
        <ExitIcon />
        <span className="text-black dark:text-white uppercase text-sm">
          log out
        </span>
      </div>
    </div>
  );
}

export default ControlPanel;
