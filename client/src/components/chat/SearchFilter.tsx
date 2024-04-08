function SearchFilter() {
  return (
    <select className="bg-transparent text-black dark:text-white">
      <option selected>Recent Chats</option>
      <option>Last 24 Hours</option>
      <option>Last 7 Days</option>
      <option>Last 30 Days</option>
      <option>Last 6 Months</option>
      <option>Last Year</option>
      <option>All Time</option>
    </select>
  );
}

export default SearchFilter;
