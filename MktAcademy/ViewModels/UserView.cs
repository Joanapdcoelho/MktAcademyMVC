using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Permissions;
using System.Web;

namespace MktAcademy.ViewModels
{
    public class UserView
    {
        //Isto são só modelos porque as tabelas já lá estão criadas
        public string UserID { get; set; }

        public string UserName { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public List<RoleView> Roles { get; set; }

        public RoleView Role { get; set; }


    }
}