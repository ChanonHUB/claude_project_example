import apiClient from './apiClient';
import { Item, CreateItemRequest, UpdateItemRequest } from '../types';

export const itemApi = {
  getAll: async (): Promise<Item[]> => {
    const response = await apiClient.get<Item[]>('/items');
    return response.data;
  },

  getById: async (id: number): Promise<Item> => {
    const response = await apiClient.get<Item>(`/items/${id}`);
    return response.data;
  },

  create: async (data: CreateItemRequest): Promise<Item> => {
    const response = await apiClient.post<Item>('/items', data);
    return response.data;
  },

  update: async (id: number, data: UpdateItemRequest): Promise<Item> => {
    const response = await apiClient.put<Item>(`/items/${id}`, data);
    return response.data;
  },

  delete: async (id: number): Promise<void> => {
    await apiClient.delete(`/items/${id}`);
  },
};
