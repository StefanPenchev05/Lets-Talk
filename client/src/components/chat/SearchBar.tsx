interface SearchBarProps {
  selectOption?: boolean;
  setSearching?: (query: string ) => void
}

function SearchBar({ selectOption = false, setSearching }: SearchBarProps) {
  return (
    <div className="w-full join">
      <input
        className="input input-bordered w-full join-item bg-white dark:bg-base-200 dark:text-white shadow-md hover:outline-none active:outline-none"
        placeholder="Search"
        onChange={(e) => setSearching && setSearching(e.target.value) }
      />
      {selectOption && (
        <select className="select select-bordered join-item w-full dark:text-white bg-white dark:bg-base-200">
          <option selected>Messsages</option>
          <option>Channels</option>
          <option>Files</option>
        </select>
      )}
      <div className="indicator">
        <button className="btn dark:shadow-md join-item">Search</button>
      </div>
    </div>
  );
}

export default SearchBar;
