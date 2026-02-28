import { BrowserRouter, Routes, Route } from "react-router-dom";
import Layout from "@/shared/components/Layout";
import HardshipListPage from "@/features/hardships/pages/HardshipListPage";
import CreateHardshipPage from "@/features/hardships/pages/CreateHardshipPage";
import EditHardshipPage from "@/features/hardships/pages/EditHardshipPage";
import { Toaster } from "react-hot-toast";


function App() {
  return (
    <BrowserRouter>
     <Toaster position="top-right" />
      <Routes>
        <Route element={<Layout />}>
          <Route path="/" element={<HardshipListPage />} />
          <Route path="/create" element={<CreateHardshipPage />} />
          <Route path="/edit/:id" element={<EditHardshipPage />} />
        </Route>
      </Routes>
    </BrowserRouter>
  );
}

export default App;