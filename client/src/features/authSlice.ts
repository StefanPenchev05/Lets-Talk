import { createSlice, createAsyncThunk, PayloadAction } from "@reduxjs/toolkit";
import { api } from "@services/api";
import { IAuthResponse } from "@types";

interface AuthState {
  isAuth: boolean;
  isAwaitTwoFactor: boolean;
  isAwaitEmailVerifiaction: boolean;
  roomId: string | null;
  isLoading: boolean;
  error: string | null;
}

const initialState: AuthState = {
  isAuth: false,
  isAwaitTwoFactor: false,
  isAwaitEmailVerifiaction: false,
  roomId: null,
  isLoading: true,
  error: null,
};

export const checkAuth = createAsyncThunk("auth/check", async (_, thunkAPI) => {
  try {
    const response: any = await api("/auth/", { method: "GET" });
    const data = response.data as IAuthResponse;
    return data;
  } catch (err) {
    return thunkAPI.rejectWithValue({ err });
  }
});

const authSlice = createSlice({
  name: "auth",
  initialState,
  reducers: {
    setIsAuth: (state, action: PayloadAction<boolean>) => {
      state.isAuth = action.payload;
    },
    setRoomId: (state, action: PayloadAction<string>) => {
      state.roomId = action.payload;
    }
  },
  extraReducers: (builder) => {
    builder
      .addCase(checkAuth.pending, (state) => {
        state.isLoading = true;
        state.error = null;
      })
      .addCase(checkAuth.fulfilled, (state, action) => {
        state.isLoading = false;
        if (action.payload.awaitTwoFactorAuth) {
          state.isAwaitTwoFactor = true;
        } else if (action.payload.awaitForEmailVerification) {
          state.isAwaitEmailVerifiaction = true;
          state.roomId = action.payload.roomId;
        } else {
          state.isAuth = true;
        }
      })
      .addCase(checkAuth.rejected, () => {
        return { ...initialState, isLoading: false };
      });
  },
});

export const { setIsAuth, setRoomId } = authSlice.actions;
export default authSlice.reducer;