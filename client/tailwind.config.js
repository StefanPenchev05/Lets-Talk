/** @type {import('tailwindcss').Config} */
export default {
  content: ["./index.html", "./src/**/*.{js,ts,jsx,tsx,vue}"],
  darkMode: "class",
  theme: {
    extend: {},
  },
  daisyui: {
    themes: [
      {
        mytheme: {
          primary: "#005cff",
          secondary: "#005aff",
          accent: "#0045ff",
          neutral: "#000e0f",
          "base-100": "#292a35",
          "base-200": "#3c3d4e",
          info: "#009bff",
          success: "#14ff53",
          warning: "#f5ae00",
          error: "#f9003f",
        },
      },
    ],
  },
  plugins: [require("daisyui")],
};
