import { CustomError } from "@utils/CustomError";

export default function validateEmail(email: string) {
    const emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (!emailPattern.test(email)) {
        throw new CustomError("Invalid email format", { type: "email" });
    }
    return true;
}