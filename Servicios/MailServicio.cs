using System.Net.Mail;
using System.Net;
using Dominio;
using System.Globalization;

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
            <h4 style=""text-align: justify;"">Bienvenido/a a la Clínica Sr/a ##NOMBRE##!</h4>
            <h5 style=""text-align: justify;"">Acercate a nuestra sucursal más cercana para gozar de todos nuestros servicios con los profesionales más experimentados.</h5>
        ";
        private static string tituloModificacionUsuario = @"Modificacion con exito!";
        private static string cuerpoModificacionUsuario = @"
            <h4 style=""text-align: justify;"">Sr/a ##NOMBRE## se han realizado cambios en su perfil.</h4>
            <h5 style=""text-align: justify;"">En caso no haber sido usted quien realizó los cambios o de no haberlos solicitado a la Recepcionista comuniquese con la Clínica para solucionar la situación.</h5>
        ";
        private static string tituloReasignacionTurno = @"El turno ha sido reasignado con exito!";
        private static string cuerpoReasignacionTurno = @"
            <h4 style=""text-align: justify;"">El turno del Paciente ##PACIENTE## y Doctor/a ##DOCTOR## fue reasignado para la siguiente fecha, ##FECHA## a las ##HORA##hs.</h4>
            <h5 style=""text-align: justify;"">En caso no haber sido usted quien realizó los cambios o de no haberlos solicitado a la Recepcionista o al Doctor comuniquese con la Clínica para solucionar la situación.</h5>
        "; 
        private static string tituloAsignacionTurno = @"Nuevo turno asignado!";
        private static string cuerpoAsignacionTurno = @"
            <h4 style=""text-align: justify;"">En este turno se va a atender el/la Sr/a ##PACIENTE## con el/la Doctor/a ##DOCTOR## en la especialidad de ##ESPECIALIDAD## el día ##FECHA## a las ##HORA##hs.</h4>
            <h5 style=""text-align: justify;"">El motivo del turno es:</h5>
            <h5 style=""text-align: justify;"">##CAUSAS##</h5>
        ";
        private static string tituloObservacionesTurno = @"Su turno tiene nuevas observaciones!";
        private static string cuerpoObservacionesTurno = @"
            <h4 style=""text-align: justify;"">Sr/a ##PACIENTE## a su turno con el/la Doctor/a ##DOCTOR## del día ##FECHA## a las ##HORA##hs se le han agregado las siguientes observaiones:</h4>
            <h5 style=""text-align: justify;"">##OBSERVACIONES##</h5>
        "; 
        private static string tituloCancelacionTurno = @"Se ha cancelado el turno!";
        private static string cuerpoCancelacionTurno = @"
            <h4 style=""text-align: justify;"">El turno del día ##FECHA## a las ##HORA##hs ha sido cancelado.</h4>
            <h4 style=""text-align: justify;"">Se le informa a el/la Sr/a ##PACIENTE## y a el/la Doctor/a ##DOCTOR## que alguna de las partes no podrá asistir al turno.</h4>
            <h5 style=""""text-align: justify;"""">En caso de que usted crea que esto ha de ser un error comuniquese con la Clínica para solucionar la situación.</h5>
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

        public static void EnviarMailUsuario(Usuario usuario, TipoMail tipoMail)
        {
            mensaje.To.Clear();

            SetearBodySubject(tipoMail, usuario:usuario);

            client.Send(mensaje);
        }
        public static void EnviarMailTurno(Turno turno, TipoMail tipoMail)
        {
            mensaje.To.Clear();

            SetearBodySubject(tipoMail, turno:turno);

            client.Send(mensaje);
        }

        private static void SetearBodySubject(TipoMail tipoMail, Usuario usuario = null, Turno turno = null)
        {
            switch (tipoMail)
            {
                case TipoMail.RegistroUsuario:
                    mensaje.To.Add(usuario.Email);
                    mensaje.Body = baseHtml.Replace("##TITULO##", tituloRegistroUsuario)
                                            .Replace("##CUERPO##", cuerpoRegistroUsuario)
                                            .Replace("##NOMBRE##",usuario.ToString());
                    mensaje.Subject = tituloRegistroUsuario;
                    break;
                case TipoMail.ModificacionUsuario:
                    mensaje.To.Add(usuario.Email);
                    mensaje.Body = baseHtml.Replace("##TITULO##", tituloModificacionUsuario)
                                            .Replace("##CUERPO##", cuerpoModificacionUsuario)
                                            .Replace("##NOMBRE##", usuario.ToString());
                    mensaje.Subject = tituloModificacionUsuario;
                    break;
                case TipoMail.ReasignacionTurno:
                    mensaje.To.Add(turno.Paciente.Email);
                    mensaje.To.Add(turno.Doctor.Email);
                    mensaje.Body = baseHtml.Replace("##TITULO##", tituloReasignacionTurno)
                                            .Replace("##CUERPO##", cuerpoReasignacionTurno)
                                            .Replace("##PACIENTE##", turno.Paciente.ToString())
                                            .Replace("##DOCTOR##", turno.Doctor.ToString())
                                            .Replace("##FECHA##", CultureInfo.CurrentCulture.TextInfo.ToTitleCase(turno.Horario.ToString("dddd dd/MM")))
                                            .Replace("##HORA##", turno.Horario.ToString("HH:mm"));
                    break;
                case TipoMail.ObservacionesTurno:
                    mensaje.To.Add(turno.Paciente.Email);
                    mensaje.Body = baseHtml.Replace("##TITULO##", tituloObservacionesTurno)
                                            .Replace("##CUERPO##", cuerpoObservacionesTurno)
                                            .Replace("##PACIENTE##", turno.Paciente.ToString())
                                            .Replace("##DOCTOR##", turno.Doctor.ToString())
                                            .Replace("##FECHA##", CultureInfo.CurrentCulture.TextInfo.ToTitleCase(turno.Horario.ToString("dddd dd/MM")))
                                            .Replace("##HORA##", turno.Horario.ToString("HH:mm"))
                                            .Replace("##OBSERVACIONES##", turno.Observaciones);
                    break;
                case TipoMail.AsignacionTurno:
                    mensaje.To.Add(turno.Paciente.Email);
                    mensaje.To.Add(turno.Doctor.Email);
                    mensaje.Body = baseHtml.Replace("##TITULO##", tituloAsignacionTurno)
                                            .Replace("##CUERPO##", cuerpoAsignacionTurno)
                                            .Replace("##PACIENTE##", turno.Paciente.ToString())
                                            .Replace("##DOCTOR##", turno.Doctor.ToString())
                                            .Replace("##ESPECIALIDAD##", turno.Especialidad.ToString())
                                            .Replace("##FECHA##", CultureInfo.CurrentCulture.TextInfo.ToTitleCase(turno.Horario.ToString("dddd dd/MM")))
                                            .Replace("##HORA##", turno.Horario.ToString("HH:mm"))
                                            .Replace("##CAUSAS##", turno.Causas);
                    break;
                case TipoMail.CancelacionTurno:
                    mensaje.To.Add(turno.Paciente.Email);
                    mensaje.To.Add(turno.Doctor.Email);
                    mensaje.Body = baseHtml.Replace("##TITULO##", tituloCancelacionTurno)
                                            .Replace("##CUERPO##", cuerpoCancelacionTurno)
                                            .Replace("##PACIENTE##", turno.Paciente.ToString())
                                            .Replace("##DOCTOR##", turno.Doctor.ToString())
                                            .Replace("##FECHA##", CultureInfo.CurrentCulture.TextInfo.ToTitleCase(turno.Horario.ToString("dddd dd/MM")))
                                            .Replace("##HORA##", turno.Horario.ToString("HH:mm"));
                    break;
            }
        }
    }
}
