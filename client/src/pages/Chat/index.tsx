import { useEffect, useState } from "react";
import InfiniteScroll from "react-infinite-scroll-component";

import useAppDispatch from "@hooks/useAppDispatch.hook";
import useAppSelector from "@hooks/useAppSelector.hook";

import { clearUsers, fetchSearchUsers } from "@store/search/usersSlice";

import SearchBar from "@components/chat/SearchBar";
import NewChatUser from "@components/chat/newChatUser";
import SearchFilter from "@components/chat/SearchFilter";
import Button from "@components/chat/Button";
import CreateChatConfiguration from "./CreateChannel/index";

import { SearchUserSlice } from "@types";

function index() {
  const [createNewChat, setCreateNewChat] = useState<boolean>(false);
  const [searchQuery, setSearchQuery] = useState<string>("");
  const [selectedUsers, setSelectedUsers] = useState<SearchUserSlice[]>([]);
  const [pageIndex, setPageIndex] = useState<number>(0);
  const [configurationWindow, setConfigurationWindow] =
    useState<boolean>(false);

  const dispatch = useAppDispatch();
  const searchedUser = useAppSelector((state) => state.search);

  const onNewChat = () => {
    setCreateNewChat(true);
  };

  const onClose = () => {
    setCreateNewChat(false);
    setConfigurationWindow(false);
    setSelectedUsers([]);
    setSearchQuery("");
  };

  const onNextCreateStep = () => {
    setConfigurationWindow(true);
  };

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
              <SearchFilter />
            </div>
            <Button onClick={onNewChat} placeholder="+ Create New Chat" />
          </div>
        </header>
        <div className="flex flex-col p-3">
          <SearchBar selectOption={true} />
        </div>
      </div>
      <div className="w-2/3">
        {configurationWindow ? (
          <div className="h-full shadow-md rounded-md bg-white dark:bg-base-200 p-4 flex flex-col items-center justify-center">
            <CreateChatConfiguration
              selectedUsers={selectedUsers}
              onClose={onClose}
            />
          </div>
        ) : (
          <>
            {createNewChat ? (
              <div className="h-full shadow-md rounded-md bg-white dark:bg-base-200 p-4 flex flex-col">
                <SearchBar setSearching={setSearchQuery} />
                <div className="flex-grow overflow-auto">
                  {searchedUser.length > 0 ? (
                    <InfiniteScroll
                      dataLength={searchedUser.length}
                      next={() => setPageIndex(pageIndex + 1)}
                      hasMore={true}
                      loader={null}
                    >
                      <div className="flex flex-col w-full h-auto p-4 space-y-2">
                        {searchedUser.map((user, index) => (
                          <NewChatUser
                            index={index}
                            user={user}
                            selectedUsers={selectedUsers}
                            setSelectedUser={setSelectedUsers}
                          />
                        ))}
                      </div>
                    </InfiniteScroll>
                  ) : (
                    <>
                      {selectedUsers.length > 0 ? (
                        <div className="flex flex-col w-full h-auto p-4 space-y-4">
                          {selectedUsers.map((user, index) => (
                            <NewChatUser
                              index={index}
                              user={user}
                              selectedUsers={selectedUsers}
                              setSelectedUser={setSelectedUsers}
                            />
                          ))}
                        </div>
                      ) : (
                        <div className="h-full flex items-center justify-center">
                          <p className="text-5xl font-bold text-gray-500 tracking-wide leading-loose bg-gradient-to-r from-purple-400 via-pink-500 to-red-500 text-transparent bg-clip-text">
                            No user selected
                          </p>
                        </div>
                      )}
                    </>
                  )}
                </div>
                <div className="flex w-full space-x-4">
                  <Button
                    onClick={onNextCreateStep}
                    placeholder="Next"
                    disabled={selectedUsers.length > 1 ? false : true}
                  />
                  <Button onClick={onClose} placeholder="Close" />
                </div>
              </div>
            ) : (
              <div className="flex items-center justify-center bg-gradient-to-r from-purple-400 via-pink-500 to-red-500 dark:from-indigo-500 dark:via-purple-700 dark:to-blue-500 w-full h-full shadow-md rounded-lg">
                <p className="text-white text-2xl font-semibold tracking-wide animate-bounce">
                  <span className="text-yellow-300">Welcome!</span> Please
                  select an existing channel or create a new one.
                </p>
              </div>
            )}
          </>
        )}
      </div>
    </div>
  );
}

export default index;
