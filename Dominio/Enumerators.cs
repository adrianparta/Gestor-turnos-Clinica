public enum Estado
{
    Nuevo = 1, 
    Reprogramado, 
    NoAsistio, 
    Cancelado, 
    Cerrado
}

public enum TipoUsuario
{
    Admin = 1,
    Doctor = 2,
    Recepcionista = 3,
    Paciente = 4
}
public enum Sexo
{
    Masculino = 1,
    Femenino,
    PrefiereNoDecir
}
public enum Dia
{
    Lunes = 1,
    Martes,
    Miércoles,
    Jueves,
    Viernes,
    Sábado,
    Domingo
}
public enum TipoMail
{
    RegistroUsuario,
    ModificacionUsuario,
    AsignacionTurno,
    ReasignacionTurno,
    CancelacionTurno,
    ObservacionesTurno
}