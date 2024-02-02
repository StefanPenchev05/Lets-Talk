import axios from "axios";
const apiUrl = import.meta.env.VITE_APP_API_URL;

const axiosInstance = axios.create({
  baseURL: apiUrl,
  headers: {
    "Content-Type": "application/json",
  },
});

export function api(url: string, options: any) {
  return new Promise((resolve, reject) => {
    //Check if the user is online
    if (!navigator.onLine) {
      // If the user is offline, reject the promise immediately
      reject(new Error("No internet connection"));

      // But also wait until they're online to make the request
      window.addEventListener("online", () => {
        axiosInstance(url, options)
          .then((response) => resolve(response))
          .catch((error) => reject(error));
      });
    }

    //method:...  data: {}
    axiosInstance(url, options)
      .then((response) => resolve(response))
      .catch((error) => reject(error));
  });
}

export default axiosInstance;
