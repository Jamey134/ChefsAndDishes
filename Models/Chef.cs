#pragma warning disable CS8618

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChefsAndDishes.Models;
public class Chef
{        
    [Key]        
    public int ChefId { get; set; }
    
    [Required] 
    [MinLength(0, ErrorMessage = "First name is required.")]       
    public string FirstName { get; set; }
    
    [Required]  
    [MinLength(0, ErrorMessage = "Last name is required.")]      
    public string LastName { get; set; }         
    
    [Required]
    [MinimumDOB(18)]
    
    public DateTime DateOfBirth { get; set; }          
    
    public DateTime CreatedAt {get;set;} = DateTime.Now;        
    public DateTime UpdatedAt {get;set;} = DateTime.Now;
    public List<Dish> AllDishes { get; set; } = new List<Dish>();
}

public class MinimumDOBAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        DateTime Now = DateTime.Now;
        DateTime Input = (DateTime)value;


        if (Input > Now)
        {
            return new ValidationResult("Date of Birth must be in the past.");
        } else {
            return ValidationResult.Success;
        }
    }
}