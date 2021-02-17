using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Lab1_KASR_MAGZ.Models.Data;
using ListLibrary;

namespace Lab1_KASR_MAGZ.Controllers
{
    public class PlayersController : Controller
    {        
        // GET: Players
        public ActionResult Index()
        {
            return View(Singleton.Instance.player_list);
        }

        // GET: Players/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Players/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Players/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                int IdNumber;
                if(Singleton.Instance.PlayerList.Count ==0)
                {
                    IdNumber = 0;
                }
                else
                {
                    IdNumber = Singleton.Instance.PlayerList.Count;
                }

                var newPlayer = new Models.Players
                {
                    Id = IdNumber,
                    Name = collection["Name"],
                    LastName = collection["LastName"],
                    Position = collection["Position"],
                    Salary = Convert.ToInt32(collection["Salary"]),
                    Club = collection["Club"]
                };
                Singleton.Instance.PlayerList.Add(newPlayer);                
                Singleton.Instance.player_list.InsertAtEnd(newPlayer);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Players/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Players/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Players/Delete/5
        public ActionResult Delete(int id)
        {
            var DeletedPlayer = Singleton.Instance.PlayerList.Find(x => x.Id == id);
            return View(DeletedPlayer);
        }

        // POST: Players/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var DeletedPlayer = Singleton.Instance.PlayerList.Find(x => x.Id == id);
                Singleton.Instance.PlayerList.Remove(DeletedPlayer);
                Singleton.Instance.player_list.ExtractAtPosition(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }        
    }
}
