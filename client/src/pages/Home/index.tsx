import Loader from "../Loader";
import * as HomeImports from "./imports";
import { useEffect } from "react";

function index() {

    const dispatch = HomeImports.useAppDispatch();
    const { isLoading } = HomeImports.useAppSelector(state => state.profile);
    
    useEffect(() => {
        dispatch(HomeImports.fetchProfileUser());
    },[])

    if(isLoading){
        return <Loader/>
    }

  return (
    <div className="flex flex-col h-screen md:h-[100dvh] w-full bg-gray-300 dark:bg-neutral">
      <HomeImports.ControlPanel/>
    </div>
  );
}

export default index;
