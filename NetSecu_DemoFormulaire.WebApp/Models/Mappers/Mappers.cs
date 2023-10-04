using NetSecu_DemoFormulaire.Models.Entities;
using System.Data;

namespace NetSecu_DemoFormulaire.WebApp.Models.Mappers
{
    internal static class Mappers
    {
        internal static Utilisateur ToUtilisateur(this IDataRecord record)
        {
            return new Utilisateur()
            {
                Id = (Guid)record["Id"],
                Nom = (string)record["Nom"],
                Prenom = (string)record["Prenom"],
                Email = (string)record["Email"]
            };
        }
    }
}
