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
    
    public partial class HABITACION
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HABITACION()
        {
            this.ASIGNACIONs = new HashSet<ASIGNACION>();
        }
    
        public int IdHabitacion { get; set; }
        public int IdTipo { get; set; }
        public string Nivel { get; set; }
        public string Comentario { get; set; }
        public decimal Precio { get; set; }
        public string Disponible { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ASIGNACION> ASIGNACIONs { get; set; }
        public virtual TIPO TIPO { get; set; }
    }
}
