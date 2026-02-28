import { useParams, useNavigate } from "react-router-dom";
import { useEffect, useState } from "react";
import toast from "react-hot-toast";
import HardshipForm from "../components/HardshipForm";
import { hardshipApi } from "../api/hardshipApi";
import { getApiErrorMessage } from "@/shared/api/axiosInstance";
import type { Hardship } from "../types/hardship";
import type { HardshipFormValues } from "../schemas/hardshipSchema";

export default function EditHardshipPage() {
  const { id } = useParams();
  const navigate = useNavigate();
  const [data, setData] = useState<Hardship|null>(null);

  useEffect(() => {
    if (!id) return;
  
    const fetchData = async () => {
      try {
        const result = await hardshipApi.getById(id);
        setData(result);
      } catch (error) {
        toast.error(getApiErrorMessage(error));
        navigate("/");
      }
    };
  
    fetchData();
  }, [id, navigate]);

  const handleSubmit = async (formData: HardshipFormValues) => {
    if (!id) return;

    try {
      await hardshipApi.update(id, formData);
      toast.success("Application updated successfully");
      navigate("/");
    } catch (error) {
      toast.error(getApiErrorMessage(error));
    }
  };

  if (!data) {
    return <div className="p-10 text-center">Loading...</div>;
  }

  return (
    <HardshipForm
      title="Edit Hardship Application"
      defaultValues={data}
      onSubmit={handleSubmit}
    />
  );
}