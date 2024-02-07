import { defineConfig } from "cypress";

export default defineConfig({
  component: {
    devServer: {
      framework: "react",
      bundler: "vite",
    },
  },
  chromeWebSecurity: false,
  e2e: {
    setupNodeEvents(on, config) {},
  },
});
