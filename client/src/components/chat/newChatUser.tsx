import { SearchUserSlice } from "@types";

interface UserPros {
  index: number;
  user: SearchUserSlice;
  selectedUsers: SearchUserSlice[];
  setSelectedUser: (selectedUser: SearchUserSlice[]) => void;
}

export default function NewChatUser({
  index,
  user,
  selectedUsers,
  setSelectedUser,
}: UserPros) {
  return (
    <>
      <div
        className="flex items-center justify-between p-3 bg-white shadow-lg rounded-md hover:bg-gray-100 active:bg-gray-200"
        key={index}
        onClick={() => {
          if (user.isFriend) {
            const isUserSelected = selectedUsers.find(u => u.username === user.username);
            if(!isUserSelected){
              setSelectedUser([
                ... selectedUsers,
                user,
              ]);
            }else {
              setSelectedUser(selectedUsers.filter(u => u.username !== user.username))
            }
          }
        }}
      >
        <div className="flex items-center space-x-4">
          <img
            src={
              user.avatarURL
                ? user.avatarURL
                : "http://localhost:5295/uploads/default/User.svg"
            }
            alt={`${user.firstName} ${user.lastName}`}
            className="w-12 h-12 object-cover rounded-full"
          />
          <div>
            <p
              className={`text-base font-bold ${
                user.isFriend ? null : "text-gray-200"
              }`}
            >
              {user.firstName} {user.lastName}
            </p>
            <p className={user.isFriend ? "text-green-500" : "text-red-500"}>
              {user.isFriend ? "Friend" : "Not Friend"}
            </p>
          </div>
        </div>
        {selectedUsers.find((u) => u.username === user.username) && (
          <svg
            xmlns="http://www.w3.org/2000/svg"
            fill="none"
            viewBox="0 0 24 24"
            stroke="currentColor"
            className="h-6 w-6 text-green-500"
          >
            <path
              strokeLinecap="round"
              strokeLinejoin="round"
              strokeWidth={2}
              d="M5 13l4 4L19 7"
            />
          </svg>
        )}
      </div>
    </>
  );
}
