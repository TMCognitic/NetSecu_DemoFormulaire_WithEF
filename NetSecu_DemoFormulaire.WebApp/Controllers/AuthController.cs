using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using NetSecu_DemoFormulaire.Models.Domain;
using NetSecu_DemoFormulaire.Models.Entities;
using NetSecu_DemoFormulaire.WebApp.Models.Forms;
using NetSecu_DemoFormulaire.WebApp.Models.Mappers;
using Tools;
using Tools.Database;

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

            #region CODE EF
            //SampleDbContext sampleDbContext = new SampleDbContext();
            //sampleDbContext.Add(new Utilisateur() { Nom = form.Nom, Prenom = form.Prenom, Email = form.Email, Passwd = form.Passwd });
            //sampleDbContext.SaveChanges();
            #endregion

            #region CODE ADO
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DemoFormulaire;Integrated Security=True;Encrypt=False";
                connection.Open();
                connection.ExecuteNonQuery("INSERT INTO Utilisateur (Nom, Prenom, Email, Passwd) VALUES (@Nom, @Prenom, @Email, @Passwd)", parameters: new { form.Nom, form.Prenom, form.Email, Passwd = form.Passwd.Hash() });
            }
            #endregion

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

            #region CODE EF
            //SampleDbContext sampleDbContext = new SampleDbContext();
            //Utilisateur? utilisateur = sampleDbContext.Utilisateurs.SingleOrDefault(u => u.Email == form.Email);

            //if (utilisateur is null || utilisateur.Passwd != form.Passwd.Hash().ToBase64String())
            //{
            //    ModelState.AddModelError("", "Erreur Email / Mot de passe");
            //    return View(form);
            //}
            #endregion

            #region CODE ADO
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DemoFormulaire;Integrated Security=True;Encrypt=False";
                connection.Open();
                Utilisateur? utilisateur = connection.ExecuteReader("SELECT Id, Nom, Prenom, Email FROM Utilisateur WHERE Email = @Email AND Passwd = @Passwd;", dr => dr.ToUtilisateur(), parameters: new { form.Email, Passwd = form.Passwd.Hash() }).SingleOrDefault();

                if (utilisateur is null)
                {
                    ModelState.AddModelError("", "Erreur Email / Mot de passe");
                    return View(form);
                }
            }
            #endregion

            ViewBag.Message = "Félicitation, vous êtes connecté!";
            return View();
            
        }
    }
}
