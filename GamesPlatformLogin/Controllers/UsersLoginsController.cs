using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GamesPlatformLogin.Context;
using GamesPlatformLogin.Models;

namespace GamesPlatformLogin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersLoginsController : Controller
    {
        private readonly AppDbContext _context;

        public UsersLoginsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: UsersLogins
        public async Task<IActionResult> Index()
        {
            return View(await _context.UserLogin.ToListAsync());
        }

        // GET: UsersLogins/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usersLogin = await _context.UserLogin
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usersLogin == null)
            {
                return NotFound();
            }

            return View(usersLogin);
        }

        // GET: UsersLogins/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UsersLogins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Email,Password,UserName,City,Department")] UsersLogin usersLogin)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usersLogin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(usersLogin);
        }

        // GET: UsersLogins/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usersLogin = await _context.UserLogin.FindAsync(id);
            if (usersLogin == null)
            {
                return NotFound();
            }
            return View(usersLogin);
        }

        // POST: UsersLogins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Email,Password,UserName,City,Department")] UsersLogin usersLogin)
        {
            if (id != usersLogin.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usersLogin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsersLoginExists(usersLogin.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(usersLogin);
        }

        // GET: UsersLogins/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usersLogin = await _context.UserLogin
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usersLogin == null)
            {
                return NotFound();
            }

            return View(usersLogin);
        }

        // POST: UsersLogins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usersLogin = await _context.UserLogin.FindAsync(id);
            _context.UserLogin.Remove(usersLogin);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsersLogin>>> GetUsers()
        {
            return await _context.UserLogin.ToListAsync();
        }

        [HttpGet("{username}/{password}")]
        public ActionResult<List<UsersLogin>>GetLogin(string username,string password)
        {
            var usersLogin = _context.UserLogin.Where(x=> x.UserName.Equals(username) && x.Password.Equals(password)).ToList();
            if(usersLogin == null)
            {
                return NotFound();
            }
            return usersLogin;

        }

        private bool UsersLoginExists(int id)
        {
            return _context.UserLogin.Any(e => e.Id == id);
        }
    }
}
