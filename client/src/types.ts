export interface LoginResponse {
  twoFactorAwait: boolean;
  existingUser: boolean;
  incorrectPassword: boolean;
  message: string;
}

export interface RegisterResponse{
  emailExists: boolean;
  usernameExists: boolean;
  awaitForEmailVerification: boolean;
  roomId: string;
  message: string;
}

export interface AuthResponse{
  authSession:boolean;
  awaitTwoFactorAuth:boolean;
  awaitForEmailVerification: boolean;
  roomId: string;
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

export interface ProfileSlice{
  username: string | null;
  firstName: string | null;
  lastName: string | null;
  avatarURL: string | null;
  isLoading: boolean;
}