import { ActionCreatorWithPayload } from "@reduxjs/toolkit";
import useAppDispatch from "./useAppDispatch.hook";
import { useEffect } from "react";

/**
 * Custom hook for subscribing to Server-Sent Events (SSE) and dispatching actions with the received data.
 * 
 * @template T - The type of data received from the SSE.
 * @param {ActionCreatorWithPayload<T, string>} actionCreator - The action creator function to dispatch with the received data.
 * @param {string} url - The full URL of the SSE endpoint.
 * @param {(err: Event | ErrorEvent) => void} onError - Callback function that gets called when an error occurs.
 * @returns {void}
 */
export default function useSSE<T>(
    actionCreator: ActionCreatorWithPayload<T, string>,
    url: string,
    onError: (err: Event | ErrorEvent) => void
): void {
    const dispatch = useAppDispatch();

    useEffect(() => {
        const eventSource = new EventSource(url);

        eventSource.onmessage = (event) => {
            const data = JSON.parse(event.data);
            dispatch(actionCreator(data));
        };

        eventSource.onerror = (err) => {
            console.error(err);
            onError(err);
        };

        return () => {
            eventSource.close();
        };
    }, [url, dispatch, actionCreator, onError]);
}