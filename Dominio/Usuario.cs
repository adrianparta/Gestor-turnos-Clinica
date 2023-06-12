namespace Dominio
{
    public class Usuario
    {
        public int IdUsuario{ get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
    }
}
