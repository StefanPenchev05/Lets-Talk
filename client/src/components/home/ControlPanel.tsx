import ChatIcon from "@assets/svg/ChatIcon";
import ContactIcon from "@assets/svg/ContactIcon";
import ExitIcon from "@assets/svg/ExitIcon";
import NotificationIcon from "@assets/svg/NotificationIcon";
import SettingsIcon from "@assets/svg/SettingsIcon";

import useAppSelector from "@hooks/useAppSelector.hook";
import { useState } from "react";

const ICONS = [
  {component: ChatIcon, label: "chat"},
  {component: ContactIcon, label: "contact"},
  {component: NotificationIcon, label: "notifications"},
  {component: SettingsIcon, label: "settings"}
];

function ControlPanel() {

  const { firstName, lastName, avatarURL } = useAppSelector((state) => state.profile);
  const [activeOn, setActiveOn] = useState<string>("chat")

  return (
    <div className="container flex flex-col rounded-xl items-center justify-between min-h-[100dvh] bg-white dark:bg-base-100 w-full lg:w-60 px-8 py-14">
      <div className="flex flex-col items-center space-y-2">
        <div className="avatar">
          <div className="w-24 h-24 rounded-full">
            <img src={avatarURL as string} />
          </div>
        </div>
        <span className="text-black dark:text-white text-lg font-medium">
          {firstName} {lastName}
        </span>
      </div>
      <div className="flex flex-col justify-center items-center md:items-start space-y-8 w-full">
        {ICONS.map(({ component: Icon, label }) => (
          <div key={label} className={`flex items-center space-x-2 ${activeOn === label ? "border-l-2 pl-4 border-blue-500" : null}`} onClick={() => setActiveOn(label)}>
            <Icon isActive={activeOn === label} />
            <span className={`text-black dark:text-white ${activeOn === label ? "text-blue-500" : null} uppercase text-sm`}>
              {label}
            </span>
          </div>
        ))}
      </div>
      <div className="space-x-2 w-full mt-40 flex items-center md:items-start">
        <ExitIcon />
        <span className="text-black dark:text-white uppercase text-sm">
          log out
        </span>
      </div>
    </div>
  );
}

export default ControlPanel;
