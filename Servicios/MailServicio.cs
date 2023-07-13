using System.Net.Mail;
using System.Net;
using Dominio;

namespace Servicios
{
    public class MailServicio
    {
        private static SmtpClient client;
        private static MailMessage mensaje;
        private static string baseHtml = @"
            <head>
                <style>
                  body {
                      background-color: lightsteelblue;
                      height: 100%;
                  }
              </style>
            </head>
            <h1 style=""text-align: center;"">##TITULO##</h1>
            <h3 style=""text-align: center;""><img src=""https://www.dropbox.com/scl/fi/qtaj9u02iabtieohqam20/ClinicaIcono.png?rlkey=o61y53rl5ijxem8e7ulhktf2f&amp;dl=1"" alt=""iconoClinica"" width=""200"" height=""200"" /></h3>
            ##CUERPO##
            <p></p>
            <hr />
            <p style=""color: #566B80;"">© 2023 Clínica. Todos los derechos reservados </p>
            <p style=""color: #566B80;"">Nunca te pediremos tu clave o datos de tu cuenta por e-mail.</p>
            <p style=""color: #566b80;"">Dirigete a nuestra <a href=""123"" target=""_blank"" rel=""noopener"">Página Web</a> para continuar disfrutando de nuestros servicios</p>
        ";
        private static string tituloRegistroUsuario = @"Registrado con exito!";
        private static string cuerpoRegistroUsuario = @"
            <h4 style=""text-align: justify;"">Bienvenido/a a la Clínica Sr/a ##NOMBRE##</h4>
            <h5 style=""text-align: justify;"">Acercate a nuestra sucursal más cercana para gozar de todos nuestros servicios con los profesionales más experimentados.</h5>
        ";

        static MailServicio()
        {
            client = new SmtpClient("sandbox.smtp.mailtrap.io", 2525)
            {
                Credentials = new NetworkCredential("d2683a1c4236a3", "b31776e7e17316"),
                EnableSsl = true,
            };
            mensaje = new MailMessage()
            {
                From = new MailAddress("noresponder@clinica.com"),
                IsBodyHtml = true
            };
        }

        public static void EnviarMail(Usuario usuario, TipoMail tipoMail)
        {
            mensaje.To.Clear();
            mensaje.To.Add(usuario.Email);

            SetearBodySubject(tipoMail, usuario);

            client.Send(mensaje);
        }

        private static void SetearBodySubject(TipoMail tipoMail, Usuario usuario)
        {
            switch (tipoMail)
            {
                case TipoMail.RegistroUsuario:
                    mensaje.Body = baseHtml.Replace("##TITULO##", tituloRegistroUsuario).Replace("##CUERPO##", cuerpoRegistroUsuario).Replace("##NOMBRE##",usuario.ToString());
                    mensaje.Subject = tituloRegistroUsuario;
                    break;
            }
        }
    }
}
