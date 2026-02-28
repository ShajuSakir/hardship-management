import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import toast from "react-hot-toast";
import { hardshipApi } from "../api/hardshipApi";
import type { Hardship } from "../types/hardship";
import { getApiErrorMessage } from "@/shared/api/axiosInstance";
import { ROUTES } from "@/shared/config/routes";

export default function HardshipListPage() {
  const [items, setItems] = useState<Hardship[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const result = await hardshipApi.getAll();
        setItems(result);
      } catch (error) {
        toast.error(getApiErrorMessage(error));
      } finally {
        setLoading(false);
      }
    };

    fetchData();
  }, []);

  if (loading) {
    return <div className="p-10 text-center">Loading...</div>;
  }

  return (
    <div className="min-h-screen bg-gray-100 p-8">
      <div className="max-w-4xl mx-auto">
        <div className="flex justify-between mb-6">
          <h1 className="text-3xl font-bold">Hardship Applications</h1>
        </div>

        {items.length === 0 ? (
          <div className="text-center text-gray-500">
            No applications found.
          </div>
        ) : (
          <div className="space-y-4">
            {items.map((item) => (
              <div
                key={item.id}
                className="bg-white p-5 rounded-xl shadow flex justify-between items-center"
              >
                <div>
                  <p className="font-semibold text-lg">
                    {item.customerName}
                  </p>
                  <p className="text-sm text-gray-500">
                    Income: {item.income} | Expenses: {item.expenses}
                  </p>
                </div>

                <Link
                  to={ROUTES.editHardship(item.id)}
                  className="text-blue-600 hover:underline"
                >
                  Edit
                </Link>
              </div>
            ))}
          </div>
        )}
      </div>
    </div>
  );
}