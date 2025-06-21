import {BrowserRouter as Router, Routes, Route} from "react-router-dom";
import OrderList from "../components/lists/OrderList";
import {OrderPage} from "../pages/OrderPage";

export const AppRouter = () => {
    return (
        <Router>
            <Routes>
                <Route path="/" element={
                    <OrderPage/>
                }/>
            </Routes>
        </Router>
    );
};