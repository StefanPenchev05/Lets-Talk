import { createSlice, createAsyncThunk, PayloadAction } from "@reduxjs/toolkit";
import { api } from "@services/api";
import { SearchUserSlice } from "@types";

export const fetchSearchUsers = createAsyncThunk(
  "search/fetchUsers",
  async (
    { userName, pageIndex }: { userName: string; pageIndex: number },
    thunkAPI
  ) => {
    try {
      const response: any = await api(
        `/search/users?userName=${userName}&pageIndex=${pageIndex}`,
        { method: "GET" }
      );
      return response.data.$values;
    } catch (error: any) {
      if(pageIndex > 0){
        return;
      }
      return thunkAPI.rejectWithValue({ error });
    }
  }
);

const initialState: SearchUserSlice[] = [];

const usersSlice = createSlice({
  name: "searchUser",
  initialState,
  reducers: {
    addUser: (state, action: PayloadAction<SearchUserSlice>) => {
      state.push(action.payload);
    },

    clearUsers: () => {
      return [];
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(
        fetchSearchUsers.fulfilled,
        (state, action: PayloadAction<SearchUserSlice[]>) => {
          if(action.payload)
            return action.payload;
        }
      )
      .addCase(
        fetchSearchUsers.rejected,
        () => {
          return [];
        }
      )
  },
});

export const { addUser, clearUsers } = usersSlice.actions;
export default usersSlice.reducer;
