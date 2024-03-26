import { CustomError } from "../CustomError";

export default function validateImageName(imageName: File | null): boolean {
    if (!(imageName instanceof File)) {
        throw new CustomError(
            "Invalid file format.",
            { type: "imageName" }
        );
    }
    const sizeInMB = imageName.size / (1024 * 1024);
    if(sizeInMB > 6){
        throw new CustomError(
            "Image file size should not be more than 6MB.",
            { type: "imageName" }
        )
    }
    return true;
};