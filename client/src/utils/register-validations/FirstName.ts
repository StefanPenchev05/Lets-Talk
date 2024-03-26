import { CustomError } from "../CustomError";

export default function validateFirstName (firstName: string): boolean {
    if (firstName.length < 2) {
        throw new CustomError(
            "First name is too short. It should be at least 2 characters long.",
            { type: "firstName" }
        );
    }
    return true;
};