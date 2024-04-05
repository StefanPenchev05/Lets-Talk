import SearchBar from "@components/chat/SearchBar";
import { useEffect, useState } from "react";

function index() {
  const [createNewChat, setCreateNewChat] = useState<boolean>(false);
  const [searchQuery, setSearchQuery] = useState<string>("");


  const onNewChat = () => {
    setCreateNewChat(true);
  };

  useEffect(() => {
    console.log(searchQuery);
  }, [searchQuery])

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
          <div className="h-full shadow-md rounded-md bg-white dark:bg-base-200 p-4">
            <SearchBar setSearching={setSearchQuery} />
            <div className="flex flex-row justify-center items-center w-full h-full">
              No Users Found
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
