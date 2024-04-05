import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { SearchUserSlice } from "@types";

const initialState: SearchUserSlice[] = [
    {
        firstName: null,
        lastName: null,
        username: null,
        avatarURL: null,
        isFriend: false,
        isLoading: false
    }
];

const usersSlice = createSlice({
    name: "searchUser",
    initialState,
    reducers: {
        addUser: (state, action: PayloadAction<SearchUserSlice>) => {
            state.push(action.payload);
        }
    },
});

export const {addUser} = usersSlice.actions;
export default usersSlice.reducer;