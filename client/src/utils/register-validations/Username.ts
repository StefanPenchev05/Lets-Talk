import { CustomError } from "../CustomError";

export default function validateUsername(username: string): boolean {
    const regex = /^[a-zA-Z0-9_]{3,15}$/;
    if (!regex.test(username)) {
        throw new CustomError(
            "Invalid username. Username should be 3-15 characters long and can only contain alphanumeric characters and underscores.",
            { type: "username" }
        );
    }
    return true;
}