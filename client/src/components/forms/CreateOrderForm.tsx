import React, {useState} from "react";
import {api} from "../../api/api";
import {toast} from "react-toastify";

const CreateOrderForm = () => {

    const [form, setForm] = useState({
        senderCity: "",
        senderAddress: "",
        receiverCity: "",
        receiverAddress: "",
        weight: "",
        pickupDate: "",
    });

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setForm({...form, [e.target.name]: e.target.value});
    };

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();

        try {
            const response = await api.post("/orders", {
                ...form,
                weight: parseFloat(form.weight),
                pickupDate: new Date(form.pickupDate).toISOString(),
            });

            const orderId = response.data.orderId;

            if (response.status === 200 && orderId) {
                toast.success(`Order Successful: ${orderId}`, {
                    className: "bg-white text-black font-semibold",
                });
                return;
            }
        } catch (error) {
            console.error("Creation error:", error);
            toast.error("Error creating order", {
                className: "bg-white text-black font-semibold",
            });
        }
    };

    return (
        <div className="max-w-xl mx-auto p-6 bg-white rounded shadow mt-6">
            <h2 className="text-xl font-bold mb-4">Order creation</h2>
            <form onSubmit={handleSubmit} className="space-y-4">
                <div>
                    <label className="block font-semibold">Sender city</label>
                    <input
                        type="text"
                        name="senderCity"
                        required
                        value={form.senderCity}
                        onChange={handleChange}
                        className="w-full p-2 border rounded"
                    />
                </div>

                <div>
                    <label className="block font-semibold">Sender address</label>
                    <input
                        type="text"
                        name="senderAddress"
                        required
                        value={form.senderAddress}
                        onChange={handleChange}
                        className="w-full p-2 border rounded"
                    />
                </div>

                <div>
                    <label className="block font-semibold">Receiver city</label>
                    <input
                        type="text"
                        name="receiverCity"
                        required
                        value={form.receiverCity}
                        onChange={handleChange}
                        className="w-full p-2 border rounded"
                    />
                </div>

                <div>
                    <label className="block font-semibold">Receiver address</label>
                    <input
                        type="text"
                        name="receiverAddress"
                        required
                        value={form.receiverAddress}
                        onChange={handleChange}
                        className="w-full p-2 border rounded"
                    />
                </div>

                <div>
                    <label className="block font-semibold">Weight (kg)</label>
                    <input
                        type="number"
                        step="0.01"
                        name="weight"
                        min="0.1"
                        required
                        value={form.weight}
                        onChange={handleChange}
                        className="w-full p-2 border rounded"
                    />
                </div>

                <div>
                    <label className="block font-semibold">Pickup Date</label>
                    <input
                        type="datetime-local"
                        name="pickupDate"
                        required
                        value={form.pickupDate}
                        onChange={handleChange}
                        className="w-full p-2 border rounded"
                    />
                </div>

                <button
                    type="submit"
                    className="w-full py-2 bg-blue-600 text-white font-semibold rounded hover:bg-blue-700"
                >
                    Create order
                </button>
            </form>
        </div>
    );
};

export default CreateOrderForm;