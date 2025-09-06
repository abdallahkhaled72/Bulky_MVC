using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Models
{
    /*
            Order Header will have all the information of all order like:
                1- where the order is being shipped
                2- what is the paying status
                3- what was the order date
                4- what was the payment id
                5- what was the tracking information
                .. and much more
     */
    public class OrderHeader
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }

        public DateTime OrderDate { get; set; }
        public DateTime ShippingDate { get; set; }
        public double OrderTotal{ get; set; }


        public string? OrderStatus { get; set; }
        public string? PaymentStatus { get; set; }
        public string? TrackingNumber { get; set; }
        public string? Carrier { get; set; }


        // PaymentDtae and PaymentDueDate both properties are gonna use to only the company users becasue they have an option to pay later
        public DateTime PaymentDate { get; set; }
        public DateOnly PaymentDueDate { get; set; }


        public string? SessionId { get; set; }
        // this will be created id the session has been successed
        public string? PaymentIntentId { get; set; } // will use when the user want to pay by creditcard



        /*
            Those properties are the same properties in the payment view
         */
        [Required]
        public string PhoneNumber{ get; set; }
        
        [Required]
        public string StreetAddress{ get; set; }
        [Required]
        public string City{ get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string PostalCode { get; set; }
        [Required]
        public string Name { get; set; }

    }
}
