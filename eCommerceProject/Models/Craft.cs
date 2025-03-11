using System.ComponentModel.DataAnnotations;

namespace eCommerceProject.Models;


/// <summary>
/// Represents a single craft available for purchase.
/// </summary>
public class Craft
{
    /// <summary>
    /// The unique identifier for the craft.
    /// </summary>
    [Key]
    public int CraftId { get; set; }
    /// <summary>
    /// The title of the craft.
    /// </summary>
    [Required]
    public string Title { get; set; }
    /// <summary>
    /// The sales price of the craft
    /// </summary>
    [Range(0.01, 500)]
    public decimal Price { get; set; }

    // TODO: Add Category
}

/// <summary>
/// A single craft that have bee added to the user's shopping cart cookie.
/// </summary>
public class CraftCartViewModel
{
    public int CraftId { get; set; }

    public string Title { get; set; }

    public double Price { get; set; }
}