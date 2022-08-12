#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
//your namespace
namespace CrudDelicious.Models;
//classname
public class dishes
{
//this is the primary key
    [Key]
    public int DishId { get; set; }
//change the field as needed
    [Required]
    [MinLength(2)]
    [MaxLength(45)]
    [Display(Name = "Dish Name")]
    public string Name { get; set; }
    [Required]
    [MinLength(2)]
    [MaxLength(45)] 
    [Display(Name = "Chef Name")] 
    public string Chef { get; set; }
// you need to use range if you want a specific number to be added
        [Required]
        [Range(1,5)]
    public int Tastiness { get; set; }

    [Required]
    [Range(1,Int32.MaxValue)]

    public int Calories { get; set; }
    [Required]
    [MinLength(2)]
    public string Description{get;set;}
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}