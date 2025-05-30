using BulkyWebRazor_Temp.Data;
using BulkyWebRazor_Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWebRazor_Temp.Pages.Categories
{

    // Soooooooooo Important Attribute without it u will face errors like ur object is null or ur object is not accessable
    [BindProperties]  // this attribute uses to make all properties are public in this class.
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        //[BindProperty]   // this attribute uses to make the property public and accessable to all th functions in this class.
        public Category Category { get; set; }
        public CreateModel(ApplicationDbContext db)
        {
            this._db = db;
        }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            _db.Categories.Add(Category);
            _db.SaveChanges();
            TempData["success"] = "Category created successfully";
            return RedirectToPage("Index");
        }
    }
}
