export interface Order {
    orderId: string;
    senderCity: string;
    senderAddress: string;
    receiverCity: string;
    receiverAddress: string;
    weight: number;
    pickupDate: string;
    createdAt: string;
}