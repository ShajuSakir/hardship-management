import { axiosInstance } from "@/shared/api/axiosInstance";
import type { Hardship } from "../types/hardship";

// API abstraction layer for Hardship-related requests.
// Keeps HTTP logic separate from UI components.

export const hardshipApi = {
  async getAll(): Promise<Hardship[]> {
    const response = await axiosInstance.get<Hardship[]>("/hardships");
    return response.data;
  },

  async getById(id: string): Promise<Hardship> {
    const response = await axiosInstance.get<Hardship>(`/hardships/${id}`);
    return response.data;
  },

  async create(data: Omit<Hardship, "id">): Promise<void> {
    const response = await axiosInstance.post("/hardships", data);
    return response.data;
  },

  async update(id: string, data: Omit<Hardship, "id">): Promise<void> {
    const response = await axiosInstance.put(`/hardships/${id}`, data);
    return response.data;
  },
};