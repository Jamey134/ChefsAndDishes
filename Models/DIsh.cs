#pragma warning disable CS8618
namespace ChefsAndDishes.Models;
using System.ComponentModel.DataAnnotations;
public class Dish
{    
    [Key]    
    public int DishId { get; set; }
    [Required]
    [MinLength(3, ErrorMessage = "Name Must Be At Least 3 Letters")]
    public string Name {get;set;}
    
    [Required]
    [Range(0, Int32.MaxValue, ErrorMessage = "Calories Must Be Greater Than 0")]
    public int Calories {get;set;}

    [Required]
    [Range(1, 6, ErrorMessage = "Tastiness Rating Must Be Between 1 and 5")]
    public int Tastiness {get;set;}
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public int ChefId { get; set; } // <--- The foreign Key
    // Our navigation property to track which Chef made this Post
    // It is VERY important to include the ? on the datatype or this won't work!
    public Chef? Creator { get; set; } //<--- Creator is that specific chef associated wth their foreign key (ChefId).
}
