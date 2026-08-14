using System;
using System.Collections.Generic;
using System.Text;

namespace BookVertex.Utility
{
    public class SD
    {
        public const string RoleCustomer = "Customer";
        public const string RoleAdmin = "Admin";
        public const string RoleEmployee = "Employee";


        public const string StatusPending = "Pending";
        public const string StatusApproved = "Approved";
        public const string StatusInProcess = "Processing";
        public const string StatusShipped = "Shipped";
        public const string StatusCancelled = "Cancelled";
        public const string StatusRefunded = "Refunded";

        //Used to track the cart count only
        public const string SessionCart = "SessionShoppingCartBookVertex";
    }
}

