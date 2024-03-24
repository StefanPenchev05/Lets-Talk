export interface LoginResponse {
  twoFactorAwait: boolean;
  existingUser: boolean;
  incorrectPassword: boolean;
  message: string;
}

export interface AuthResponse{
  authSession:boolean;
  awaitTwoFactorAuth:boolean;
  message: string;
}

export interface Alert{
  message: string;
  type: 'success' | 'error' | 'warning';
}

export interface ProtectedPage{
  isAuth: boolean;
  isAwaitTwoFactor: boolean;
}