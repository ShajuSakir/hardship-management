export const ROUTES = {
    home: "/",
    hardships: "/hardships",
    createHardship: "/hardships/create",
    hardshipDetails: (id: string | number) => `/hardships/${id}`,
    editHardship: (id: string | number) => `/edit/${id}`,
  };