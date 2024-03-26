import { CustomError } from "../CustomError";

export default function validateLastName (lastName: string): boolean {
    if (lastName.length < 2) {
        throw new CustomError(
            "Last name is too short. It should be at least 2 characters long.",
            { type: "lastName" }
        );
    }
    return true;
};