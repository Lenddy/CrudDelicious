using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CrudDelicious.Models;

namespace CrudDelicious.Controllers;

public class dishesController : Controller
{
    //here we are setting a private instance of our DB class in our dishes_Context.cs file
    private DB _context;//_context can be what ever we want         it works i test it 

    //* here we are injecting  our context service into the constructor
    // we are making a constructor
    public dishesController (DB context){
        _context = context;
    }
    [HttpGet("")]
    public IActionResult Index()
    {
        // here we are creating a List<> of dishes  and assigning it  to _context.dish(variableName in dishesContext.cs).ToList(); 
        List<dishes> allDishes = _context.dish.ToList();


        return View("Index",allDishes);
    }

    [HttpGet("/dish/new")]
    public IActionResult New(){
        return View("New");
    }
    [HttpPost("/dish/create")]
    // we are creating new dishes instances thats why we are passing  dishes as datatype for our parameter 
    public IActionResult create(dishes newDish){
        if(ModelState.IsValid == false){
            return New();
        }
        //* this only runs if our modelState is valid
        //this is saying  go to my DB folder then the variable that is call dish and add a new dish
        // this adds it to list of post but i does not saves it in the data base yet 
        _context.dish.Add(newDish);
        // * this saves it in the data base
        _context.SaveChanges();
        return Redirect("/");
    }
    [HttpGet("/dish/view/{dishId}")]
    // we are passing the id to the url
    public IActionResult view_one(int dishId){
        // here we are searching for the first id that matches  then setting it to be the on tha we put on the url
        dishes? id = _context.dish.FirstOrDefault(id => id.DishId == dishId);
        // here we are checking if the id is == to null(does not exist) the we will be redirected the index page
        if(id == null){
            return RedirectToAction("Index");
        }
        //!dont call your file view call it viewOne or something else
        // you need to pass in you id 
        return View("viewOne",id);
    }
    [HttpPost("/dish/delete/{delete}")]
    // this is for deleting 
    public IActionResult delete(int delete){
        // here we are searching for the first id that matches  then setting it to be the on tha we put on the url
        dishes? deleteId = _context.dish.FirstOrDefault(dish => dish.DishId == delete);
        // if it finds something the it will delete it 
        if(deleteId != null){
            _context.dish.Remove(deleteId);
            _context.SaveChanges();
        }
        // Else it will  redirect you to the index page
        return RedirectToAction("Index");
        

    }

    [HttpGet("/dish/edit/{edit}")]

    public IActionResult edit(int edit){
         // here we are searching for the first id that matches  then setting it to be the on tha we put on the url
        dishes? editId = _context.dish.FirstOrDefault(dish => dish.DishId == edit);
        //if it finds nothing then it will redirect you to the index page 
        if(editId == null){
            return RedirectToAction("Index");
        }
        // else it will render the edit page
        return View("edit",editId);
    }

    [HttpPost("/dish/update/{UpdatedDishId}")]
    public IActionResult update( int UpdatedDishId,dishes UpdatedDish){
        if(ModelState.IsValid == false){
            //* you can do all the logic bello in 1 line by calling the get edit function that you made above because its already doing the logic in the edit function
            return edit(UpdatedDishId);
            // * long way to doit
            // dishes? originalDish = _context.dish.FirstOrDefault(dish => dish.DishId == UpdatedDish.DishId);
            // if(originalDish == null){
            //     return RedirectToAction("index");
            // }
            // return View("edit",originalDish);
        }
        //here we are searching for the first id that matches  then setting it to be the on tha we put on the url
        dishes? originalDish = _context.dish.FirstOrDefault(dish => dish.DishId == UpdatedDishId);
        //if it finds nothing then it will redirect you to the index page 
        if(originalDish == null){
            return RedirectToAction("index");
        }
        // this are the thing that we are updating
        originalDish.Name = UpdatedDish.Name;
        originalDish.Chef = UpdatedDish.Chef;
        originalDish.Calories = UpdatedDish.Calories;
        originalDish.Tastiness = UpdatedDish.Tastiness;
        originalDish.Description = UpdatedDish.Description;
        originalDish.UpdatedAt = DateTime.Now;
        _context.dish.Update(originalDish);
        _context.SaveChanges();
        // 
            // return RedirectToAction("Index",originalDish.DishId);
        return view_one(originalDish.DishId);
    }

}
