import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";
import { api } from "@services/api";
import { ProfileSlice } from "@types";

const initialState: ProfileSlice = {
  username: null,
  firstName: null,
  lastName: null,
  avatarURL: null,
  isLoading: true,
};

export const fetchProfileUser  = createAsyncThunk(
  "profile/fetchProfileUser",
  async (_, thunkAPI) => {
    try {
        console.log("working")
      const response: any = await api("/user/profile", { method: "GET" });
      const data = response.data as ProfileSlice;
      console.log(response);
      return data;
    } catch (err) {
        console.log(err);
      thunkAPI.rejectWithValue({ err });
    }
  }
);

const profileSlice = createSlice({
  name: "profile",
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(fetchProfileUser.pending, (state) => {
        state.isLoading = true;
      })
      .addCase(fetchProfileUser.fulfilled, (state, action) => {
        Object.assign(state, action.payload);
        state.isLoading = false;
      })
      .addCase(fetchProfileUser.rejected, () => {
        return { ...initialState, isLoading: false };
      });
  },
});

export default profileSlice.reducer;
