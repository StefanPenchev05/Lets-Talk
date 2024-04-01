import { TypedUseSelectorHook, useSelector } from "react-redux";
import type { RootState } from "../store/app";

const useAppSelector: TypedUseSelectorHook<RootState> = useSelector;

export default useAppSelector;
