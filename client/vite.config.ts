import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [react()],
  server: {
    cors: {
      origin: '*', // Allow all origins
      credentials: false, // Allow cookies to be included in the requests
    },
    host: "localhost",
  },
});
