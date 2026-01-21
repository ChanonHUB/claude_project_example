export interface User {
  email: string;
  fullName: string;
}

export interface AuthResponse {
  token: string;
  email: string;
  fullName: string;
  expiresAt: string;
}

export interface LoginRequest {
  email: string;
  password: string;
}

export interface RegisterRequest {
  email: string;
  password: string;
  fullName: string;
}

export interface Item {
  id: number;
  name: string;
  description: string;
  createdBy: number;
  createdByName: string;
  createdAt: string;
  updatedAt?: string;
}

export interface CreateItemRequest {
  name: string;
  description: string;
}

export interface UpdateItemRequest {
  name: string;
  description: string;
}

export interface ApiError {
  message: string;
}
