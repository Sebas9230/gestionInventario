// src/components/ProductList.js
import React, { useEffect, useState } from 'react';
import { fetchInventory } from '../api';
import '../styles/ProductList.css'; // Importa tu archivo de estilos

const ProductList = () => {
    const [products, setProducts] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        const getProducts = async () => {
            try {
                const productos = await fetchInventory();
                setProducts(productos.listaProductos);
            } catch (error) {
                setError(error.message);
            } finally {
                setLoading(false);
            }
        };

        getProducts();
    }, []);

    if (loading) {
        return <div className="loading">Loading...</div>;
    }

    if (error) {
        return <div className="error">Error: {error}</div>;
    }

    return (
        <div className="product-list-container">
            <h1>Available Watches</h1>
            <div className="product-grid">
                {products.map(product => (
                    <div className="product-card" key={product.id}>
                        <div className="product-details">
                            <h2 className="product-id">{product.id}</h2>
                            <h2 className="product-name">{product.name}</h2>
                            <p className="product-price">${product.price} USD</p>
                            <p className="product-quantity">Stock: {product.quantity}</p>
                        </div>
                    </div>
                ))}
            </div>
        </div>
    );
};

export default ProductList;
