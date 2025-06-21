import CreateOrderForm from "../components/forms/CreateOrderForm";
import OrderList from "../components/lists/OrderList";

export const OrderPage = () => {
    return (
        <div>
            <CreateOrderForm/>
            <OrderList/>
        </div>
    )
}