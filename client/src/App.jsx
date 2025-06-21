import {AppRouter} from "./router/AppRouter.js";
import {ToastContainer} from "react-toastify";

export const App = () => (
    <div className="min-h-screen bg-gray-100">
        <header className="bg-black text-white py-4 px-6 shadow">
            <h1 className="text-xl font-semibold">Order Service</h1>
        </header>
        <main>
            <AppRouter />
        </main>
        <ToastContainer
            position="top-center"
            autoClose={5000}
            theme="light"
        />
    </div>
);
