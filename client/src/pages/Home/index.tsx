import Loader from "../Loader";
import * as HomeImports from "./imports";
import Chat from "../Chat/index"

function index() {

    const dispatch = HomeImports.useAppDispatch();
    const { isLoading } = HomeImports.useAppSelector(state => state.profile);

    const [label, setLabel] = HomeImports.useState<string>("chat");
    
    HomeImports.useEffect(() => {
        dispatch(HomeImports.fetchProfileUser());
    },[])

    const options: { [key: string]: JSX.Element } = {
      chat: <Chat />,
    };

    if (isLoading) {
      return <Loader />;
    }

    return (
      <div className="flex flex-row h-screen md:h-[100dvh] w-full bg-gray-200 dark:bg-base-100">
        <HomeImports.ControlPanel />
        {options[label]}
      </div>
    );
}

export default index;
