import React, {useEffect, useState} from "react";
import {api} from "../../api/api";
import {Order} from "../../models/Order";

const OrderList = () => {
    const [orders, setOrders] = useState<Order[]>([]);
    const [selectedOrder, setSelectedOrder] = useState<Order | null>(null);
    const [page, setPage] = useState<number>(0);


    const fetchOrders = async (page: number): Promise<Order[]> => {
        try {
            const response = await api.get("/orders", {
                params: {
                    limit: 10,
                    page,
                },
            });

            return response.data as Order[];
        } catch (error) {
            console.error("Ошибка при загрузке заказов:", error);
            return [];
        }
    };

    const handleOnLoadNext = async () => {
        const nextPage = page + 1;
        const newOrders = await fetchOrders(nextPage);

        if (newOrders.length > 0) {
            setOrders(newOrders);
            setPage(nextPage);
        }
    };

    const handleOnLoadPrev = async () => {
        if (page === 0) return;

        const prevPage = page - 1;
        const newOrders = await fetchOrders(prevPage);

        setOrders(newOrders);
        setPage(prevPage);
    };

    const handleOnReload = async () => {
        const orders = await fetchOrders(page);
        setOrders(orders);
    }

    useEffect(() => {
        const load = async () => {
            const initialOrders = await fetchOrders(0);
            setOrders(initialOrders);
        };

        load();
    }, []);

    const openOrderDetails = (orderId: string) => {
        api.get(`/orders/${orderId}`)
            .then((res) => setSelectedOrder(res.data));
    };

    return (
        <div className="p-4">
            <h1 className="text-xl font-bold mb-4">Orders List</h1>
            <div className="flex justify-center mb-4">
                <button
                    onClick={handleOnLoadPrev}
                    className="px-4 py-2 mx-1 bg-blue-600 text-white rounded hover:bg-blue-700 disabled:opacity-50"
                >
                    Prev
                </button>
                <button
                    onClick={handleOnReload}
                    className="px-4 py-2 mx-1 bg-blue-600 text-white rounded hover:bg-blue-700 disabled:opacity-50"
                >
                    Reload
                </button>
                <button
                    onClick={handleOnLoadNext}
                    className="px-4 py-2 mx-1 bg-blue-600 text-white rounded hover:bg-blue-700 disabled:opacity-50"
                >
                    Next
                </button>
            </div>
            <div className="overflow-x-auto rounded border overflow-hidden">
                <table className="min-w-full rounded text-sm sm:text-base bg-white">
                    <thead>
                    <tr>
                        <th className="border px-4 py-2">ID</th>
                        <th className="border px-4 py-2">FROM</th>
                        <th className="border px-4 py-2">TO</th>
                        <th className="border px-4 py-2">WEIGHT</th>
                        <th className="border px-4 py-2">DATE</th>
                        <th className="border px-4 py-2">ACTIONS</th>
                    </tr>
                    </thead>
                    <tbody>
                    {orders.map((order) => (
                        <tr key={order.orderId}>
                            <td className="border px-4 py-2 text-sm">{order.orderId}</td>
                            <td className="border px-4 py-2">{order.senderCity}</td>
                            <td className="border px-4 py-2">{order.receiverCity}</td>
                            <td className="border px-4 py-2">{order.weight} кг</td>
                            <td className="border px-4 py-2">{new Date(order.pickupDate).toLocaleString()}</td>
                            <td className="border px-4 py-2">
                                <button
                                    className="text-blue-500 underline"
                                    onClick={() => openOrderDetails(order.orderId)}
                                >
                                    Info
                                </button>
                            </td>
                        </tr>
                    ))}
                    </tbody>
                </table>
            </div>

            {selectedOrder && (
                <div
                    className="fixed top-0 left-0 w-full h-full bg-opacity-50 flex items-center justify-center z-50">
                    <div
                        className="bg-white rounded shadow-xl w-full max-w-2xl mx-4 sm:mx-6 md:mx-auto p-6 overflow-y-auto max-h-[90vh]">
                        <h2 className="text-lg font-bold mb-4">Order № {selectedOrder.orderId}</h2>
                        <div className="mb-2">
                            <strong>Created:</strong> {new Date(selectedOrder.createdAt).toLocaleString()}
                        </div>
                        <div className="mb-2">
                            <strong>Sender:</strong> {selectedOrder.senderCity}, {selectedOrder.senderAddress}
                        </div>
                        <div className="mb-2">
                            <strong>Receiver:</strong> {selectedOrder.receiverCity}, {selectedOrder.receiverAddress}
                        </div>
                        <div className="mb-2">
                            <strong>Weight:</strong> {selectedOrder.weight} kg
                        </div>
                        <div className="mb-2">
                            <strong>Pickup date:</strong> {new Date(selectedOrder.pickupDate).toLocaleString()}
                        </div>
                        <button
                            className="mt-4 text-blue-500 underline"
                            onClick={() => setSelectedOrder(null)}
                        >
                            Close
                        </button>
                    </div>
                </div>
            )}
        </div>
    );
};

export default OrderList;