using System;
using System.Collections.Generic;
using ServiceStack.DataAnnotations;
namespace WebApi2.SqlServerDAL
{
    public enum PhoneType
    {
        Home,
        Work,
        Mobile,
    }

    public enum AddressType
    {
        Home,
        Work,
        Other,
    }

    public class Address
    {
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string ZipCode { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }

    public class Customer
    {
        [AutoIncrement]
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Index(Unique = true)]
        public string Email { get; set; }

        public Dictionary<PhoneType, string> PhoneNumbers { get; private set; }
        public Dictionary<AddressType, Address> Addresses { get; private set; }
        public DateTime CreatedAt { get; set; }

        public Customer()
        {
            this.PhoneNumbers = new Dictionary<PhoneType, string>();
            this.Addresses = new Dictionary<AddressType, Address>();
        }
    }

    public class Order
    {
        [AutoIncrement]
        public int Id { get; set; }

        [References(typeof(Customer))]
        public int CustomerId { get; set; }

        [References(typeof(Employee))]
        public int EmployeeId { get; set; }

        public Address ShippingAddress { get; set; }

        public DateTime? OrderDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public int? ShipVia { get; set; }
        public decimal Freight { get; set; }
        public decimal Total { get; set; }
    }
}
