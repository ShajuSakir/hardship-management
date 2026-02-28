import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { hardshipSchema } from "../schemas/hardshipSchema";
import type { HardshipFormValues } from "../schemas/hardshipSchema";
import { useEffect } from "react";

// Reusable form component for Create & Edit pages.
// Uses react-hook-form + Zod validation.

interface Props {
  defaultValues?: HardshipFormValues;
  onSubmit: (data: HardshipFormValues) => Promise<void>;
  title: string;
}

export default function HardshipForm({
  defaultValues,
  onSubmit,
  title,
}: Props) {
  const {
    register,
    handleSubmit,
    formState: { errors, isSubmitting },
    reset,
  } = useForm({
    resolver: zodResolver(hardshipSchema),
  });

  //Rehydrate form when editing existing record.
  useEffect(() => {
    if (defaultValues) {
      reset({
        ...defaultValues,
        dateOfBirth: defaultValues.dateOfBirth
          ? defaultValues.dateOfBirth.split("T")[0]
          : "",
      });
    }
  }, [defaultValues, reset]);

  return (
    <div className="flex justify-center">
      <form
        onSubmit={handleSubmit(onSubmit)}
        className="bg-white shadow-xl rounded-2xl p-8 w-full max-w-lg space-y-4"
      >
        <h2 className="text-2xl font-bold text-gray-800">{title}</h2>

        <div>
          <input
            {...register("customerName")}
            placeholder="Customer Name"
            className="w-full border p-2 rounded-lg"
          />
          <p className="text-sm text-red-500">{errors.customerName?.message}</p>
        </div>

        <div>
          <input
            type="date"
            aria-label="date"
            {...register("dateOfBirth")}
            className="w-full border p-2 rounded-lg"
          />
          <p className="text-sm text-red-500">{errors.dateOfBirth?.message}</p>
        </div>

        <div>
          <input
            type="number"
            {...register("income")}
            placeholder="Income"
            className="w-full border p-2 rounded-lg"
          />
          <p className="text-sm text-red-500">{errors.income?.message}</p>
        </div>

        <div>
          <input
            type="number"
            {...register("expenses")}
            placeholder="Expenses"
            className="w-full border p-2 rounded-lg"
          />
          <p className="text-sm text-red-500">{errors.expenses?.message}</p>
        </div>

        <div>
          <textarea
            {...register("hardshipReason")}
            placeholder="Hardship Reason (optional)"
            className="w-full border p-2 rounded-lg"
          />
        </div>

        <button
          disabled={isSubmitting}
          className="w-full bg-blue-600 hover:bg-blue-700 text-white py-2 rounded-xl transition"
        >
          {isSubmitting ? "Saving..." : "Submit"}
        </button>
      </form>
    </div>
  );
}