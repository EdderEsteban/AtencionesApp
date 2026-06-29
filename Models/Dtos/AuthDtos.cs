namespace AtencionesApp.Models.Dtos;

public class LoginRequest
{
    public string Email { get; set; } = "";
    public string Password { get; set; } = "";
}

public class SeleccionInstitucionRequest
{
    public int InstitucionId { get; set; }
}

public class LoginResponse
{
    public string Token { get; set; } = "";
    public DateTime ExpiraUtc { get; set; }
    public int UsuarioId { get; set; }
    public string NombreCompleto { get; set; } = "";
    public string Email { get; set; } = "";
    public string Rol { get; set; } = "";
    public bool RequiereSeleccion { get; set; }      // true si tiene >1 y debe elegir
    public int? InstitucionActivaId { get; set; }    // set cuando ya quedó elegida
    public List<InstitucionDto> Instituciones { get; set; } = new();
}

public class InstitucionDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = "";
}
