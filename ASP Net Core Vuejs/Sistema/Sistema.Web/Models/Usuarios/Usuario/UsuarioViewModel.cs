﻿namespace Sistema.Web.Models.Usuarios.Usuario
{
    public class UsuarioViewModel
    {
        public int idusuario { get; set; }
        public int idrol { get; set; }
        public string rol { get; set; }
        public string nombre { get; set; }
        public string tipo_documento { get; set; }
        public string num_documento { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }
        public byte[] password_hash { get; set; }
        public bool condicion { get; set; }
    }
}
