using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace WebApplication1.Models
{
    public class UsersTable : SqlExpressUtilities.SqlExpressWrapper
    {
        public long ID { get; set; }

        [StringLength(50)]
        [Display(Name = "Prenom")]
        [Required(ErrorMessage = "Entrez votre prenom")]
        public String Prenom { get; set; }

        [StringLength(50)]
        [Display(Name = "Nom")]
        [Required(ErrorMessage = "Entrez votre nom")]
        public String Nom { get; set; }

        [StringLength(30)]
        [Display(Name = "Nom d'utilisateur")]
        [Required(ErrorMessage = "Entrez un pseudo")]
        public String UserName { get; set; }

        [StringLength(30)]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe")]
        [Required(ErrorMessage = "Entrez un mot de passe")]
        public String Password { get; set; }

        [StringLength(50)]
        [Display(Name = "Adresse")]
        [Required(ErrorMessage = "Entrez votre adresse à votre porte")]
        public String Address { get; set; }

        [StringLength(50)]
        [Display(Name = "Ville et province")]
        [Required(ErrorMessage = "Entrez votre nom")]
        public String CityProvince { get; set; }

        [StringLength(6)]
        [Display(Name = "Code postal")]
        [Required(ErrorMessage = "Entrez votre code postal")]
        public String PostalCode { get; set; }

        [StringLength(20)]
        [Display(Name = "Numero de carte de credit")]
        /*[RegularExpression(@"^(?:4[0-9]{12}(?:[0-9]{3})?|5[1-5][0-9]{14}|6(?:011|5[0-9][0-9])[0-9]{12}|3[47][0-9]{13}|3(?:0[0-5]|[68][0-9])[0-9]{11}|(?:2131|1800|35\d{3})\d{11})$", ErrorMessage = "Carte de crédit invalide. Entrez 16 nombres sans les espaces.")]*/
        [Required(ErrorMessage = "Entrez votre numero de carte de crédit")]
        public String CreditCard { get; set; }

        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Adresse email invalide")]
        [Required(ErrorMessage = "Entrez votre adresse email")]
        [Display(Name = "Courriel")]
        public String Email { get; set; }

        public UsersTable()
            : base("")
        {
        }

        public UsersTable(Object connexionString)
            : base(connexionString)
        {
            SQLTableName = "USERS";
        }
        public override void GetValues()
        {
            ID = long.Parse(FieldsValues[0]);
            Prenom = FieldsValues[1];
            Nom = FieldsValues[2];
            UserName = FieldsValues[3];
            Password = FieldsValues[4];
            Address = FieldsValues[5];
            CityProvince = FieldsValues[6];
            PostalCode = FieldsValues[7];
            CreditCard = FieldsValues[8];
            Email = FieldsValues[9];
        }

        public override void Insert()
        {
            InsertRecord(Prenom, Nom, UserName, Password, Address, CityProvince, PostalCode, CreditCard, Email);
        }

        public override void Update()
        {
            UpdateRecord(ID, Prenom, Nom, UserName, Password, Address, CityProvince, PostalCode, CreditCard, Email);
        }

        public void UpdateUserInfoWhereID(long ID, String Password, String Address,
            String CityProvince, String PostalCode, String CreditCard, String Email)
        {
            UpdateRecord(ID, Password, Address, CityProvince, PostalCode, CreditCard, Email);
        }

        // Des fonctions qui nous donne un boolean pour savoir si ce qu'on veut est là.
        public bool UserNameDoesExist(String userName)  // Exists alreay?
        {
            if (userName == null || userName.Length == 0)
            {
                return false;
            }

            QuerySQL("SELECT * FROM " + SQLTableName + " WHERE USERNAME = '" + userName + "'");
            return reader.HasRows;
        }
        public bool CheckPasswordForUser(String userName, String password)  // Password matches with userName
        {
            if (userName == null ||  userName.Length == 0)
            {
                return false;
            }

            if (password == null ||  password.Length == 0)
            {
                return false;
            }

            QuerySQL("SELECT * FROM " + SQLTableName + " WHERE USERNAME = '" + userName + "' AND PASSWORD='" + password + "'");
            return reader.HasRows;
        }
        // Fonction a effacer si pas utile
        public bool Exist(String userName)
        {
            bool exist = false;
            QuerySQL("SELECT * FROM " + SQLTableName + " WHERE USERNAME='" + userName + "'");
            if (reader.HasRows)
            {
                Next();
                GetValues();
                exist = true;
            }
            return exist;
        }

        // Fonction a effacer si pas utile
        public bool Valid(String userName, String Password)
        {
            bool valid = false;
           if (Exist(userName))
                valid = (this.Password == Password);
            return valid;
        }
    }
}
