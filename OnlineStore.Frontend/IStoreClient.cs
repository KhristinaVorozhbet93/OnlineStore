﻿namespace OnlineStore
{
    public interface IStoreClient
    {
        Task<List<Product>> GetProducts();
    }
}