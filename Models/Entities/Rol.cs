namespace AtencionesApp.Models.Entities;
     public class Rol
  {
      public int Id { get; set; }
      public string Nombre { get; set; } = string.Empty;
      public bool PuedeCrear { get; set; }
      public bool PuedeLeer { get; set; }
      public bool PuedeEditar { get; set; }
      public bool PuedeEliminar { get; set; }

      public ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
  }