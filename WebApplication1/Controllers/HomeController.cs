using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SqlExpressUtilities;
using WebApplication1.Models;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Controllers
{
	public class HomeController : Controller
	{
		/*public ActionResult Index()
		{
			return View();
		}*/

        public ActionResult Index(string id)
        {
            if (id != null && id != "")
            {
                // On a reçu le id d'un utilisateur

                PurchaseTable purs = new PurchaseTable(Session["MainDB"]);
                purs.SelectWhere("ID", id); // Où il y a le id de l'utilisateur passé en paramètre

                List<PurchaseTable> purSelect = new List<PurchaseTable>();

                while (purs.Next())
                {
                    // On ramasse les id de tous les items qu'il a acheté
                    PurchaseTable temp = new PurchaseTable();
                    temp.ItemID = purs.ItemID;
                    purSelect.Add(purs);
                }

                /* MAINTENANT, ON VA CHERCHER CES ITEMS */

                ItemsTable items = new ItemsTable(Session["MainDB"]);
                List<ItemsTable> itemsSelect = new List<ItemsTable>();

                // On passe dans tous les id qu'on a récolté auparavant. ce sont les choses que l'utilisateur a achetées. 
                for (int i = 0; i < purSelect.Count(); i++)
                {
                    // on prend ce id (qui represente un item)
                    long newId = purSelect.ElementAt(i).UserID;

                    // et on fait un select where pour trouver cet item
                    items.SelectWhere("ID", newId.ToString());

                    // juste un 'if', ça ferait la job
                    while (items.Next())
                    {
                        ItemsTable temp = new ItemsTable();
                        temp.ID = items.ID;
                        temp.Summary = items.Summary;
                        temp.Price = items.Price;
                        temp.Description = items.Description;
                        temp.PicturePath = items.PicturePath;
                        itemsSelect.Add(temp);
                    }
                }

                // On a de quoi à retourner
                return View(itemsSelect);
            }
            else
            { 
                // on rammasse n'importe quel item. Ok, on veut des baies. 
                string[] strings = {"baie", "framboise", "bleuet" };

                ItemsTable items = new ItemsTable(Session["MainDB"]);

                items.SelectFromStringies(strings);

                List<ItemsTable> itemsSelect = new List<ItemsTable>();

                while (items.Next())
                {
                    ItemsTable temp = new ItemsTable();
                    temp.ID = items.ID;
                    temp.Summary = items.Summary;
                    temp.Price = items.Price;
                    temp.Description = items.Description;
                    temp.PicturePath = items.PicturePath;
                    itemsSelect.Add(temp);
                }

                return View(itemsSelect);
            }

        }
        [HttpPost]
        public ActionResult Index(string BTN_Submit,string temp)
        {
            if (BTN_Submit != null)
            {
                Session["itemId"] = BTN_Submit;
                return RedirectToAction("Achat");
            }
            return Redirect("Index");
        }
		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UsersTable loguser)
        {

            UsersTable users = new UsersTable(Session["MainDB"]);
            //if (users.UserNameDoesExist(loguser.UserName))
            //if (users.Exist(loguser.UserName))
            //{
                //if (users.SelectByFieldName("USERNAME", loguser.UserName))
                // J'utilise ma méthode Valid
                if (users.Valid(loguser.UserName, loguser.Password))
                {
                    //if (users.CheckPasswordForUser(loguser.UserName, loguser.Password))
                   //{
                        // On set le ID pour cette session. 
                        Session["PresentUserID"] = users.ID;  // Ça part quand on ferme le browser. 
                        ViewBag.Error = ""; // On enlève les erreurs s'il y en a
                        return RedirectToAction("Index");
                    //}
                    
                }
                else
                    {
                        // Le mot passe est pas bon
                        ViewBag.Error = "Mot de passe ou nom d'usager invalide";
                    }
            //}
            //else
            //{
                // Le username n'existe pas
            //    ViewBag.Error = "Nom d'usager invalide";
            //}
            return View();
        }

        /*public ActionResult AjouterTest()
        {
            PersonnesTable personnes = new PersonnesTable(Session["MainDB"]);
            personnes.SelectAll();

            List<PersonnesTable> personnesSelect = new List<PersonnesTable>();

            while (personnes.Next())
            {
                PersonnesTable temp = new PersonnesTable();
                temp.ID = personnes.ID;
                temp.Prenom = personnes.Prenom;
                temp.Nom = personnes.Nom;
                temp.Telephone = personnes.Telephone;
                temp.Naissance = personnes.Naissance;
                temp.CodePostal = personnes.CodePostal;
                temp.Sexe = personnes.Sexe;
                temp.EtatCivil = personnes.EtatCivil;
                temp.Avatar = personnes.Avatar;
                personnesSelect.Add(temp);
            }

            if (!ModelState.IsValid)
            {
                return View();
            }

            return View();
        }*/

        [HttpGet]
        public ActionResult Items()
        {
            ItemsTable items = new ItemsTable(Session["MainDB"]);
            items.SelectAll();

            List<ItemsTable> itemsSelect = new List<ItemsTable>();

            while (items.Next())
            {
                ItemsTable temp = new ItemsTable();
                temp.ID = items.ID;
                temp.Summary = items.Summary;
                temp.Price = items.Price;
                temp.Description = items.Description;
                temp.PicturePath = items.PicturePath;
                itemsSelect.Add(temp);
            }

            return View(itemsSelect);
        }

        [HttpGet]
        public ActionResult Inscription()
        {
            return View();
        }

       /* [HttpGet]
        public ActionResult Inscription()
        {
            UsersTable users = new UsersTable(Session["MainDB"]);
            users.SelectAll();

            List<UsersTable> usersSelect = new List<UsersTable>();

            while (users.Next())
            {
                UsersTable temp = new UsersTable();
                temp.ID = users.ID;
                temp.Prenom = users.Prenom;
                temp.Nom = users.Nom;
                temp.UserName = users.UserName;
                temp.Password = users.Password;
                temp.Address = users.Address;
                temp.CityProvince = users.CityProvince;
                temp.PostalCode = users.PostalCode;
                temp.CreditCard = users.CreditCard;
                temp.Email = users.Email;
                usersSelect.Add(temp);
            }

            return View();
        }*/

        [HttpPost]
        public ActionResult Inscription(UsersTable newusers)
        {
            UsersTable users = new UsersTable(Session["MainDB"]);

            // Model State je sais pas c'est quoi. je vais le laisser là.
            if (ModelState.IsValid)
            {
                users.Prenom = newusers.Prenom;
                users.Nom = newusers.Nom;
                users.UserName = newusers.UserName;
                if (users.UserNameDoesExist(newusers.UserName))
                    return View();
                users.Password = newusers.Password;
                users.Address = newusers.Address;
                users.CityProvince = newusers.CityProvince;
                users.PostalCode = newusers.PostalCode;
                users.CreditCard = newusers.CreditCard;
                users.Email = newusers.Email;
                users.Insert(); // Ça devrait marcher
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Search(String name, string BTN_Submit)
        {
/**                       Session["Place"] = 0;
            // C'est le bouton pour acheter des baies
            if (BTN_Submit != null && BTN_Submit.ToString() != "NEXT" && BTN_Submit.ToString() == "PREV")
            {
                Session["itemId"] = BTN_Submit;
                return RedirectToAction("Achat");
            }
            else if (BTN_Submit != null && BTN_Submit.ToString() == "NEXT")
            {
                int place = Convert.ToInt32( Session["Place"].ToString());
                Session["Place"] = place + 20;
                return RedirectToAction("Search", Session["LastSearch"]);
            }
            else if (BTN_Submit != null && BTN_Submit.ToString() == "PREV")
            {
                int place = Convert.ToInt32(Session["Place"].ToString());

                if (place >= 20)
                {
                    Session["Place"] = place - 20;
                }
                return RedirectToAction("Search", Session["LastSearch"]);
            }
            else
            {
                */



            // C'est le bouton pour acheter des baies
            if (BTN_Submit != null)
            {
                Session["itemId"] = BTN_Submit;
                return RedirectToAction("Achat");
            }
            try
            {
                string[] strings = name.Split(' ');

                ItemsTable items = new ItemsTable(Session["MainDB"]);

                items.SelectFromStringies(strings);

                // items.SelectAll();

                List<ItemsTable> itemsSelect = new List<ItemsTable>();

                while (items.Next())
                {
                    ItemsTable temp = new ItemsTable();
                    temp.ID = items.ID;
                    temp.Summary = items.Summary;
                    temp.Price = items.Price;
                    temp.Description = items.Description;
                    temp.PicturePath = items.PicturePath;
                    itemsSelect.Add(temp);
                }

                return View(itemsSelect);
            }
            catch (Exception ex)
            {
                String Message = ex.Message;
                return RedirectToAction("Index");
            }
        }
        /*[HttpPost]
        public ActionResult Search(String BTN_Submit,string temp)
        {
            if (BTN_Submit != null)
            {
                Session["itemId"] = BTN_Submit;
                return RedirectToAction("Achat");
            }
            return RedirectToAction("Search");
        }*/
        [HttpGet]
        public ActionResult Achat(String idItem)
        {
            if (Session["itemId"] != null)
                idItem = Session["itemId"].ToString();
            else
                idItem = "0";
            // Pour l'instant, je vais mettre l'id à 0, pour au-moins sélectionner un item
            ItemsTable item = new ItemsTable(Session["MainDB"]);
            item.SelectByID(idItem);

            // Trouver les comentaire pour cette item
            CommentsTable comment = new CommentsTable(Session["MainDB"]);
            comment.QuerySQL("SELECT * from COMMENTS where ITEMID=" + item.ID);
            while (comment.Next())
            {
                UsersTable userComment = new UsersTable(Session["MainDB"]);
                comment.GetValues();

                userComment.QuerySQL("SELECT * from USERS where ID=" + comment.UserID);
                userComment.Next();
                userComment.GetValues();
                ViewData["Comments"] += userComment.Prenom + " " + userComment.Nom + ": " + comment.Comment.ToString() + ";";
            }

            return View(item);
        }
        [HttpPost]
        public ActionResult Achat(String BTN_Submit, PurchaseTable purchaseTable, String Commentaire)
        {
            // Si itemID est null, redirect a l'index
            if(Session["itemId"] == null)
                return RedirectToAction("index");
            // Vérifier aussi si la quantité est supérieur a 0
            if(purchaseTable.Quantity <= 0 || purchaseTable.Quantity == null)
                return RedirectToAction("Achat");// On le redirect a l'achat

            // Les variables pour le user et item ID
            long itemID = long.Parse(Session["itemId"].ToString());
            // Si le userID est null, donc pas login, revoyer vers le login
            if (Session["PresentUserID"] == null)
            {
                return RedirectToAction("Login");
            }
            long userID = long.Parse(Session["PresentUserID"].ToString());

            // Pour l'instant, je vais mettre l'id à 0, pour au-moins sélectionner un item
            ItemsTable item = new ItemsTable(Session["MainDB"]);
            item.SelectByID(itemID.ToString());

            // Ajouter a la table PURCHASES l'enregistrement
            if (BTN_Submit == "Acheter")
            {
                PurchaseTable purchase = new PurchaseTable(Session["MainDB"]);
                purchase.ItemID = itemID;
                purchase.Date = DateTime.Now;
                purchase.Quantity = purchaseTable.Quantity;
                purchase.UserID = userID;
                purchase.Insert();
                // Voir l'achat dans notre panier
                return RedirectToAction("HistoriqueAchat");
            }
            else if (BTN_Submit == "Soumettre le commentaire")
            {
                // si le commentaire est trop gros...
                if(Commentaire.Length > 199)
                    return RedirectToAction("Achat");

                CommentsTable comment = new CommentsTable(Session["MainDB"]);
                comment.ItemID = itemID;
                comment.Comment = Commentaire;
                comment.UserID = userID;
                comment.Insert();
                return RedirectToAction("Achat");
            }
            return View(item);
        }
        public ActionResult OptionUtilisateur()
        {
            UsersTable users = new UsersTable(Session["MainDB"]);
            users.SelectByID(Session["PresentUserID"].ToString());
            users.Next();
            return View(users);
        }
        [HttpPost]
        public ActionResult OptionUtilisateur(string BTN_Submit, UsersTable updateUser)
        {
            // Sa marche pas...pcq y manque les champs Nom,Prenom et username
            // faque sa met ModelState.IsValid a false directement

            // Loader le premier user car C'est lui qui a notre ID qu'on veut
            UsersTable loadusers = new UsersTable(Session["MainDB"]);
            loadusers.SelectByID(Session["PresentUserID"].ToString());
            loadusers.Next();

            UsersTable users = new UsersTable(Session["MainDB"]);
            users.SelectByID(Session["PresentUserID"].ToString());
            users.GetValues();
            // Modifier l'usager

            // Fausse table cheapette
            UsersTable usersfortemp = new UsersTable(Session["MainDB"]);
            usersfortemp.SelectAll();
            // Une liste qui sert à essayer de faire un insertion
            List<UsersTable> UsersTableSelect = new List<UsersTable>();

            try
            {

                while (usersfortemp.Next())
                {
                    UsersTable temp = new UsersTable();
                    temp.Prenom = usersfortemp.Prenom;
                    temp.Nom = usersfortemp.Nom;
                    temp.UserName = usersfortemp.UserName;

                    temp.Password = usersfortemp.Password;
                    temp.Address = usersfortemp.Address;
                    temp.CityProvince = usersfortemp.CityProvince;
                    temp.PostalCode = usersfortemp.PostalCode;
                    temp.CreditCard = usersfortemp.CreditCard;
                    temp.Email = usersfortemp.Email;

                    UsersTableSelect.Add(temp);
                }

                if (BTN_Submit == "Update")
                {
                    users.ID = long.Parse(Session["PresentUserID"].ToString());

                    // == DÉBUG: Autre table
                    users.Prenom = loadusers.Prenom;
                    users.Nom = loadusers.Nom;
                    users.UserName = loadusers.UserName;
                    // == FIN: Autre table

                    users.Password = updateUser.Password;
                    users.Address = updateUser.Address;
                    users.CityProvince = updateUser.CityProvince;
                    users.PostalCode = updateUser.PostalCode;
                    users.CreditCard = updateUser.CreditCard;
                    users.Email = updateUser.Email;
                    users.Update(); // ESSAYER VOIR SI ÇA MARCHE
                }
            }
            catch (Exception ex)
            {
                //return View(usersfortemp);
                return View(/*usersfortemp*/loadusers);
            }
            // Retourner a l'index...
            return RedirectToAction("Index");
        }
        public ActionResult HistoriqueAchat()
        {
            long PrixTotal = 0;
            PurchaseTable purchase = new PurchaseTable(Session["MainDB"]);
            purchase.SelectByFieldName("USERID", Session["PresentUserID"]);
            List<ItemsTable> itemsSelect = new List<ItemsTable>();
            // juste pour être sur
            if (purchase.UserID == long.Parse(Session["PresentUserID"].ToString()))
            {           
                do
                {
                    
                    ItemsTable temp = new ItemsTable(Session["MainDB"]);
                    temp.SelectByID(purchase.ItemID.ToString());
                    itemsSelect.Add(temp);
                    ViewData["NombreBais"] += " Quantité: " + purchase.Quantity + " Prix: " + purchase.Quantity * temp.Price + "$;";
                    PrixTotal += purchase.Quantity * temp.Price;
                } while (purchase.Next());
            }
            ViewData["PrixTotal"] = PrixTotal;
            return View(itemsSelect);
        }          
       /*  [HttpPost]
        public ActionResult Inscription(PersonnesTable nouvellePersonne)
        {
            UsersTable users = new UsersTable(Session["MainDB"]);
            if (ModelState.IsValid)
            {

            }
            return View();
        }*/

       /* [HttpPost]
        public ActionResult Inscription(PersonnesTable nouvellePersonne)
        {
            UsersTable users = new UsersTable(Session["MainDB"]);
            if (ModelState.IsValid)
            {

            }
            return View();
        }*/

      //  [HttpPost]
        /*public ActionResult AjouterTest(PersonnesTable nouvellePersonne)
        {
            PersonnesTable personnes = new PersonnesTable(Session["MainDB"]);
            if (ModelState.IsValid)
            {

            }
            return View();
        }*/


        /*public ActionResult Lister()
        {
			  PersonnesTable personnesModel = new PersonnesTable(Session["MainDB"]);
			  personnesModel.SelectAll();
			  return View(personnesModel);
        }*/

        public ActionResult Modifier(long id = 0)
        {
            UsersTable users = new UsersTable(Session["MainDB"]);
            users.SelectByID(id.ToString());

            return View(users);
        }


	}
}