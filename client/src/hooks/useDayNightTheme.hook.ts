import { useEffect } from "react";

const useDayNightTheme = (dayStart = 6, dayEnd = 10, checkIntervalMinutes = 30) => {
    useEffect(() => {
        const setTheme = () => {
            const currentHour = new Date().getHours();
            const isDayTime = currentHour >= dayStart && currentHour < dayEnd;
            document.documentElement.classList.toggle("dark", !isDayTime);
        };

        setTheme();

        const intervalId = setInterval(setTheme, checkIntervalMinutes * 60 * 1000);

        return () => {
            clearInterval(intervalId);
        };
    }, [dayStart, dayEnd, checkIntervalMinutes]);
};

export default useDayNightTheme;