﻿using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Clinic
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegistrarse_Click(object sender, EventArgs e)
        {
            Response.Redirect("RegistroUsuario.aspx");
        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            var usuario = UsuarioNegocio.Login(txtEmail.Text, txtPassword.Text);
            if(usuario != null)
            {
                Session.Add("Usuario", usuario);
                Response.Redirect("Default.aspx");
            }
        }
    }
}