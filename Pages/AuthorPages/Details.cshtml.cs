using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Trifan_Carla_Lab2.Models;

namespace Trifan_Carla_Lab2.Pages.AuthorPages;

public class DetailsModel : PageModel
{
    private readonly Trifan_Carla_Lab2Context _context;
    public DetailsModel(Trifan_Carla_Lab2Context context)
    {
        _context = context;
    }

    public Author Author { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var author = await _context.Author.FirstOrDefaultAsync(m => m.ID == id);
        if (author is null)
        {
            return NotFound();
        }
        else
        {
            Author = author;
        }

        return Page();
    }
}
