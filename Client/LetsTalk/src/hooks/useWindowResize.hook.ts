import { useState, useEffect, useCallback } from "react";

export const useWindowResize = () => {
  const [windowWidth, setWindowWidth] = useState<number>(window.innerWidth);

  const handleWindowResize = useCallback(() => {
    setWindowWidth(window.innerWidth);
  }, []);

  useEffect(() => {
    window.addEventListener("resize", handleWindowResize);

    return () => {
      window.removeEventListener("resize", handleWindowResize);
    };
  }, [handleWindowResize]);

  return windowWidth;
};
