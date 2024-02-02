export class CustomError extends Error {
  public details: object;

  constructor(message: string, details: object) {
    super(message);
    this.details = details;

    Object.setPrototypeOf(this, new.target.prototype);
  }
}
