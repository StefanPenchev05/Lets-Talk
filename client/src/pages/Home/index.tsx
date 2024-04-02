import React from "react";
import * as HomeImports from "./imports";

function index() {
  return (
    <div className="flex flex-col h-screen md:h-[100dvh] w-full bg-gray-300 dark:bg-neutral">
      <HomeImports.ControlPanel/>
    </div>
  );
}

export default index;
