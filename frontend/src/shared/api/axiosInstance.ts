import axios from "axios";
import type { ProblemDetails } from "@/shared/types/problemDetails";

export const axiosInstance = axios.create({
  baseURL: import.meta.env.VITE_API_BASE_URL,
  headers: {
    "Content-Type": "application/json",
  },
});

export function getApiErrorMessage(error: unknown): string {
  if (axios.isAxiosError(error)) {
    const data = error.response?.data as ProblemDetails | undefined;

    if (data?.errors?.length) {
      return data.errors.join(", ");
    }

    if (data?.detail) {
      return data.detail;
    }

    return data?.title ?? "Unexpected API error";
  }

  return "Unexpected error occurred";
}