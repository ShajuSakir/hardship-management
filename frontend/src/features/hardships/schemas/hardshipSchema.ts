import { z } from "zod";

export const hardshipSchema = z.object({
  customerName: z
    .string()
    .min(1, "Customer name is required"),

  dateOfBirth: z
    .string()
    .min(1, "Date of birth is required"),

    income: z.coerce
    .number()
    .refine((val) => !isNaN(val), {
      message: "Income is required",
    })
    .positive("Income must be positive"),
  
  expenses: z.coerce
    .number()
    .refine((val) => !isNaN(val), {
      message: "Expenses are required",
    })
    .nonnegative("Expenses must be 0 or more"),

  hardshipReason: z.string().optional(),
});

export type HardshipFormValues = z.output<typeof hardshipSchema>;