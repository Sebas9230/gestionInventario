// src/components/OrderForm.js

import React, { useState } from 'react';
import { updateInventory, getProduct } from '../api';
import '../styles/OrderForm.css';

const OrderForm = () => {
    const [productId, setProductId] = useState('');
    const [quantity, setQuantity] = useState('');
    const [message, setMessage] = useState('');

    const isNumeric = (value) => {
        return /^-?\d+$/.test(value);
    };

    const handleSubmit = async (event) => {
        event.preventDefault();

        if (!productId || !quantity) {
            setMessage('Se debe ingresar los 2 parámetros.');
            return;
        }

        if (!isNumeric(quantity)) {
            setMessage('Quantity debe ser un número.');
            return;
        }

        try {
            // Obtén los datos del producto original
            const originalProductResponse = await getProduct(productId);
            const originalProduct = originalProductResponse.producto;

            if (!originalProduct) {
                setMessage('Product no encontrado.');
                return;
            }

            // Obtener valor original del stock
            const stock = originalProduct.quantity;

            // Convertir quantity a número para la comparación
            const quantityNumber = parseInt(quantity, 10);

            // Validar que el stock sea mayor que la cantidad ingresada
            if (stock < quantityNumber) {
                setMessage(`La cantidad de pedido (${quantityNumber}) excede el stock del producto (${stock}).`);
                return;
            }

            // Calcular nueva cantidad
            const newQuantity = (stock - quantityNumber).toString();

            // Crea el objeto actualizado con los datos originales y la nueva cantidad
            const updatedProduct = {
                id: productId,
                quantity: newQuantity,
                name: originalProduct.name,  // Usa el nombre original
                price: originalProduct.price  // Usa el precio original
            };

            const response = await updateInventory(productId, updatedProduct);
            setMessage('Pedido realizado satisfactoriamente!');
        } catch (error) {
            setMessage('No se pudo realizar el pedido.');
        }
    };

    return (
        <div>
            <h2>Realizar Pedidos</h2>
            <form onSubmit={handleSubmit}>
                <div>
                    <label htmlFor="productId">Product ID:</label>
                    <input
                        type="text"
                        id="productId"
                        value={productId}
                        onChange={(e) => setProductId(e.target.value)}
                    />
                </div>
                <div>
                    <label htmlFor="quantity">Quantity:</label>
                    <input
                        type="text"
                        id="quantity"
                        value={quantity}
                        onChange={(e) => setQuantity(e.target.value)}
                    />
                </div>
                <button type="submit">Realizar Pedido</button>
            </form>
            {message && <p>{message}</p>}
        </div>
    );
};

export default OrderForm;
