import { CustomError } from "../CustomError";

export default function validateImageName(imageName: File | null): boolean {
    if (imageName === null) {
        throw new CustomError(
            "Image file is required.",
            { type: "imageName" }
        );
    }
    if (!(imageName instanceof File)) {
        throw new CustomError(
            "Invalid file format.",
            { type: "imageName" }
        );
    }
    return true;
};