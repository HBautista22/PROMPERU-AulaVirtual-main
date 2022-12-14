
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------


namespace PROMPERU.AULAVIRTUAL.BE.USUARIO
{

    using System;
    using System.Collections.Generic;
    using PROMPERU.AULAVIRTUAL.BE.PARAMETROS;
    using PROMPERU.AULAVIRTUAL.BE.CURSOS;

    public partial class Usuario
    {


        public Usuario()
        {

            this.CambioContrasena = new HashSet<CambioContrasena>();

            this.LogAccesosUsuario = new HashSet<LogAccesosUsuario>();

            this.LogCertificado = new HashSet<LogCertificado>();

            this.LogUnidad = new HashSet<LogUnidad>();

            this.MatriculaCursoOnline = new HashSet<MatriculaCursoOnline>();

            this.ParametroScorm = new HashSet<ParametroScorm>();

            this.UsuarioMultiEmpresa = new HashSet<UsuarioMultiEmpresa>();

            this.UsuarioGrupo = new HashSet<UsuarioGrupo>();

        }


        public int UsuarioId { get; set; }

        public string Codigo { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Grupo { get; set; }

        public string Cargo { get; set; }

        public string Rol { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public Nullable<int> EmpresaId { get; set; }

        public string Estado { get; set; }

        public string DNI { get; set; }

        public Nullable<bool> Conversion { get; set; }

        public Nullable<int> DistritoId { get; set; }

        public Nullable<int> NacionalidadId { get; set; }

        public Nullable<int> SexoId { get; set; }

        public Nullable<System.DateTime> UltimoAcceso { get; set; }

        public Nullable<int> TipoDocumento { get; set; }

        public string Telefono { get; set; }

        public string Sector { get; set; }

        public string Avatar { get; set; }

        public string Descripcion { get; set; }

        public string Curso { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

        public virtual ICollection<CambioContrasena> CambioContrasena { get; set; }

        public virtual Distrito Distrito { get; set; }

        public virtual Empresa Empresa { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

        public virtual ICollection<LogAccesosUsuario> LogAccesosUsuario { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

        public virtual ICollection<LogCertificado> LogCertificado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

        public virtual ICollection<LogUnidad> LogUnidad { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

        public virtual ICollection<MatriculaCursoOnline> MatriculaCursoOnline { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

        public virtual ICollection<ParametroScorm> ParametroScorm { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

        public virtual ICollection<UsuarioMultiEmpresa> UsuarioMultiEmpresa { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

        public virtual ICollection<UsuarioGrupo> UsuarioGrupo { get; set; }

    }

}
