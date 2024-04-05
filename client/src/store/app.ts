import { configureStore } from "@reduxjs/toolkit";
import authSlice from "src/features/authSlice";
import profileSlice from "@store/user/profileSlice";
import usersSlice from "@store/search/usersSlice";

const store = configureStore({
  reducer: {
    auth: authSlice,
    profile: profileSlice,
    search: usersSlice
  },
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;

export default store;
