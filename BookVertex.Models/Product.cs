using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BookVertex.Models
{
    /// <summary>
    /// Represents a book product in the store with built-in tiered pricing for bulk orders.
    /// </summary>
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        [Required]
        public string ISBN { get; set; } = string.Empty;

        [Required]
        public string Author { get; set; } = string.Empty;


        // Original catalog retail price / MSRP
        [Required]
        [Display(Name = "List Price")]
        [Range(1, 1000)]
        public double ListPrice { get; set; }


        // Standard retail price for buying 1 to 49 quantities
        [Required]
        [Display(Name = "Price for 1-50")]
        [Range(1, 1000)]
        public double Price { get; set; }


        // Bulk discount price for buying 50 to 99 quantities
        [Required]
        [Display(Name = "Price for 50+")]
        [Range(1, 1000)]
        public double Price50 { get; set; }


        // Maximum bulk discount price for buying 100+ quantities
        [Required]
        [Display(Name = "Price for  100+")]
        [Range(1, 1000)]
        public double Price100 { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [ValidateNever]
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        [ValidateNever]
        [Display(Name = "Product Image")]
        public string? ImageUrl { get; set; }

        [ValidateNever]
        public string? ImagePublicId { get; set; }
    }
}
