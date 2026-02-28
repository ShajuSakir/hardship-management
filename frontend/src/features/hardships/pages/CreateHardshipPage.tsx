import { useNavigate } from "react-router-dom";
import toast from "react-hot-toast";
import HardshipForm from "../components/HardshipForm";
import { hardshipApi } from "../api/hardshipApi";
import { getApiErrorMessage } from "@/shared/api/axiosInstance";
import type { HardshipFormValues } from "../schemas/hardshipSchema";
import { ROUTES } from "@/shared/config/routes";

export default function CreateHardshipPage() {
  const navigate = useNavigate();

  const handleSubmit = async (data: HardshipFormValues) => {
    try {
      await hardshipApi.create(data);
      toast.success("Application created successfully");
      navigate(ROUTES.home);
    } catch (error) {
      toast.error(getApiErrorMessage(error));
    }
  };

  return (
    <HardshipForm
      title="Create Hardship Application"
      onSubmit={handleSubmit}
    />
  );
}