using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Lab1_KASR_MAGZ.Models.Data;
using Lab1_KASR_MAGZ.Models;
using ListLibrary;
using System.IO;
using System.Data;
using Microsoft.AspNetCore.Hosting;
using System.Diagnostics;

namespace Lab1_KASR_MAGZ.Controllers
{
    public class PlayersController : Controller
    {        

        private IHostingEnvironment Environment;
        Stopwatch timer = new Stopwatch();
        

        public PlayersController(IHostingEnvironment _environment)
        {
            Environment = _environment;
        }

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

        [HttpPost]
        public IActionResult Index(IFormFile postedFile)
        {
            timer.Start();
            bool TypeListArt = false;
            int IdNumber = 0;
            int NumberListCraft = Singleton.Instance.player_list.GetCount;
            int NumberListC = Singleton.Instance.PlayerList.Count;
            string ListType = "";

            if (NumberListCraft != 0)
            {
                TypeListArt = true;
                if (NumberListCraft == 1 && Singleton.Instance.player_list.First().Id == 0)
                {
                    IdNumber = 0;
                    ListType = "Lista Artesanal. ";
                    Singleton.Instance.player_list.ExtractAtStart();
                }
                else
                {
                    IdNumber = NumberListCraft + 1;
                }

            }

            if (NumberListC != 0)
            {
                if (NumberListC == 1 && Singleton.Instance.PlayerList.First().Id == 0)
                {
                    IdNumber = 0;
                    ListType = "Lista C#. ";
                    var DeletePlayerCreate = Singleton.Instance.PlayerList.Find(x => x.Id == 0);
                    Singleton.Instance.PlayerList.Remove(DeletePlayerCreate);
                }
                else
                {
                    IdNumber = Singleton.Instance.PlayerList.Count + 1;
                }
            }

            if (postedFile != null)
            {
                string path = Path.Combine(this.Environment.WebRootPath, "Uploads");
                if(!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                string fileName = Path.GetFileName(postedFile.FileName);
                string filePath = Path.Combine(path, fileName);
                
                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }
                string csvData = System.IO.File.ReadAllText(filePath);

                StreamReader streamReader = new StreamReader(filePath);
                string lineaActual;

                while(!streamReader.EndOfStream)
                {
                    lineaActual = streamReader.ReadLine();

                    IdNumber += 1;
                    string[] FileInformationList = lineaActual.Split(',');
                    string FileName = FileInformationList[0];
                    string FileLastName = FileInformationList[1];
                    string FilePosition = FileInformationList[2];
                    int FileSalary = Convert.ToInt32(FileInformationList[3]);
                    string FileClub = FileInformationList[4];

                    var FilePlayer = new Models.Players
                    {
                        Id = IdNumber,
                        Name = FileName,
                        LastName = FileLastName,
                        Position = FilePosition,
                        Salary = FileSalary,
                        Club = FileClub
                    };

                    if (TypeListArt == true)
                    {
                        Singleton.Instance.player_list.InsertAtEnd(FilePlayer);
                    }
                    else
                    {
                        Singleton.Instance.PlayerList.Add(FilePlayer);
                    }
                }

                //MIDIENDO TIEMPO Y GUARDARLO
                timer.Stop();
                var NewDescriptionTime = new Models.TimeDescription
                {
                    DescriptionTime = ListType + "Carga de datos por archivo ",
                    NumberTime = Convert.ToString(timer.Elapsed.TotalMilliseconds)
                };
                Singleton.Instance.OperationTime.Add(NewDescriptionTime);

                return RedirectToAction(nameof(Index));

            }
            return View();
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
                timer.Start();
                bool TypeListArt = false;
                string ListType = "";
                int IdNumber = 0;
                int NumberListCraft = Singleton.Instance.player_list.GetCount;
                int NumberListC = Singleton.Instance.PlayerList.Count;

                if (NumberListCraft != 0)
                {
                    TypeListArt = true;
                    if (NumberListCraft == 1 && Singleton.Instance.player_list.First().Id == 0)
                    {
                        IdNumber = 1;
                        ListType = "Lista Artesanal. ";
                        Singleton.Instance.player_list.ExtractAtStart();
                    }
                    else
                    {
                        IdNumber = NumberListCraft + 1;
                    }

                }

                if (NumberListC != 0)
                {
                    if (NumberListC == 1 && Singleton.Instance.PlayerList.First().Id == 0)
                    {
                        IdNumber = 1;
                        ListType = "Lista C#. ";
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

                //MEDIR TIEMPO Y GUARDARLO
                timer.Stop();
                var NewDescriptionTime = new Models.TimeDescription
                {
                    DescriptionTime = ListType +  "Carga de datos de forma manual ",
                    NumberTime = Convert.ToString(timer.Elapsed.TotalMilliseconds)
                };
                Singleton.Instance.OperationTime.Add(NewDescriptionTime);

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
                int pos = 0;
                bool Finding = false;
                while (Finding == false)
                {
                    if (Convert.ToInt32(Singleton.Instance.player_list.ElementAt(pos).Id) == id)
                    {
                        Finding = true;
                    }
                    else
                    {
                        pos++;
                    }
                }
                var EditPlayer = Singleton.Instance.player_list.ElementAt(pos);
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
                timer.Start();
                int EditId;
                int pos = 0;
                string TypeList = "";
                string EditName, EditLastName, EditPosition;
                if (Singleton.Instance.PlayerList.Count != 0)
                {
                    EditId = Convert.ToInt32(Singleton.Instance.PlayerList.Find(x => x.Id == id).Id);
                    EditName = Convert.ToString(Singleton.Instance.PlayerList.Find(x => x.Id == id).Name);
                    EditLastName = Convert.ToString(Singleton.Instance.PlayerList.Find(x => x.Id == id).LastName);
                    EditPosition = Convert.ToString(Singleton.Instance.PlayerList.Find(x => x.Id == id).Position);
                    var DeletePlayerEdit = Singleton.Instance.PlayerList.Find(x => x.Id == id);
                    Singleton.Instance.PlayerList.Remove(DeletePlayerEdit);
                    TypeList = "Lista C#. ";
                }
                else
                {
                    TypeList = "Lista Artesanal. ";
                    bool Finding = false;
                    while (Finding == false)
                    {
                        if (Convert.ToInt32(Singleton.Instance.player_list.ElementAt(pos).Id) == id)
                        {
                            Finding = true;
                        }
                        else
                        {
                            pos++;
                        }
                    }

                    EditId = Convert.ToInt32(Singleton.Instance.player_list.ElementAt(pos).Id);
                    EditName = Convert.ToString(Singleton.Instance.player_list.ElementAt(pos).Name);
                    EditLastName = Convert.ToString(Singleton.Instance.player_list.ElementAt(pos).LastName);
                    EditPosition = Convert.ToString(Singleton.Instance.player_list.ElementAt(pos).Position);

                    if (id == Singleton.Instance.player_list.First().Id)
                    {
                        Singleton.Instance.player_list.ExtractAtStart();
                    }
                    else if (id == Singleton.Instance.player_list.Last().Id)
                    {
                        Singleton.Instance.player_list.ExtractAtEnd();
                    }
                    else
                    {
                        Singleton.Instance.player_list.ExtractAtPosition(pos);
                    }

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
                    Singleton.Instance.PlayerList.Insert(id - 1, EditPlayer);
                }
                else
                {
                    Singleton.Instance.player_list.InsertAtPosition(EditPlayer, pos);
                }


                //MEDIR TIEMPO Y GUARDARLO
                timer.Stop();
                var NewDescriptionTime = new Models.TimeDescription
                {
                    DescriptionTime = TypeList + "Editar jugador ",
                    NumberTime = Convert.ToString(timer.Elapsed.TotalMilliseconds)
                };
                Singleton.Instance.OperationTime.Add(NewDescriptionTime);

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
                int pos = 0;
                bool Finding = false;
                while(Finding==false)
                {
                    if (Convert.ToInt32(Singleton.Instance.player_list.ElementAt(pos).Id) == id)
                    {
                        Finding = true;
                    }
                    else
                    {
                        pos++;
                    }
                }
                var DeletedPlayer = Singleton.Instance.player_list.ElementAt(pos);
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
                timer.Start();
                string TypeList = "";
                if (Singleton.Instance.PlayerList.Count != 0)
                {
                    var DeletedPlayer = Singleton.Instance.PlayerList.Find(x => x.Id == id);
                    Singleton.Instance.PlayerList.Remove(DeletedPlayer);
                    TypeList = "Lista C#. ";
                }
                else
                {
                    int pos = 0;
                    bool Finding = false;
                    TypeList = "Lista Artesanal. ";
                    while (Finding == false)
                    {
                        if (Convert.ToInt32(Singleton.Instance.player_list.ElementAt(pos).Id) == id)
                        {
                            Finding = true;
                        }
                        else
                        {
                            pos++;
                        }
                    }
                    var DeletedPlayer = Singleton.Instance.player_list.ElementAt(pos);
                    Singleton.Instance.player_list.ExtractAtPosition(pos);
                }


                //MEDIR TIEMPO Y GUARDARLO
                timer.Stop();
                var NewDescriptionTime = new Models.TimeDescription
                {
                    DescriptionTime = TypeList + "Eliminar jugador ",
                    NumberTime = Convert.ToString(timer.Elapsed.TotalMilliseconds)
                };
                Singleton.Instance.OperationTime.Add(NewDescriptionTime);

                //MOSTRAR VISTA
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Searching(string information)
        {
            timer.Start();
            if (Singleton.Instance.PlayerList.Count != 0)
            {
                ClassSearch.Looking(information, Singleton.Instance.PlayerList.Count, Singleton.Instance.PlayerList, x => x.Name == information);
                ClassSearch.Looking(information, Singleton.Instance.PlayerList.Count, Singleton.Instance.PlayerList, x => x.Position == information);
                ClassSearch.Looking(information, Singleton.Instance.PlayerList.Count, Singleton.Instance.PlayerList, x => x.LastName == information);
                ClassSearch.Looking(information, Singleton.Instance.PlayerList.Count, Singleton.Instance.PlayerList, x => x.Club == information);
            }
            else
            {
                ClassSearch.Looking(information, Singleton.Instance.player_list.Count(), Singleton.Instance.player_list, x => x.Name == information);
                ClassSearch.Looking(information, Singleton.Instance.player_list.Count(), Singleton.Instance.player_list, x => x.Position == information);
                ClassSearch.Looking(information, Singleton.Instance.player_list.Count(), Singleton.Instance.player_list, x => x.LastName == information);
                ClassSearch.Looking(information, Singleton.Instance.player_list.Count(), Singleton.Instance.player_list, x => x.Club == information);
            }

            //MEDIR TIEMPO Y GUARDARLO
            timer.Stop();
            var NewDescriptionTime = new Models.TimeDescription
            {
                DescriptionTime = "Busqueda " + information + " ",
                NumberTime = Convert.ToString(timer.Elapsed.TotalMilliseconds)
            };
            Singleton.Instance.OperationTime.Add(NewDescriptionTime);

            return View();
        }

        public ActionResult CraftList()
        {
            if(Singleton.Instance.player_list.Count() == 0 && Singleton.Instance.PlayerList.Count() == 0)
            {
                var newPlayer = new Models.Players { };
                Singleton.Instance.player_list.InsertAtEnd(newPlayer);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        public ActionResult ListC()
        {
            if(Singleton.Instance.PlayerList.Count() == 0 && Singleton.Instance.player_list.Count() == 0)
            {
                var newPlayer = new Models.Players { };
                Singleton.Instance.PlayerList.Add(newPlayer);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
            
        }



        public ActionResult Download()
        {
            string text = "";
            for (int i = 0; i < Singleton.Instance.OperationTime.Count; i++)
            {
                string NameDescription = Singleton.Instance.OperationTime.ElementAt(i).DescriptionTime;
                string timeDescription = Singleton.Instance.OperationTime.ElementAt(i).NumberTime;
                text += NameDescription + ", tiempo: " + timeDescription +" ms"+ "\n";
            }

            StreamWriter writer = new StreamWriter("Log_File.txt");
            writer.Write(text);
            writer.Close();
            return RedirectToAction(nameof(Index));
        }


    }
}
