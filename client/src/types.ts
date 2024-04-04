/**
 * Interface for the response returned by the login endpoint.
 */
export interface ILoginResponse {
    /**
     * Indicates whether two-factor authentication is awaiting.
     */
    twoFactorAwait?: boolean;

    /**
     * Indicates whether the user exists.
     */
    existingUser?: boolean;

    /**
     * Indicates whether the provided password is incorrect.
     */
    incorrectPassword?: boolean;

    /**
     * A message describing the result of the operation.
     */
    message: string;
}

/**
 * Interface for the response returned by the register endpoint.
 */
export interface IRegisterResponse {
    /**
     * Indicates whether the provided email already exists.
     */
    emailExists?: boolean;

    /**
     * Indicates whether the provided username already exists.
     */
    usernameExists?: boolean;

    /**
     * Indicates whether the user is awaiting email verification.
     */
    awaitForEmailVerification?: boolean;

    /**
     * The ID of the room.
     */
    roomId?: string;

    /**
     * A message describing the result of the operation.
     */
    message: string;
}

/**
 * Represents the response object returned by the authentication API.
 */
export interface IAuthResponse {
  /**
   * Indicates whether the user has an active session.
   */
  authSession: boolean;

  /**
   * Indicates whether the user needs to complete two-factor authentication.
   */
  awaitTwoFactorAuth: boolean;

  /**
   * Indicates whether the user needs to verify their email address.
   */
  awaitForEmailVerification: boolean;

  /**
   * The ID of the room associated with the user.
   */
  roomId: string;

  /**
   * A message associated with the response.
   */
  message: string;
}

/**
 * Interface for the response returned by the reset password endpoint.
 */
export interface IResetPasswordResponse {
  /**
   * Indicates whether the provided token is invalid.
   */
  invalidToken?: boolean;

  /**
   * Indicates whether the provided password is empty.
   */
  emptyPassword?: boolean;

  /**
   * Indicates whether the new password is the same as the current password.
   */
  samePassword?: boolean;

  /**
   * Indicates whether the password was successfully changed.
   */
  passwordChanged?: boolean;

  /**
   * A message describing the result of the operation.
   */
  message: string;
}

export interface VerifiedEmailSignalRResponse{
  verifiedEmail: boolean;
  encryptUserId: Uint8Array,
  message: string;
}

export interface Alert{
  message: string;
  type: 'success' | 'error' | 'warning';
}

export interface Icon{
  isActive: boolean;
}

export interface ProfileSlice{
  username: string | null;
  firstName: string | null;
  lastName: string | null;
  avatarURL: string | null;
  isLoading: boolean;
}
