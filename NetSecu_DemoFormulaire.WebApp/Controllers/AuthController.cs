using Microsoft.AspNetCore.Mvc;
using NetSecu_DemoFormulaire.Models.Domain;
using NetSecu_DemoFormulaire.Models.Entities;
using NetSecu_DemoFormulaire.WebApp.Models.Forms;
using Tools;

namespace NetSecu_DemoFormulaire.WebApp.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Register");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterForm form)
        {
            if(!ModelState.IsValid)
            {
                return View(form);
            }

            SampleDbContext sampleDbContext = new SampleDbContext();
            sampleDbContext.Add(new Utilisateur() { Nom = form.Nom, Prenom = form.Prenom, Email = form.Email, Passwd = form.Passwd });
            sampleDbContext.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginForm form)
        {
            if (!ModelState.IsValid)
            {
                return View(form);
            }

            SampleDbContext sampleDbContext = new SampleDbContext();
            Utilisateur? utilisateur = sampleDbContext.Utilisateurs.SingleOrDefault(u => u.Email == form.Email);

            if(utilisateur is null || utilisateur.Passwd != form.Passwd.Hash().ToBase64String())
            {
                ModelState.AddModelError("", "Erreur Email / Mot de passe");
                return View(form);
            }
           
            ViewBag.Message = "Félicitation, vous êtes connecté!";
            return View();
            
        }
    }
}
