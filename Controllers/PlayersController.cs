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
            if(Singleton.Instance.player_list.Count() != 0)
            {
                return View(Singleton.Instance.player_list);
            }
            else
            {
                return View(Singleton.Instance.PlayerList);
            }
            
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
                bool TypeListArt = false;
                bool TypeListC = false;
                int IdNumber = 0;
                int NumberListCraft = Singleton.Instance.player_list.GetCount;
                int NumberListC = Singleton.Instance.PlayerList.Count;

                if (NumberListCraft != 0)
                {
                    TypeListArt = true;
                    if (NumberListCraft == 1 && Singleton.Instance.player_list.First().Id == 0)
                    {
                        IdNumber = 1;
                        Singleton.Instance.player_list.ExtractAtStart();
                    }
                    else
                    {
                        IdNumber = NumberListCraft + 1;
                    }

                }

                if (NumberListC != 0)
                {
                    TypeListC = true;
                    if (NumberListC == 1 && Singleton.Instance.PlayerList.First().Id == 0)
                    {
                        IdNumber = 1;
                        var DeletePlayerCreate = Singleton.Instance.PlayerList.Find(x => x.Id == 0);
                        Singleton.Instance.PlayerList.Remove(DeletePlayerCreate);
                    }
                    else
                    {
                        IdNumber = Singleton.Instance.PlayerList.Count + 1;
                    }
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

                if (TypeListArt == true)
                {
                    Singleton.Instance.player_list.InsertAtEnd(newPlayer);
                }
                else
                {
                    Singleton.Instance.PlayerList.Add(newPlayer);
                }

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
            if (Singleton.Instance.PlayerList.Count != 0)
            {
                var EditPlayer = Singleton.Instance.PlayerList.Find(x => x.Id == id);
                return View(EditPlayer);
            }
            else
            {
                var EditPlayer = Singleton.Instance.player_list.ElementAt(id-1);
                return View(EditPlayer);
            }
            
            
        }

        // POST: Players/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                int EditId;
                string EditName, EditLastName, EditPosition;
                if (Singleton.Instance.PlayerList.Count != 0)
                {
                    EditId = Convert.ToInt32(Singleton.Instance.PlayerList.Find(x => x.Id == id).Id);
                    EditName = Convert.ToString(Singleton.Instance.PlayerList.Find(x => x.Id == id).Name);
                    EditLastName = Convert.ToString(Singleton.Instance.PlayerList.Find(x => x.Id == id).LastName);
                    EditPosition = Convert.ToString(Singleton.Instance.PlayerList.Find(x => x.Id == id).Position);
                    var DeletePlayerEdit = Singleton.Instance.PlayerList.Find(x => x.Id == id);
                    Singleton.Instance.PlayerList.Remove(DeletePlayerEdit);
                }
                else
                {
                    EditId = Convert.ToInt32(Singleton.Instance.player_list.ElementAt(id-1).Id);
                    EditName = Convert.ToString(Singleton.Instance.player_list.ElementAt(id-1).Name);
                    EditLastName = Convert.ToString(Singleton.Instance.player_list.ElementAt(id-1).LastName);
                    EditPosition = Convert.ToString(Singleton.Instance.player_list.ElementAt(id-1).Position);
                    Singleton.Instance.player_list.ExtractAtPosition(id - 1);
                }

                var EditPlayer = new Models.Players
                {
                    Id = EditId,
                    Name = EditName,
                    LastName = EditLastName,
                    Position = EditPosition,
                    Salary = Convert.ToSingle(collection["Salary"]),
                    Club = collection["Club"]
                };

                if(Singleton.Instance.PlayerList.Count != 0)
                {
                    Singleton.Instance.PlayerList.Insert(id, EditPlayer);
                }
                else
                {
                    Singleton.Instance.player_list.InsertAtPosition(EditPlayer, id-1);
                }
                
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
            if (Singleton.Instance.PlayerList.Count != 0)
            {
                var DeletedPlayer = Singleton.Instance.PlayerList.Find(x => x.Id == id);
                return View(DeletedPlayer);
            }
            else
            {
                var DeletedPlayer = Singleton.Instance.player_list.ElementAt(id-1);
                return View(DeletedPlayer);
            }
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
                Singleton.Instance.player_list.ExtractAtPosition(id-1);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult CraftList()
        {
            if(Singleton.Instance.player_list.Count() == 0)
            {
                var newPlayer = new Models.Players { };
                Singleton.Instance.player_list.InsertAtEnd(newPlayer);
                return RedirectToAction(nameof(Create));
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        public ActionResult ListC()
        {
            if(Singleton.Instance.PlayerList.Count() == 0)
            {
                var newPlayer = new Models.Players { };
                Singleton.Instance.PlayerList.Add(newPlayer);
                return RedirectToAction(nameof(Create));
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
            
        }
    }
}
