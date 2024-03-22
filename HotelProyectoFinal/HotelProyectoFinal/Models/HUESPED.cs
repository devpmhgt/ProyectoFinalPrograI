//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HotelProyectoFinal.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public partial class HUESPED
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HUESPED()
        {
            this.REGISTROes = new HashSet<REGISTRO>();
        }
    
        public int Id { get; set; }

        [DisplayName("DPI")]
        [Required(ErrorMessage = "DPI es requerido")]
        [StringLength(12, MinimumLength = 5, ErrorMessage = "DPI debe tener entre 5 y 12 caracteres")]
        public string DPI { get; set; }

        [DisplayName("Nombres")]
        [Required(ErrorMessage = "El campo de Nombres es requerido")]
        public string Nombres { get; set; }

        [DisplayName("Apellidos")]
        [Required(ErrorMessage = "El campo de Apellidos es requerido")]
        public string Apellidos { get; set; }
        public string Direccion { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<REGISTRO> REGISTROes { get; set; }
    }
}