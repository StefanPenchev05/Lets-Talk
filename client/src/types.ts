export interface LoginResponse {
  errors: {
    filed: string;
    message: string;
  };
  twoFactorAwait: boolean;
  existingUser: boolean;
  incorrectPassword: boolean;
  message: string;
}

export interface AuthSessionResponse{
  errors: {
    authSession: boolean;
    message: string;
  };
  authSession:boolean;
  message: string;
}

export interface ProtectedPage{
  children: React.ReactNode;
}