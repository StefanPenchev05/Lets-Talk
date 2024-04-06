import SearchBar from "@components/chat/SearchBar";
import { useEffect, useState } from "react";
import useAppDispatch from "@hooks/useAppDispatch.hook";
import { clearUsers, fetchSearchUsers } from "@store/search/usersSlice";
import useAppSelector from "@hooks/useAppSelector.hook";
import InfiniteScroll from "react-infinite-scroll-component";

function index() {
  const [createNewChat, setCreateNewChat] = useState<boolean>(false);
  const [searchQuery, setSearchQuery] = useState<string>("");
  const [selectedUser, setSelectedUser] = useState<
    { username: string | null }[]
  >([]);
  const [pageIndex, setPageIndex] = useState<number>(0);

  const dispatch = useAppDispatch();
  const searchedUser = useAppSelector((state) => state.search);

  const onNewChat = () => {
    setCreateNewChat(true);
  };

  const onClose = () => {
    setCreateNewChat(false);
    setSelectedUser([]);
    setSearchQuery("");
  };

  const onNewChatCreate = async() => {
    
  }

  useEffect(() => {
    if (searchQuery) {
      dispatch(fetchSearchUsers({ userName: searchQuery, pageIndex }));
    } else {
      setPageIndex(0);
      dispatch(clearUsers());
    }
    console.log(searchedUser);
  }, [searchQuery, pageIndex]);

  return (
    <div className="w-full flex flex-row space-x-6 py-12 pl-12 pr-6 text-black">
      <div className="h-[100dvh] w-1/3">
        <header>
          <div className="flex justify-between items-center p-4">
            <div>
              <p className="text-2xl text-black dark:text-white font-bold mb-2">
                Chats
              </p>
              <select className="bg-transparent text-black dark:text-white">
                <option selected>Recent Chats</option>
                <option>Last 24 Hours</option>
                <option>Last 7 Days</option>
                <option>Last 30 Days</option>
                <option>Last 6 Months</option>
                <option>Last Year</option>
                <option>All Time</option>
              </select>
            </div>
            <button
              className="w-1/2 p-4 bg-blue-500 text-white font-mono rounded shadow-xl hover:bg-blue-600 active:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:ring-opacity-50 transition duration-150 ease-in-out"
              onClick={onNewChat}
            >
              + Create New Chat
            </button>
          </div>
        </header>
        <div className="flex flex-col p-3">
          <SearchBar selectOption={true} />
        </div>
      </div>
      <div className="w-2/3">
        {createNewChat ? (
          <div className="h-full shadow-md rounded-md bg-white dark:bg-base-200 p-4 flex flex-col">
            <SearchBar setSearching={setSearchQuery} />
            <div className="flex-grow overflow-auto">
              {searchedUser && (
                <InfiniteScroll
                  dataLength={searchedUser.length}
                  next={() => setPageIndex(pageIndex + 1)}
                  hasMore={true}
                  loader={null}
                >
                  <div className="flex flex-col w-full h-auto p-4 space-y-2">
                    {searchedUser.map((user, index) => (
                      <div
                        key={index}
                        className="flex items-center justify-between p-3 bg-white shadow-lg rounded-md"
                        onClick={() => {
                          if (user.isFriend) {
                            setSelectedUser([
                              ...selectedUser,
                              { username: user.username && user.username },
                            ]);
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
                            <p
                              className={
                                user.isFriend
                                  ? "text-green-500"
                                  : "text-red-500"
                              }
                            >
                              {user.isFriend ? "Friend" : "Not Friend"}
                            </p>
                          </div>
                        </div>
                        {selectedUser.find(
                          (u) => u.username === user.username
                        ) && (
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
                    ))}
                  </div>
                </InfiniteScroll>
              )}
            </div>
            <div className="flex w-full space-x-4">
              <button
                className="w-1/2 p-4 bg-blue-500 text-white font-mono rounded shadow-xl hover:bg-blue-600 active:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:ring-opacity-50 transition duration-150 ease-in-out disabled:bg-gray-400 mt-auto"
                disabled={selectedUser.length > 0 ? false : true}

              >
                Create
              </button>
              <button
                className="w-1/2 p-4 bg-blue-500 text-white font-mono rounded shadow-xl hover:bg-blue-600 active:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:ring-opacity-50 transition duration-150 ease-in-out mt-auto"
                onClick={onClose}
              >
                Cancel
              </button>
            </div>
          </div>
        ) : (
          <div className="flex items-center justify-center bg-gradient-to-r from-purple-400 via-pink-500 to-red-500 dark:from-indigo-500 dark:via-purple-700 dark:to-blue-500 w-full h-full shadow-md rounded-lg">
            <p className="text-white text-2xl font-semibold tracking-wide animate-bounce">
              <span className="text-yellow-300">Welcome!</span> Please select an
              existing channel or create a new one.
            </p>
          </div>
        )}
      </div>
    </div>
  );
}

export default index;
