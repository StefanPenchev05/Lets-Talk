import { useDispatch } from "react-redux";
import type { AppDispatch } from "../store/app";

const useAppDispatch: () => AppDispatch = useDispatch;
export default useAppDispatch;
