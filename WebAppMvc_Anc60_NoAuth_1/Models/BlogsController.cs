using Microsoft.AspNetCore.Mvc;

namespace WebAppMvc_Anc60_NoAuth_1.Models;

public class BlogsController : Controller
{
    private readonly BloggingContext _context;
    public BlogsController(BloggingContext context)
    {
        _context = context;
    }
    public IActionResult Index()
    {
        if (_context.Blogs == null)
        {
            throw new ArgumentNullException("BloggingContext does not contain any blogs.");
        }
        return View(_context.Blogs.ToList());
    }
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Blog blog)
    {
        if (ModelState.IsValid)
        {
            if (_context.Blogs == null)
            {
                throw new ArgumentNullException("BloggingContext does not contain any blogs.");
            }
            _context.Blogs.Add(blog);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(blog);
    }
}