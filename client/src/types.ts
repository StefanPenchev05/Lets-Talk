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
