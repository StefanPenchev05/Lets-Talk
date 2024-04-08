import { TextField } from "@mui/material";
import { SearchUserSlice } from "@types";
import { useState } from "react";
import InfiniteScroll from "react-infinite-scroll-component";
import Button from "@components/chat/Button";

interface CreateChannelConfigProps {
  selectedUsers: SearchUserSlice[];
  onClose: () => void;
}

function index({ selectedUsers, onClose }: CreateChannelConfigProps) {
  const [channelAvatar, setChannelAvatar] = useState<File | null>(null);
  const [channelName, setChannelName] = useState<string | null>(null);
  const [channelNameError, setChannelError] = useState<string | null>(null);

  const onNextCreateStep = () => {};

  return (
    <div className="flex flex-col justify-center items-center space-y-4">
      {channelAvatar ? (
        <div className="avatar relative">
          <div className="w-24 h-24 md:w-26 md:h-26 lg:w-40 lg:h-40 rounded-full mb-5">
            <img
              src={channelAvatar ? URL.createObjectURL(channelAvatar) : ""}
            />
            <button
              className=" absolute top-0 left-0 lg:right-2 bg-red-500 w-7 rounded-full text-center text-xl text-white"
              onClick={() => setChannelAvatar(null)}
            >
              x
            </button>
          </div>
        </div>
      ) : (
        <label
          htmlFor="file"
          className="relative w-24 h-24 md:w-26 md:h-26 lg:w-40 lg:h-40 rounded-full border-2 border-dashed cursor-pointer flex items-center justify-center"
        >
          <input
            id="file"
            className="hidden"
            placeholder="Please Upload Avatar of Channel"
            type="file"
            onChange={(e) => setChannelAvatar(e.target.files![0])}
          />
          <span className="text-gray-500 w-2/3 text-center">
            Avatar of Channel
          </span>
        </label>
      )}
      <TextField
        data-testid="Username-input"
        type="title"
        label="title"
        variant="outlined"
        color="primary"
        value={channelName}
        error={!!channelNameError}
        onChange={(e) => setChannelName(e.target.value)}
        helperText={
          channelNameError
            ? String(channelNameError)
            : "Please enter the name of the Channel"
        }
        InputProps={{
          className:
            "bg-white dark:bg-base-100 text-black dark:text-white rounded-lg w-full",
        }}
        InputLabelProps={{
          className: "text-black dark:text-white",
        }}
        sx={{
          ".dark & .MuiOutlinedInput-root": {
            "& fieldset": {
              borderColor: "white",
            },
            "&:hover fieldset": {
              borderColor: "green",
            },
            "&.Mui-focused fieldset": {
              borderColor: "yellow",
            },
          },

          ".dark & .MuiFormHelperText-root": {
            color: "white",
            "&.Mui-error": {
              color: "red",
            },
          },
        }}
      />
      <InfiniteScroll
        dataLength={selectedUsers.length}
        next={() => {}}
        hasMore={true}
        loader={<h4>Loading...</h4>}
        endMessage={
          <p style={{ textAlign: "center" }}>
            <b>Yay! You have seen it all</b>
          </p>
        }
      >
        {selectedUsers.map((user, index) => (
          <div
            className="flex flex-row justify-center items-center space-x-4 p-4 rounded-lg hover:shadow-lg transition duration-200 ease-in-out"
            key={index}
          >
            <img
              src={
                user.avatarURL
                  ? user.avatarURL
                  : "http://localhost:5295/uploads/default/User.svg"
              }
              onError={(e) => {
                const target = e.target as HTMLImageElement;
                target.onerror = null;
                target.src = "http://localhost:5295/uploads/default/User.svg";
              }}
              className="w-12 h-12 rounded-full"
              alt={`Avatar of ${user.username}`}
            />
            <span className="font-bold text-lg text-black dark:text-white">
              {user.firstName} {user.lastName}
            </span>
            <select className="ml-2 rounded-md p-1 bg-transparent text-black dark:text-white">
              <option selected>General User</option>
              <option>Admin</option>
              <option>Spectator</option>
            </select>
          </div>
        ))}
      </InfiniteScroll>
      <div className="flex w-full space-x-4">
        <Button
          placeholder="Next"
          onClick={onNextCreateStep}
          disabled={selectedUsers.length > 1 ? false : true}
        />
        <Button onClick={onClose} placeholder="Close" />
      </div>
    </div>
  );
}

export default index;
