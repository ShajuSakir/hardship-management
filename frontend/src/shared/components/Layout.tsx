import { Link, Outlet, useLocation } from "react-router-dom";

export default function Layout() {
  const location = useLocation();

  const linkClass = (path: string) =>
    `px-3 py-2 rounded-md text-sm font-medium ${
      location.pathname === path
        ? "bg-blue-600 text-white"
        : "text-gray-700 hover:bg-gray-200"
    }`;

  return (
    <div className="min-h-screen bg-gray-100">
      <nav className="bg-white shadow-md px-6 py-4 flex justify-between items-center">
        <h1 className="text-lg font-bold text-gray-800">
          Hardship Application System
        </h1>

        <div className="flex gap-3">
          <Link to="/" className={linkClass("/")}>
            Applications
          </Link>
          <Link to="/create" className={linkClass("/create")}>
            Create
          </Link>
        </div>
      </nav>

      <main className="p-6">
        <Outlet />
      </main>
    </div>
  );
}