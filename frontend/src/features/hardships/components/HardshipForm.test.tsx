import { render, screen } from "@testing-library/react";
import userEvent from "@testing-library/user-event";
import HardshipForm from "./HardshipForm";
import { describe, it, expect, vi } from "vitest";

describe("HardshipForm", () => {
  const renderForm = () => {
    const onSubmit = vi.fn().mockResolvedValue(undefined);

    render(
      <HardshipForm
        title="Create Hardship"
        onSubmit={onSubmit}
      />
    );

    return { onSubmit };
  };

  it("shows validation errors when submitting empty form", async () => {
    renderForm();

    const submitButton = screen.getByRole("button", { name: /submit/i });
    await userEvent.click(submitButton);

    expect(await screen.findAllByText(/required/i)).toBeTruthy();
  });

  it("calls onSubmit when form is valid", async () => {
    const { onSubmit } = renderForm();

    await userEvent.type(screen.getByPlaceholderText("Customer Name"), "John Doe");
    await userEvent.type(screen.getByLabelText(/date/i), "2000-01-01");
    await userEvent.type(screen.getByPlaceholderText("Income"), "5000");
    await userEvent.type(screen.getByPlaceholderText("Expenses"), "2000");

    const submitButton = screen.getByRole("button", { name: /submit/i });
    await userEvent.click(submitButton);

    expect(onSubmit).toHaveBeenCalledTimes(1);
  });
});