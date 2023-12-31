﻿using Order.Domain.Events;
using Order.Domain.SeedWork;
using System;
using System.Collections.Generic;

namespace Order.Domain.AggregateModels.OrderModels
{
    public class Order : BaseEntity, IAggreagateRoot
    {
        public DateTime OrderDate { get; private set; }
        public string Description { get; private set; }
        public string UserName { get; private set; }
        public Address Address { get; private set; }
        public ICollection<OrderItem> OrderItems { get; private set; }

        public Order(DateTime orderDate, string description, string userName, Address address, ICollection<OrderItem> orderItems)
        {
            if(orderDate < DateTime.Now)
            {
                throw new Exception("OrderDate must be greater than now.");
            }

            if(address.City == null) 
            {
                throw new Exception("City cannot be empty.");
            }

            OrderDate = orderDate;
            Description = description;
            UserName = userName;
            Address = address;
            OrderItems = orderItems;

            AddDomainEvents(new OrderStartedDomainEvent(userName,this));
        }

        public void AddOrderItem(int quantity, decimal price, int productId) 
        {
            OrderItem item = new OrderItem(quantity,price,productId);
            OrderItems.Add(item);
        }
    }
}
